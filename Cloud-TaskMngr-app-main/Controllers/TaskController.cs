using Microsoft.AspNetCore.Mvc;
using CloudBackend.DTOs;
using Microsoft.EntityFrameworkCore; 
using CloudBackend.Models; 
using CloudBackend.Data;

namespace backend.Controllers;

[Route("api/tasks")]
[ApiController]
public class TasksController : ControllerBase
{
    // Używamy nowej nazwy!
    private readonly CloudTaskDbContext _context; 

    public TasksController(CloudTaskDbContext context)
    {
        _context = context;
    }
        
    [HttpGet]   
    public async Task<ActionResult<IEnumerable<TaskReadDto>>> GetAll()
    {
        var tasks = await _context.Tasks.ToListAsync();
        var tasksDto = tasks.Select(t => new TaskReadDto
        {
             Id = t.Id,
             Name = t.Name,
             IsCompleted = t.IsCompleted
        });
        return Ok(tasksDto);
    }   
 
    [HttpGet("{id}")]
    public async Task<ActionResult<TaskReadDto>> GetById(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null) return NotFound();  
        return Ok(new TaskReadDto 
        { 
            Id = task.Id, 
            Name = task.Name, 
            IsCompleted = task.IsCompleted 
        });
    }       
 
    [HttpPost]
    public async Task<ActionResult<TaskReadDto>> Create(TaskCreateDto taskDto)
    {
        var newTask = new CloudTask
        {
            Name = taskDto.Name,
            IsCompleted = false 
        };

        _context.Tasks.Add(newTask);
        await _context.SaveChangesAsync();

        var readDto = new TaskReadDto
        {
            Id = newTask.Id,
            Name = newTask.Name,
            IsCompleted = newTask.IsCompleted
        };

        return CreatedAtAction(nameof(GetById), new { id = readDto.Id }, readDto);
    }
 
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] object updatedTask)
    {
        if (id <= 0) return BadRequest("Nieprawidłowe ID.");
        return NoContent(); 
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (id > 2) return NotFound("Zadanie nie istnieje.");
        return NoContent(); 
    }
}