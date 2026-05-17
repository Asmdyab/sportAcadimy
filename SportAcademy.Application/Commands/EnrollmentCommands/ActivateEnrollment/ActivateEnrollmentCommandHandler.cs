using MediatR;
using SportAcademy.Application.Common.Result;
using SportAcademy.Application.Interfaces;

namespace SportAcademy.Application.Commands.EnrollmentCommands.ActivateEnrollment;

public class ActivateEnrollmentCommandHandler : IRequestHandler<ActivateEnrollmentCommand, Result<bool>>
{
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly string _operationType = "ActivateEnrollment";

    public ActivateEnrollmentCommandHandler(IEnrollmentRepository enrollmentRepository)
    {
        _enrollmentRepository = enrollmentRepository;
    }

    public async Task<Result<bool>> Handle(ActivateEnrollmentCommand request, CancellationToken cancellationToken)
    {
        var enrollment = await _enrollmentRepository.GetByIdAsync(request.Id, cancellationToken);
        if (enrollment == null)
            return Result<bool>.Failure(_operationType, "Enrollment not found", 404);

        enrollment.IsActive = true;
        await _enrollmentRepository.UpdateAsync(enrollment, cancellationToken);
        return Result<bool>.Success(true, _operationType);
    }
}
