using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ElevatorAPI.Data;
using ElevatorAPI.Models;

namespace ElevatorAPI.Controllers
{
    [ApiController]
    public sealed class ElevatorController : ControllerBase
    {
        private readonly ElevatorDbContext _context;

        public ElevatorController(ElevatorDbContext context)
        {
            _context = context;
        }

        // Request elevator from a floor
        [HttpPost("/api/pickup")]
        public async Task<IActionResult> RequestFromFloor(PickupRequest req)
        {
            // Check if the floor is already requested
            var existingRequest = await _context.FloorRequests
                .FirstOrDefaultAsync(f => f.Floor == req.Floor);

            if (existingRequest == null)
            {
                var floorRequest = new FloorRequest
                {
                    Floor = req.Floor,
                    RequestedAt = DateTime.UtcNow
                };
                _context.FloorRequests.Add(floorRequest);
                await _context.SaveChangesAsync();
            }
            
            return new AcceptedResult();
        }

        // Request elevator to a floor from inside the elevator
        [HttpPost("/api/floors")]
        public async Task<IActionResult> RequestFromElevatorAsync([FromBody] PickupRequest req)
        {
            // Check if floor is already requested
            var existingRequest = await _context.FloorRequests.FirstOrDefaultAsync(f => f.Floor == req.Floor);

            if (existingRequest == null)
            {
                var floorRequest = new FloorRequest
                {
                    Floor = req.Floor,
                    RequestedAt = DateTime.UtcNow
                };
                _context.FloorRequests.Add(floorRequest);
                await _context.SaveChangesAsync();
            }
            
            return new AcceptedResult();
        }

        // Get all requested floors
        [HttpGet("/api/requested-floors")]
        public async Task<IResult> GetRequestedFloors()
        {
            var floors = await _context.FloorRequests.Select(f => f.Floor).ToListAsync();
            
            return Results.Ok(floors);
        }

        // Get the next floor to service
        [HttpGet("/api/next-floor")]
        public async Task<IResult> GetNextFloor()
        {
            var nextFloor = await _context.FloorRequests.OrderBy(f => f.RequestedAt).FirstOrDefaultAsync();

            if (nextFloor != null)
            {
                return Results.Ok(new { floor = nextFloor.Floor, status = "moving" });
            }
            
            return Results.Ok(new { floor = (int?)null, status = "idle" });
        }

        // Report that a floor has been serviced
        [HttpPost("/api/serviced")]
        public async Task<IActionResult> ReportFloorServiced(FloorServiced req)
        {
            var floorRequest = await _context.FloorRequests.OrderBy(f => f.RequestedAt).FirstOrDefaultAsync(f => f.Floor == req.Floor);

            if (floorRequest != null)
            {
                _context.FloorRequests.Remove(floorRequest);
                await _context.SaveChangesAsync();
            }

            return new NoContentResult();
        }
        
    }
}
