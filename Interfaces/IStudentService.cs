using StudentManagementApi.DTOs;
using StudentManagementApi.Models;

namespace StudentManagementApi.Interfaces;


public interface IStudentService
{
    List<StudentResponseDto> GetStudents();

    StudentResponseDto AddStudent(CreateStudentDto dto);

    StudentResponseDto? GetStudentById(int id);

    StudentResponseDto? UpdateStudent(
        int id,
        UpdateStudentDto dto);

    bool DeleteStudent(int id);
}