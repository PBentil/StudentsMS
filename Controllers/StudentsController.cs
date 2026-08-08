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
    public async Task<IActionResult> GetStudents()
    {
        var students = await studentService.GetStudentsAsync();

        return Ok(students);
    }

    [HttpPost]
    public async Task<IActionResult> AddStudent(CreateStudentDto dto)
    {
        var newStudent = await studentService.AddStudentAsync(dto);

        return CreatedAtAction(
            nameof(GetStudentById),
            new { id = newStudent.Id },
            newStudent
        );
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetStudentById(int id)
    {
        var student = await studentService.GetStudentByIdAsync(id);

        if (student == null)
        {
            return NotFound();
        }

        return Ok(student);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStudent(
        int id,
        UpdateStudentDto dto)
    {
        var updatedStudent =
            await studentService.UpdateStudentAsync(id, dto);

        if (updatedStudent == null)
        {
            return NotFound();
        }

        return Ok(updatedStudent);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        var deleted = await studentService.DeleteStudentAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}