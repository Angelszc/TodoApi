using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using System.Linq;
using System;

namespace TodoApi.Controllers
{
    // [controller] es el nombre de la clase TodoController,
    // de cuyo nombre se queda con la palabra raíz, es decir, "todo"
    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        private readonly TodoContext _context;

        public TodoController(TodoContext context)
        {
            _context = context;

            // Si la tabla (en memoria) está vacia añade 6 registros de muestra
            if (_context.TodoItems.Count() == 0)
            {
                // El Id único lo genera ef según el modelo TodoItem
                _context.TodoItems.Add(new TodoItem { Name = "Item1" });
                _context.TodoItems.Add(new TodoItem { Name = "Item2" });
                _context.TodoItems.Add(new TodoItem { Name = "Item3" });
                _context.TodoItems.Add(new TodoItem { Name = "Item4" });
                _context.TodoItems.Add(new TodoItem { Name = "Item5" });
                _context.TodoItems.Add(new TodoItem { Name = "Item6" });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable<TodoItem> GetAll()
        {
            return _context.TodoItems.ToList();
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public IActionResult GetById(long id)
        {
            var item = _context.TodoItems.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] TodoItem item)
        {
            //if ((item == null) || (String.IsNullOrEmpty(item.Name)))
            if (ModelState.IsValid)
            {
                return BadRequest();
            }

            _context.TodoItems.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetTodo", new { id = item.Id }, item);
        }

    }
}