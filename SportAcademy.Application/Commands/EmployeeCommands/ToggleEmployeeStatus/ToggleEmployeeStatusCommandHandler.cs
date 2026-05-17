using MediatR;
using SportAcademy.Application.Common.Result;
using SportAcademy.Application.Interfaces;

namespace SportAcademy.Application.Commands.EmployeeCommands.ToggleEmployeeStatus
{
    public class ToggleEmployeeStatusCommandHandler : IRequestHandler<ToggleEmployeeStatusCommand, Result<bool>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly string _operationType = "ToggleEmployeeStatus";

        public ToggleEmployeeStatusCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Result<bool>> Handle(ToggleEmployeeStatusCommand request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetByIdAsync(request.Id, cancellationToken);
            if (employee == null)
                return Result<bool>.Failure(_operationType, "Employee not found", 404);

            employee.IsWork = !employee.IsWork;
            await _employeeRepository.UpdateAsync(employee, cancellationToken);
            return Result<bool>.Success(true, _operationType);
        }
    }
}
