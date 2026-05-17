using MediatR;
using SportAcademy.Application.Common.Result;
using SportAcademy.Application.Interfaces;

namespace SportAcademy.Application.Commands.EnrollmentCommands.UpdatePaymentStatus;

public class UpdatePaymentStatusCommandHandler : IRequestHandler<UpdatePaymentStatusCommand, Result<bool>>
{
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly ISubscriptionDetailsRepository _subscriptionRepository;
    private readonly IPaymentRepository _paymentRepository;
    private readonly string _operationType = "UpdatePaymentStatus";

    public UpdatePaymentStatusCommandHandler(
        IEnrollmentRepository enrollmentRepository,
        ISubscriptionDetailsRepository subscriptionRepository,
        IPaymentRepository paymentRepository)
    {
        _enrollmentRepository = enrollmentRepository;
        _subscriptionRepository = subscriptionRepository;
        _paymentRepository = paymentRepository;
    }

    public async Task<Result<bool>> Handle(UpdatePaymentStatusCommand request, CancellationToken cancellationToken)
    {
        var enrollment = await _enrollmentRepository.GetByIdAsync(request.Id, cancellationToken);
        if (enrollment == null)
            return Result<bool>.Failure(_operationType, "Enrollment not found", 404);

        return Result<bool>.Success(true, _operationType);
    }
}
