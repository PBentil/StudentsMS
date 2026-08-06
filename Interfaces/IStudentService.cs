using StudentManagementApi.Models;

namespace StudentManagementApi.Interfaces;


public interface IStudentService
{
    List<Student> GetStudents();    
    Student AddStudent(Student student);
    Student? GetByStudentById(int Id);
    Student? UpdateStudent(int id, Student student);
    bool? DeleteStudent(int id);
}