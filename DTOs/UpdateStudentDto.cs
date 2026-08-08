namespace StudentManagementApi.DTOs;

public class UpdateStudentDto
{
    public string StudentId { get; set; } = string.Empty;
    
    public string Name { get; set; } = string.Empty;
    
    public int Age { get; set; }
    
    public string Course { get; set; } = string.Empty;
}