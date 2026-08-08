using Microsoft.EntityFrameworkCore;
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

    public async Task<List<StudentResponseDto>> GetStudentsAsync()
    {
        return await context.Students
            .Select(student => new StudentResponseDto
            {
                Id = student.Id,
                StudentId = student.StudentId,
                Name = student.Name,
                Age = student.Age,
                Course = student.Course
            })
            .ToListAsync();
    }

    public async Task<StudentResponseDto> AddStudentAsync(
        CreateStudentDto dto)
    {
        var student = new Student
        {
            StudentId = dto.StudentId,
            Name = dto.Name,
            Age = dto.Age,
            Course = dto.Course
        };

        context.Students.Add(student);

        await context.SaveChangesAsync();

        return new StudentResponseDto
        {
            Id = student.Id,
            StudentId = student.StudentId,
            Name = student.Name,
            Age = student.Age,
            Course = student.Course
        };
    }

    public async Task<StudentResponseDto?> GetStudentByIdAsync(int id)
    {
        var student = await context.Students
            .FirstOrDefaultAsync(s => s.Id == id);

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

    public async Task<StudentResponseDto?> UpdateStudentAsync(
        int id,
        UpdateStudentDto dto)
    {
        var existingStudent = await context.Students
            .FirstOrDefaultAsync(s => s.Id == id);

        if (existingStudent == null)
        {
            return null;
        }

        existingStudent.StudentId = dto.StudentId;
        existingStudent.Name = dto.Name;
        existingStudent.Age = dto.Age;
        existingStudent.Course = dto.Course;

        await context.SaveChangesAsync();

        return new StudentResponseDto
        {
            Id = existingStudent.Id,
            StudentId = existingStudent.StudentId,
            Name = existingStudent.Name,
            Age = existingStudent.Age,
            Course = existingStudent.Course
        };
    }

    public async Task<bool> DeleteStudentAsync(int id)
    {
        var student = await context.Students
            .FirstOrDefaultAsync(s => s.Id == id);

        if (student == null)
        {
            return false;
        }

        context.Students.Remove(student);

        await context.SaveChangesAsync();

        return true;
    }
}