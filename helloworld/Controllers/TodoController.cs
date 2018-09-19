using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using helloworld.Models;

namespace helloworld.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext context;

        public TodoController(TodoContext context)
        {
            this.context = context;

            if(context.TodoItems.Count()==0)
            {
                context.TodoItems.Add(new TodoItem{ Name = "Item 1"});
                context.SaveChanges();
            }
            
        }
        [HttpGet]
        public ActionResult<List<TodoItem>> GetAll()
        {
            return this.context.TodoItems.ToList();
        }
        [HttpGet("{id}", Name="GetTodo")]
        public ActionResult<TodoItem> GetItemById(long id)
        {
           var item = this.context.TodoItems.Find(id);
           if (item==null) return NotFound();
           return item; 
        }

        [HttpPost]
        public IActionResult Create(TodoItem item)
        {
            this.context.TodoItems.Add(item);
            this.context.SaveChanges();

            return CreatedAtRoute("GetTodo", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, TodoItem item)
        {
            var todo = this.context.TodoItems.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.IsComplete = item.IsComplete;
            todo.Name = item.Name;

            this.context.TodoItems.Update(todo);
            this.context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = this.context.TodoItems.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            this.context.TodoItems.Remove(todo);
            this.context.SaveChanges();
            return NoContent();
        }
    }
}