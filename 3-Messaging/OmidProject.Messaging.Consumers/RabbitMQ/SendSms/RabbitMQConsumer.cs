using System.Text;
using OmidProject.Applications.ACL.Contracts.Sms;
using OmidProject.Infrastructures.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using static System.Formats.Asn1.AsnWriter;

namespace OmidProject.Messaging.Consumers.RabbitMQ.SendSms;

// تعریف کلاس RabbitMQConsumer که از رابط IMessageConsumer پیاده‌سازی شده است
public class RabbitMQConsumer : IMessageConsumer
{
    // متغیر خصوصی برای ذخیره تنظیمات RabbitMQ
    private readonly RabbitMQSettings _rabbitMqSettings;
    // متغیر خصوصی برای ایجاد scope جهت دسترسی به سرویس‌ها
    private readonly IServiceScopeFactory _serviceScopeFactory;

    // سازنده کلاس برای مقداردهی اولیه تنظیمات و ServiceScopeFactory
    public RabbitMQConsumer(IOptions<RabbitMQSettings> rabbitMqSettings, IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory; // مقداردهی ServiceScopeFactory
        _rabbitMqSettings = rabbitMqSettings.Value; // مقداردهی تنظیمات RabbitMQ از طریق IOptions
    }

    // متد اصلی برای شروع مصرف پیام‌ها از صف
    public async Task StartConsuming(CancellationToken stoppingToken)
    {
        // ایجاد کارخانه اتصال به RabbitMQ با تنظیمات مربوطه
        var factory = new ConnectionFactory
        {
            HostName = _rabbitMqSettings.Hostname, // تنظیم نام هاست
            UserName = _rabbitMqSettings.Username, // تنظیم نام کاربری
            Password = _rabbitMqSettings.Password  // تنظیم رمز عبور
        };

        // ایجاد اتصال به RabbitMQ به صورت async
        var connection = await factory.CreateConnectionAsync(stoppingToken);
        // ایجاد کانال ارتباطی به RabbitMQ
        var channel = await connection.CreateChannelAsync(cancellationToken: stoppingToken);

        // تعریف صف برای دریافت پیام‌ها
        await channel.QueueDeclareAsync(
            queue: _rabbitMqSettings.QueueName, // نام صف از تنظیمات
            durable: true, // پیام‌ها پایدار باشند
            exclusive: false, // صف توسط کانال‌های دیگر هم قابل دسترسی باشد
            autoDelete: false, // صف بعد از بسته شدن کانال حذف نشود
            cancellationToken: stoppingToken); // لغو عملیات در صورت توقف

        // ایجاد یک مصرف‌کننده غیرهمزمان
        var consumer = new AsyncEventingBasicConsumer(channel);

        // ایجاد scope برای دسترسی به سرویس ISmsAclService
        using var scope = _serviceScopeFactory.CreateScope();
        // دریافت سرویس ISmsAclService از DI
        var acl = scope.ServiceProvider.GetRequiredService<ISmsAclService>();

        // تعریف رویداد برای دریافت پیام‌ها از صف
        consumer.ReceivedAsync += async (model, ea) =>
        {
            // دریافت بایت‌های پیام
            var body = ea.Body.ToArray();
            // تبدیل بایت‌ها به رشته JSON
            var smsAclInputModelJson = Encoding.UTF8.GetString(body);
            // دی‌سریالایز کردن JSON به آبجکت SmsAclInputModel
            var smsAclInput = JsonConvert.DeserializeObject<SmsAclInputModel>(smsAclInputModelJson);
            // ارسال پیامک با استفاده از سرویس ISmsAclService
            await acl.Send(smsAclInput);
            // اعلام دریافت موفق پیام به RabbitMQ
            await channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false, cancellationToken: stoppingToken);
        };

        // شروع مصرف پیام‌ها از صف به صورت غیرهمزمان
        await channel.BasicConsumeAsync(
            queue: _rabbitMqSettings.QueueName, // نام صف
            autoAck: false, // تأیید دستی دریافت پیام
            consumer: consumer, // مصرف‌کننده‌ای که تعریف شده
            cancellationToken: stoppingToken); // لغو عملیات در صورت توقف
    }
}
