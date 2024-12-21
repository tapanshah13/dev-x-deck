using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyWebApi.Data;
using MyWebApi.Models;

namespace MyWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MyController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MyController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MyModel>>> Get()
        {
            return await _context.MyModels.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MyModel>> Get(int id)
        {
            var model = await _context.MyModels.FindAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return model;
        }

        [HttpPost]
        public async Task<ActionResult<MyModel>> Post(MyModel model)
        {
            _context.MyModels.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, MyModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _context.MyModels.FindAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            _context.MyModels.Remove(model);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
