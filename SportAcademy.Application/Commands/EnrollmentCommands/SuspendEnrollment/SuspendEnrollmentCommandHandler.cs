using MediatR;
using SportAcademy.Application.Common.Result;
using SportAcademy.Application.Interfaces;

namespace SportAcademy.Application.Commands.EnrollmentCommands.SuspendEnrollment;

public class SuspendEnrollmentCommandHandler : IRequestHandler<SuspendEnrollmentCommand, Result<bool>>
{
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly string _operationType = "SuspendEnrollment";

    public SuspendEnrollmentCommandHandler(IEnrollmentRepository enrollmentRepository)
    {
        _enrollmentRepository = enrollmentRepository;
    }

    public async Task<Result<bool>> Handle(SuspendEnrollmentCommand request, CancellationToken cancellationToken)
    {
        var enrollment = await _enrollmentRepository.GetByIdAsync(request.Id, cancellationToken);
        if (enrollment == null)
            return Result<bool>.Failure(_operationType, "Enrollment not found", 404);

        enrollment.IsActive = false;
        await _enrollmentRepository.UpdateAsync(enrollment, cancellationToken);
        return Result<bool>.Success(true, _operationType);
    }
}
