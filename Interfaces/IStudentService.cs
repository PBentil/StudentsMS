using StudentManagementApi.DTOs;

namespace StudentManagementApi.Interfaces;

public interface IStudentService
{
    Task<List<StudentResponseDto>> GetStudentsAsync();

    Task<StudentResponseDto> AddStudentAsync(CreateStudentDto dto);

    Task<StudentResponseDto?> GetStudentByIdAsync(int id);

    Task<StudentResponseDto?> UpdateStudentAsync(
        int id,
        UpdateStudentDto dto);

    Task<bool> DeleteStudentAsync(int id);
}