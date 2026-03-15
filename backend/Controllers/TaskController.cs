using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        // ZADANIE 4.1 / 4.3: 5 Endpointów CRUD dla jednej encji (Zadania)
        
        // 1. GET: Lista (Read all)
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(new[] { 
                new { Id = 1, Title = "Skonfigurować Dockera", IsCompleted = true },
                new { Id = 2, Title = "Stworzyć Backend", IsCompleted = true }
            });
        }

        // 2. GET: Szczegóły (Read one)
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            if (id <= 0) return BadRequest("Nieprawidłowe ID zadania."); // Zad. 4.4 Walidacja
            if (id > 2) return NotFound("Zadanie nie istnieje."); // Zad. 4.4 Błąd
            
            return Ok(new { Id = id, Title = "Przykładowe zadanie", IsCompleted = false });
        }

        // 3. POST: Dodaj (Create)
        [HttpPost]
        public IActionResult Create([FromBody] object newTask)
        {
            if (newTask == null) return BadRequest("Dane nie mogą być puste."); // Zad. 4.4 Walidacja
            return CreatedAtAction(nameof(GetById), new { id = 3 }, newTask);
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