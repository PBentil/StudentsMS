using System.ComponentModel.DataAnnotations;

namespace StudentManagementApi.DTOs;

public class CreateStudentDto
{
    [Required]
    [StringLength(20)]
    public string StudentId { get; set; } = string.Empty;
    
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Name { get; set; } = string.Empty;
    
    [Range(16, 100)]
    public int Age { get; set; }
    
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Course { get; set; } = string.Empty;
}