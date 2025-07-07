using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Frameworks_dev_web_I.Data;
using Frameworks_dev_web_I.Models;

namespace Frameworks_dev_web_I.Controllers
{
   [ApiController]
   [Route("api/[controller]")]
   public class ChatsController : ControllerBase
   {
      private readonly AppDbContext _context;
      public ChatsController(AppDbContext context) => _context = context;

      [HttpGet]
      public async Task<ActionResult<IEnumerable<Chat>>> Get() => await _context.Chats.ToListAsync();

      [HttpGet("{id}")]
      public async Task<ActionResult<Chat>> Get(long id)
      {
         var chat = await _context.Chats.FindAsync(id);
         if (chat == null) return NotFound();
         return chat;
      }

      [HttpPost]
      public async Task<ActionResult<Chat>> Post(Chat chat)
      {
         _context.Chats.Add(chat);
         await _context.SaveChangesAsync();
         return CreatedAtAction(nameof(Get), new { id = chat.Id }, chat);
      }

      [HttpPut("{id}")]
      public async Task<IActionResult> Put(long id, Chat chat)
      {
         if (id != chat.Id) return BadRequest();
         _context.Entry(chat).State = EntityState.Modified;
         try { await _context.SaveChangesAsync(); }
         catch (DbUpdateConcurrencyException) { if (!_context.Chats.Any(e => e.Id == id)) return NotFound(); else throw; }
         return NoContent();
      }

      [HttpDelete("{id}")]
      public async Task<IActionResult> Delete(long id)
      {
         var chat = await _context.Chats.FindAsync(id);
         if (chat == null) return NotFound();
         _context.Chats.Remove(chat);
         await _context.SaveChangesAsync();
         return NoContent();
      }
   }
}
