using SportAcademy.Application.Commands.EnrollmentCommands.CreateEnrollment;
using SportAcademy.Application.Commands.EnrollmentCommands.UpdateEnrollment;
using SportAcademy.Application.DTOs.EnrollmentDtos;
using SportAcademy.Domain.Entities;

namespace SportAcademy.Application.Mappings.EnrollmentProfile
{
    public class EnrollmentMappingProfile : AutoMapper.Profile
    {
        public EnrollmentMappingProfile()
        {
            CreateMap<Enrollment, EnrollmentDto>()
                .ReverseMap();

            CreateMap<CreateEnrollmentCommand, Enrollment>();

            CreateMap<UpdateEnrollmentCommand, Enrollment>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Enrollment, EnrollmentCardDto>()
                .ForMember(dest => dest.TraineeName,
                    opt => opt.MapFrom(src => $"{src.Trainee.FirstName} {src.Trainee.LastName}"))
                .ForMember(dest => dest.TraineeEmail,
                    opt => opt.MapFrom(src => src.Trainee.Email.Value))
                .ForMember(dest => dest.Sport,
                    opt => opt.MapFrom(src => src.TraineeGroup.Coach.Sport.Name))
                .ForMember(dest => dest.Program,
                    opt => opt.MapFrom(src => src.TraineeGroup.Name))
                .ForMember(dest => dest.Branch,
                    opt => opt.MapFrom(src => src.TraineeGroup.Branch.Name))
                .ForMember(dest => dest.CoachName,
                    opt => opt.MapFrom(src => $"{src.TraineeGroup.Coach.Employee.FirstName} {src.TraineeGroup.Coach.Employee.LastName}"))
                .ForMember(dest => dest.StartDate,
                    opt => opt.MapFrom(src => src.SubscriptionDetails.StartDate.ToDateTime(TimeOnly.MinValue)))
                .ForMember(dest => dest.EndDate,
                    opt => opt.MapFrom(src => src.SubscriptionDetails.EndDate.ToDateTime(TimeOnly.MinValue)))
                .ForMember(dest => dest.MonthlyFee,
                    opt => opt.MapFrom(src => src.SubscriptionDetails.SportPrice.Price))
                .ForMember(dest => dest.PaymentStatus,
                    opt => opt.MapFrom(src => src.SessionRemaining > 0 ? "Pending" : "Paid"))
                .ForMember(dest => dest.Status,
                    opt => opt.MapFrom(src => src.IsActive ? "Active" : "Suspended"))
                .ForMember(dest => dest.SessionsCompleted,
                    opt => opt.MapFrom(src => src.SessionAllowed - src.SessionRemaining))
                .ForMember(dest => dest.TotalSessions,
                    opt => opt.MapFrom(src => src.SessionAllowed));
        }
    }
}
