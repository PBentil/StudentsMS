using StudentManagementApi.Data;
using StudentManagementApi.DTOs;
using StudentManagementApi.Interfaces;
using StudentManagementApi.Models;

namespace StudentManagementApi.Services;

public class StudentService : IStudentService
{
    private readonly AppDbContext context;

    public StudentService(AppDbContext context)
    {
        this.context = context;
    }

    public List<StudentResponseDto> GetStudents()
    {
        return context.Students
            .Select(student => new StudentResponseDto
            {
                Id = student.Id,
                StudentId = student.StudentId,
                Name = student.Name,
                Age = student.Age,
                Course = student.Course
            })
            .ToList();
    }

    public StudentResponseDto AddStudent(CreateStudentDto dto)
    {
        var student = new Student
        {
            StudentId = dto.StudentId,
            Name = dto.Name,
            Age = dto.Age,
            Course = dto.Course
        };

        context.Students.Add(student);
        context.SaveChanges();

        return new StudentResponseDto
        {
            Id = student.Id,
            StudentId = student.StudentId,
            Name = student.Name,
            Age = student.Age,
            Course = student.Course
        };
    }

    public StudentResponseDto? GetStudentById(int id)
    {
        var student = context.Students
            .FirstOrDefault(s => s.Id == id);

        if (student == null)
        {
            return null;
        }

        return new StudentResponseDto
        {
            Id = student.Id,
            StudentId = student.StudentId,
            Name = student.Name,
            Age = student.Age,
            Course = student.Course
        };
    }

    public StudentResponseDto? UpdateStudent(
        int id,
        UpdateStudentDto dto)
    {
        var existingStudent = context.Students
            .FirstOrDefault(s => s.Id == id);

        if (existingStudent == null)
        {
            return null;
        }

        existingStudent.StudentId = dto.StudentId;
        existingStudent.Name = dto.Name;
        existingStudent.Age = dto.Age;
        existingStudent.Course = dto.Course;

        context.SaveChanges();

        return new StudentResponseDto
        {
            Id = existingStudent.Id,
            StudentId = existingStudent.StudentId,
            Name = existingStudent.Name,
            Age = existingStudent.Age,
            Course = existingStudent.Course
        };
    }

    public bool DeleteStudent(int id)
    {
        var student = context.Students
            .FirstOrDefault(s => s.Id == id);

        if (student == null)
        {
            return false;
        }

        context.Students.Remove(student);
        context.SaveChanges();

        return true;
    }
}