using StudentPaymentSystem.Application.Common.Interfaces;

namespace SchoolMonitoringSystem.Infrastructure;

public class GuidGeneratorService : IGuidGenerator
{
    public Guid Guid => Guid.NewGuid();
}
