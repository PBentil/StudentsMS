using Microsoft.AspNetCore.Http.HttpResults;
using StudentManagementApi.Data;
using StudentManagementApi.Models;
using StudentManagementApi.Interfaces;

namespace StudentManagementApi.Services;

public class StudentService : IStudentService
{
    private readonly AppDbContext context;

    public StudentService(AppDbContext context)
    {
        this.context = context;
    }

    public List<Student> GetStudents()
    {
        return context.Students.ToList();
    }

    public Student AddStudent(Student student)
    {
        context.Students.Add(student);
        
        context.SaveChanges();
        return student;
    }

    public Student? GetByStudentById(int Id)
    {
        return context.Students.FirstOrDefault(s => s.Id == Id);
    }

    public Student? UpdateStudent(int id, Student student)
    {
        var existingStudent = context.Students.FirstOrDefault(s => s.Id == id);

        if (existingStudent == null)
        {
            return null;
        }

        existingStudent.StudentId = student.StudentId;
        existingStudent.Name = student.Name;
        existingStudent.Age = student.Age;
        existingStudent.Course = student.Course;
        
        context.SaveChanges();
        return existingStudent;
    }

    public bool? DeleteStudent(int id)
    {
        var student = context.Students.FirstOrDefault(s => s.Id == id);

        if (student == null)
        {
            return null;
        }
        
        context.Students.Remove(student);
        context.SaveChanges();
        return true;
        
    }
    
}