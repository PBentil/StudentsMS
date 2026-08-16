using StudentManagementApi.DTOs;

namespace StudentManagementApi.Interfaces;

public interface IStudentService
{
    Task<PagedResponseDto<StudentResponseDto>> GetStudentsAsync(
        int page,
        int pageSize
    );

    Task<StudentResponseDto> AddStudentAsync(CreateStudentDto dto);

    Task<StudentResponseDto?> GetStudentByIdAsync(int id);

    Task<StudentResponseDto?> UpdateStudentAsync(
        int id,
        UpdateStudentDto dto);

    Task<bool> DeleteStudentAsync(int id);
}