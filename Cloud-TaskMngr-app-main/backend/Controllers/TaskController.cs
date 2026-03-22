using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        // ZADANIE 4.1 / 4.3: 5 Endpointów CRUD dla jednej encji (Zadania)
        
        [HttpGet]   
        public async Task<ActionResult<IEnumerable<TaskReadDto>>> GetAll()
        {
            // Pobieramy encje z bazy danych
                var tasks = await _context.Tasks.ToListAsync();
            // Mapujemy każdą encję na obiekt DTO
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
            if (task == null) return NotFound();  // Zwracamy DTO zamiast czystej encji
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
    // 1. Mapowanie DTO -> Entity
    // Przekształcamy to, co przyszło z sieci, na model bazy danych
     var newTask = new CloudTask
      {
            Name = taskDto.Name,
         IsCompleted = false // Domyślnie nowe zadanie nie jest gotowe
      };

    // 2. Zapis do bazy danych
    _context.Tasks.Add(newTask);
    await _context.SaveChangesAsync();

    // 3. Mapowanie Entity -> DTO (Zwrotka)
    // Zwracamy TaskReadDto, który zawiera już nadane przez bazę Id
    var readDto = new TaskReadDto
    {
        Id = newTask.Id,
        Name = newTask.Name,
        IsCompleted = newTask.IsCompleted
    };

    return CreatedAtAction(nameof(GetById), new { id = readDto.Id }, readDto);
    }
 

        // 4. PUT: Edytuj (Update)
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] object updatedTask)
        {
            if (id <= 0) return BadRequest("Nieprawidłowe ID.");
            return NoContent(); // Sukces (204 No Content)
        }

        // 5. DELETE: Usuń (Delete)
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id > 2) return NotFound("Zadanie nie istnieje.");
            return NoContent(); // Sukces (204)
        }
    }
}