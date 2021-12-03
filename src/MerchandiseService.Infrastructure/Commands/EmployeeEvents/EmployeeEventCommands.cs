using MediatR;

namespace MerchandiseService.Infrastructure.Commands.EmployeeEvents
{
    public abstract class EmployeeEventCommand : IRequest
    {
        protected EmployeeEventCommand(EmployeeEventPayload payload) => Payload = payload;
        
        public EmployeeEventPayload Payload { get; }
    }
    
    public record EmployeeEventPayload
    {
        public string EmployeeEmail { get; init; }
        public string EmployeeName { get; init; }
        public string ManagerEmail { get; init; }
        public string ManagerName { get; init; }
        public int? ClothingSize { get; init; }
        public int? MerchType { get; init; }
    }

    public class EmployeeDismissalCommand : EmployeeEventCommand
    {
        public EmployeeDismissalCommand(EmployeeEventPayload payload) : base(payload) {}
    }

    public class EmployeeHiredCommand : EmployeeEventCommand
    {
        public EmployeeHiredCommand(EmployeeEventPayload payload) : base(payload) {}
    }

    public class EmployeeConferenceAttendanceCommand : EmployeeEventCommand
    {
        public EmployeeConferenceAttendanceCommand(EmployeeEventPayload payload) : base(payload) {}
    }

    public class EmployeeProbationPeriodEndingCommand : EmployeeEventCommand
    {
        public EmployeeProbationPeriodEndingCommand(EmployeeEventPayload payload) : base(payload) {}
    }
}