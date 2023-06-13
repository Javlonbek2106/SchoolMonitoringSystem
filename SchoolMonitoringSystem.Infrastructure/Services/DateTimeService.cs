using StudentPaymentSystem.Application.Common.Interfaces;

namespace SchoolMonitoringSystem.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.UtcNow;
}
