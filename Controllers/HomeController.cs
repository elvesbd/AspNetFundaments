using Microsoft.AspNetCore.Mvc;
using Todo.Data;
using Todo.Models;

namespace Todo.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet("/")]
        public List<TodoModel> Get([FromServices] AppDbContext ctx)
        {
            return ctx.Todos.ToList();
        }

        [HttpGet("/{id:int}")]
        public TodoModel Get([FromRoute] int id, [FromServices] AppDbContext ctx)
        {
            return ctx.Todos.FirstOrDefault(x => x.Id == id);
        }

        [HttpPost("/")]
        public TodoModel Post([FromBody] TodoModel todo, [FromServices] AppDbContext ctx)
        {
            ctx.Todos.Add(todo);
            ctx.SaveChanges();
            return todo;
        }

        [HttpPut("/{id:int}")]
        public TodoModel Put(
            [FromRoute] int id,
            [FromBody] TodoModel todo,
            [FromServices] AppDbContext ctx
        )
        {
            var model = ctx.Todos.FirstOrDefault(x => x.Id == id);
            if (model == null)
            {
                return todo;
            }

            model.Title = todo.Title;
            model.Done = todo.Done;
            ctx.Todos.Update(model);
            ctx.SaveChanges();
            return model;
        }

        [HttpDelete("/{id:int}")]
        public TodoModel Put([FromRoute] int id, [FromServices] AppDbContext ctx)
        {
            var model = ctx.Todos.FirstOrDefault(x => x.Id == id);

            ctx.Todos.Remove(model);
            ctx.SaveChanges();
            return model;
        }
    }
}