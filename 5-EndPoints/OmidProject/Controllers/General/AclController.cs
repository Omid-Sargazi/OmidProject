using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using OmidProject.Applications.Contracts.SmsContracts.Commands;
using OmidProject.Frameworks.Contracts.Markers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OmidProject.Host.Controllers.General;

/// <summary>
///     Controller responsible for handling SMS related operations.
/// </summary>
public class SmsController(IDistributor distributor) : MainController(distributor)
{
    /// <summary>
    ///     Sends an SMS via the ACL service.
    /// </summary>
    /// <param name="smsCommand">The command containing SMS details.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>An IActionResult indicating the outcome of the operation.</returns>
    [Description("وب سرویس اس ام اس")]
    [HttpPost("send-sms")]
    public async Task<IActionResult> SendSmsAsync(SendSmsCommand smsCommand, CancellationToken cancellationToken)
    {
        var response =
            await Distributor.PushCommand<SendSmsCommand, SendSmsCommandResponse>(smsCommand, cancellationToken);
        return OkApiResult(response);
    }
}