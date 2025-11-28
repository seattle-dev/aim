using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using ElevatorAPI.Controllers;
using ElevatorAPI.Data;
using ElevatorAPI.Models;

namespace ElevatorAPI.Tests;

public class ElevatorControllerTests : IDisposable
{
    private readonly ElevatorDbContext _context;
    private readonly ElevatorController _controller;

    public ElevatorControllerTests()
    {
        var options = new DbContextOptionsBuilder<ElevatorDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new ElevatorDbContext(options);
        _controller = new ElevatorController(_context);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    [Fact]
    public async Task RequestFromFloor_AddsNewFloorRequest()
    {
        // Arrange
        var request = new PickupRequest(5);

        // Act
        var result = await _controller.RequestFromFloor(request);

        // Assert
        Assert.IsType<AcceptedResult>(result);
        var floorRequest = await _context.FloorRequests.FirstOrDefaultAsync(f => f.Floor == 5);
        Assert.NotNull(floorRequest);
        Assert.Equal(5, floorRequest.Floor);
    }

    [Fact]
    public async Task RequestFromFloor_DoesNotAddDuplicateFloor()
    {
        // Arrange
        var request = new PickupRequest(3);
        await _controller.RequestFromFloor(request);

        // Act
        var result = await _controller.RequestFromFloor(request);

        // Assert
        Assert.IsType<AcceptedResult>(result);
        var count = await _context.FloorRequests.CountAsync(f => f.Floor == 3);
        Assert.Equal(1, count);
    }

    [Fact]
    public async Task RequestFromElevatorAsync_AddsNewFloorRequest()
    {
        // Arrange
        var request = new PickupRequest(7);

        // Act
        var result = await _controller.RequestFromElevatorAsync(request);

        // Assert
        Assert.IsType<AcceptedResult>(result);
        var floorRequest = await _context.FloorRequests.FirstOrDefaultAsync(f => f.Floor == 7);
        Assert.NotNull(floorRequest);
        Assert.Equal(7, floorRequest.Floor);
    }

    [Fact]
    public async Task RequestFromElevatorAsync_DoesNotAddDuplicateFloor()
    {
        // Arrange
        var request = new PickupRequest(4);
        await _controller.RequestFromElevatorAsync(request);

        // Act
        var result = await _controller.RequestFromElevatorAsync(request);

        // Assert
        Assert.IsType<AcceptedResult>(result);
        var count = await _context.FloorRequests.CountAsync(f => f.Floor == 4);
        Assert.Equal(1, count);
    }

    [Fact]
    public async Task GetRequestedFloors_ReturnsEmptyList_WhenNoRequests()
    {
        // Act
        var result = await _controller.GetRequestedFloors();

        // Assert
        var okResult = Assert.IsType<Microsoft.AspNetCore.Http.HttpResults.Ok<List<int>>>(result);
        Assert.Empty(okResult.Value);
    }

    [Fact]
    public async Task GetRequestedFloors_ReturnsAllRequestedFloors()
    {
        // Arrange
        await _controller.RequestFromFloor(new PickupRequest(2));
        await _controller.RequestFromFloor(new PickupRequest(5));
        await _controller.RequestFromFloor(new PickupRequest(8));

        // Act
        var result = await _controller.GetRequestedFloors();

        // Assert
        var okResult = Assert.IsType<Microsoft.AspNetCore.Http.HttpResults.Ok<List<int>>>(result);
        Assert.Equal(3, okResult.Value.Count);
        Assert.Contains(2, okResult.Value);
        Assert.Contains(5, okResult.Value);
        Assert.Contains(8, okResult.Value);
    }

    [Fact]
    public async Task GetNextFloor_ReturnsIdle_WhenNoRequests()
    {
        // Act
        var result = await _controller.GetNextFloor();

        // Assert
        Assert.IsAssignableFrom<Microsoft.AspNetCore.Http.IResult>(result);
        var okResult = result.GetType().GetProperty("Value")?.GetValue(result);
        Assert.NotNull(okResult);
        
        var floorProp = okResult.GetType().GetProperty("floor")?.GetValue(okResult);
        var statusProp = okResult.GetType().GetProperty("status")?.GetValue(okResult);
        
        Assert.Null(floorProp);
        Assert.Equal("idle", statusProp);
    }

    [Fact]
    public async Task ReportFloorServiced_RemovesFloorRequest()
    {
        // Arrange
        await _controller.RequestFromFloor(new PickupRequest(6));
        var servicedRequest = new FloorServiced(6);

        // Act
        var result = await _controller.ReportFloorServiced(servicedRequest);

        // Assert
        Assert.IsType<NoContentResult>(result);
        var floorRequest = await _context.FloorRequests.FirstOrDefaultAsync(f => f.Floor == 6);
        Assert.Null(floorRequest);
    }

    [Fact]
    public async Task ReportFloorServiced_DoesNothing_WhenFloorNotRequested()
    {
        // Arrange
        await _controller.RequestFromFloor(new PickupRequest(3));
        var servicedRequest = new FloorServiced(99);

        // Act
        var result = await _controller.ReportFloorServiced(servicedRequest);

        // Assert
        Assert.IsType<NoContentResult>(result);
        var count = await _context.FloorRequests.CountAsync();
        Assert.Equal(1, count);
    }
}
