using Microsoft.AspNetCore.Mvc;
using StudentManagementApi.DTOs;
using StudentManagementApi.Interfaces;

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
    public IActionResult AddStudent(CreateStudentDto dto)
    {
        var newStudent = studentService.AddStudent(dto);

        return CreatedAtAction(
            nameof(GetStudentById),
            new { id = newStudent.Id },
            newStudent
        );
    }

    [HttpGet("{id}")]
    public IActionResult GetStudentById(int id)
    {
        var student = studentService.GetStudentById(id);

        if (student == null)
        {
            return NotFound();
        }

        return Ok(student);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateStudent(
        int id,
        UpdateStudentDto dto)
    {
        var updatedStudent = studentService.UpdateStudent(
            id,
            dto
        );

        if (updatedStudent == null)
        {
            return NotFound();
        }

        return Ok(updatedStudent);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteStudent(int id)
    {
        var deleted = studentService.DeleteStudent(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}