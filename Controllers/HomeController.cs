using Microsoft.AspNetCore.Mvc;
using Todo.Data;
using Todo.Migrations;
using Todo.Models;

namespace Todo.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet("/")]
        public IActionResult Get([FromServices] AppDbContext ctx)
            => Ok(ctx.Todos.ToList());

        [HttpGet("/{id:int}")]
        public IActionResult GetById([FromRoute] int id, [FromServices] AppDbContext ctx)
        {
            var todos = ctx.Todos.FirstOrDefault(x => x.Id == id);
            if (todos == null)
                return NotFound();

            return Ok(todos);
        }

        [HttpPost("/")]
        public IActionResult Post([FromBody] TodoModel todo, [FromServices] AppDbContext ctx)
        {
            ctx.Todos.Add(todo);
            ctx.SaveChanges();
            return Create($"/{todo.Id}", todo);
        }

        [HttpPut("/{id:int}")]
        public IActionResult Put(
            [FromRoute] int id,
            [FromBody] TodoModel todo,
            [FromServices] AppDbContext ctx
        )
        {
            var model = ctx.Todos.FirstOrDefault(x => x.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            model.Title = todo.Title;
            model.Done = todo.Done;
            ctx.Todos.Update(model);
            ctx.SaveChanges();
            return Ok(model);
        }

        [HttpDelete("/{id:int}")]
        public IActionResult Put([FromRoute] int id, [FromServices] AppDbContext ctx)
        {
            var model = ctx.Todos.FirstOrDefault(x => x.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            ctx.Todos.Remove(model);
            ctx.SaveChanges();
            return Ok(model);
        }
    }
}