using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VBOOK2.Backend.Data;
using VBOOK2.Backend.Models;

[ApiController]
[Route("api/[controller]")]
public class MessageController : ControllerBase
{
    private readonly ProfileBookContext _context;

    public MessageController(ProfileBookContext context)
    {
        _context = context;
    }

    // POST: api/message/send
    [HttpPost("send")]
    public async Task<IActionResult> SendMessage([FromBody] Message message)
    {
        message.TimeStamp = DateTime.UtcNow;
        _context.Messages.Add(message);
        await _context.SaveChangesAsync();

        // SignalR hub invocation (implement this method in your hub)
        // await _hubContext.Clients.User(message.ReceiverId.ToString()).SendAsync("ReceiveMessage", message);

        return Ok(message);
    }

    // GET: api/message/{userId}
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetMessages(int userId)
    {
        var messages = await _context.Messages
            .Where(m => m.ReceiverId == userId || m.SenderId == userId)
            .ToListAsync();
        return Ok(messages);
    }
}
