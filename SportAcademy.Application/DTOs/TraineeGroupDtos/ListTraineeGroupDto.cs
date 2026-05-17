using SportAcademy.Application.DTOs.GroupScheduleDtos;

namespace SportAcademy.Application.DTOs.TraineeGroupDtos
{
    public class ListTraineeGroupDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SportName { get; set; } = string.Empty;
        public string CoachName { get; set; } = string.Empty;
        public string BranchName { get; set; } = string.Empty;
        public int DurationInMinutes { get; set; }
        public int TraineesCount { get; set; }
        public List<GroupScheduleDto> Schedules { get; set; } = [];
    }
}
