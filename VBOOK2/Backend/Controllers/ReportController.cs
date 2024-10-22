using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VBOOK2.Backend.Data;
using VBOOK2.Backend.Models;

[ApiController]
[Route("api/[controller]")]
public class ReportController : ControllerBase
{
    private readonly ProfileBookContext _context;

    public ReportController(ProfileBookContext context)
    {
        _context = context;
    }

    // POST: api/report
    [HttpPost]
    public async Task<IActionResult> ReportUser([FromBody] Report report)
    {
        report.TimeStamp = DateTime.UtcNow;
        _context.Reports.Add(report);
        await _context.SaveChangesAsync();
        return Ok(report);
    }

    // GET: api/report
    [HttpGet]
    public async Task<IActionResult> GetReports()
    {
        var reports = await _context.Reports.ToListAsync();
        return Ok(reports);
    }
}
