# ElevatorAPI solution  

•	ElevatorAPI\Controllers\ElevatorController.cs — HTTP endpoints that expose elevator operations.  
•	ElevatorAPI\Data\ElevatorDbContext.cs — EF Core DbContext responsible for persisting FloorRequest entities.  
•	ElevatorAPI\Models\ElevatorModels.cs — DTOs and entity model:  
    •	PickupRequest(int Floor) — request DTO for requesting an elevator.  
    •	FloorServiced(int Floor) — DTO signaling a serviced floor.  
    •	FloorRequest — persistent entity with Id, Floor, RequestedAt.  
•	ElevatorAPI\Program.cs — app startup, DI, middleware, and service registration.  
•	appsettings.json / appsettings.Development.json — configuration files.  
•	ElevatorAPI\Properties\launchSettings.json — local launch profiles.  
•	ElevatorAPI.Tests\ElevatorControllerTests.cs — unit tests exercising API behavior in the controller.  

The assumptions used in solution:  
•	There is only one elevator car  
•	Elevator requests are processed according to the time they were entered  

Solution utilizes Entity Framework to store elevator requests.  

Run the project with Visual Studio or CLI: dotnet run --project ElevatorAPI  
