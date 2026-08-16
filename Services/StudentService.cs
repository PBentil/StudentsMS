using Microsoft.EntityFrameworkCore;
using StudentManagementApi.Data;
using StudentManagementApi.DTOs;
using StudentManagementApi.Interfaces;
using StudentManagementApi.Models;

namespace StudentManagementApi.Services;

public class StudentService : IStudentService
{
    private readonly AppDbContext context;
    private readonly ILogger<StudentService> logger;

    public StudentService(AppDbContext context, ILogger<StudentService> logger)
    {
        this.context = context;
        this.logger = logger;
    }

    public async Task<PagedResponseDto<StudentResponseDto>> GetStudentsAsync(
        int page,
        int pageSize)
    {
        var totalRecords = await context.Students.CountAsync();

        var students = await context.Students
            .OrderBy(student => student.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(student => new StudentResponseDto
            {
                Id = student.Id,
                StudentId = student.StudentId,
                Name = student.Name,
                Age = student.Age,
                Course = student.Course
            })
            .ToListAsync();

        var totalPages = (int)Math.Ceiling(
            (double)totalRecords / pageSize
        );

        return new PagedResponseDto<StudentResponseDto>
        {
            Data = students,
            Page = page,
            PageSize = pageSize,
            TotalRecords = totalRecords,
            TotalPages = totalPages
        };
    }

    public async Task<StudentResponseDto> AddStudentAsync(
        CreateStudentDto dto)
    {
        logger.LogInformation(
            "Creating student with StudentId {StudentId}.",
            dto.StudentId
        );

        var student = new Student
        {
            StudentId = dto.StudentId,
            Name = dto.Name,
            Age = dto.Age,
            Course = dto.Course
        };

        context.Students.Add(student);

        await context.SaveChangesAsync();

        logger.LogInformation(
            "Student {StudentId} created successfully with database ID {Id}.",
            student.StudentId,
            student.Id
        );

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
        logger.LogInformation(
            "Looking for student with ID {Id}.",
            id
        );

        var student = await context.Students
            .FirstOrDefaultAsync(s => s.Id == id);

        if (student == null)
        {
            logger.LogWarning(
                "Student with ID {Id} was not found.",
                id
            );

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
        logger.LogInformation(
            "Updating student with ID {Id}.",
            id
        );

        var existingStudent = await context.Students
            .FirstOrDefaultAsync(s => s.Id == id);

        if (existingStudent == null)
        {
            logger.LogWarning(
                "Cannot update student. Student with ID {Id} was not found.",
                id
            );

            return null;
        }

        existingStudent.StudentId = dto.StudentId;
        existingStudent.Name = dto.Name;
        existingStudent.Age = dto.Age;
        existingStudent.Course = dto.Course;

        await context.SaveChangesAsync();

        logger.LogInformation(
            "Student with ID {Id} updated successfully.",
            id
        );

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
        logger.LogInformation(
            "Deleting student with ID {Id}.",
            id
        );

        var student = await context.Students
            .FirstOrDefaultAsync(s => s.Id == id);

        if (student == null)
        {
            logger.LogWarning(
                "Cannot delete student. Student with ID {Id} was not found.",
                id
            );

            return false;
        }

        context.Students.Remove(student);

        await context.SaveChangesAsync();

        logger.LogInformation(
            "Student with ID {Id} deleted successfully.",
            id
        );

        return true;
    }
}