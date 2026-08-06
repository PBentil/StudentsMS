namespace StudentManagementApi.Models;

public class Student
{
    public int Id { get; set; }

    public string StudentId { get; set; } = string.Empty;
    
    public string Name { get; set; } = string.Empty;
    
    public int Age { get; set; }
    
    public string Course { get; set; } = string.Empty;
}