using Microsoft.AspNetCore.Mvc;
using StudentManagementApi.Interfaces;
using StudentManagementApi.Models;

namespace StudentManagementApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
  private readonly IStudentService studentService;
  
  public StudentsController(IStudentService studentService)
  {
    this.studentService = studentService;
  }
  [HttpGet]
  public IActionResult GetStudents()
  {
    var students = studentService.GetStudents();
    return Ok(students);
  }

  [HttpPost]
  public IActionResult AddStudent(Student student)
  {
    var newStudent = studentService.AddStudent(student);
    return Ok(newStudent);
  }

  [HttpGet("{id}")]
  public IActionResult GetStudentById(int id)
  {
    var student = studentService.GetByStudentById(id);

    if (student == null)
    {
      return NotFound();
    }
    return Ok(student);
  }

  [HttpPut("{id}")]
  public IActionResult UpdateStudent(int id, Student student)
  {
    var updatedStudent = studentService.UpdateStudent(id, student);

    if (updatedStudent == null)
    {
      return NotFound();
    }
    
    return Ok(updatedStudent);
  }

  [HttpDelete("{id}")]
  public IActionResult DeleteStudent(int id)
  {
    var student = studentService.DeleteStudent(id);
    if (student == null)
    {
      return NotFound();
    }
    return Ok(new
    {
      message = "Student deleted successfully",
    });
  }
}