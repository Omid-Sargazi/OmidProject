using OmidProject.Frameworks.Contracts.Markers;

namespace OmidProject.Infrastructures.Configurations.RegisterTypes;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime GetNow()
    {
        return DateTime.Now;
    }
}