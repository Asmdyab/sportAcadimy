namespace SportAcademy.Application.DTOs.CoachDtos
{
    public class CoachDropdownDto
    {
        public int Id { get; set; }
        public string EmployeeFirstName { get; set; } = string.Empty;
        public string EmployeeLastName { get; set; } = string.Empty;
        public int BranchId { get; set; }
        public string BranchName { get; set; } = string.Empty;
    }
}
