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
Duplicate requests are prevented by checking existence before adding.  

Run the project with Visual Studio or CLI: dotnet run --project ElevatorAPI.

Endpoints
•	POST /api/pickup  
    •	Purpose: Request elevator from a floor (external call).  
    •	Body: PickupRequest (JSON: { "floor": <int> }).  
    •	Behavior: If there is no existing request for that floor already present, adds FloorRequest with current RequestedAt timestamp.  
    •	Response: 202 Accepted.  
•	POST /api/floors  
    •	Purpose: Request elevator from inside the elevator (internal floor button).  
    •	Body: PickupRequest (JSON: { "floor": <int> }).  
    •	Behavior & response: Same as /api/pickup (idempotent for same floor), returns 202 Accepted.  
•	GET /api/requested-floors  
    •	Purpose: Return list of currently requested floor numbers.  
    •	Response: 200 OK with array of ints (e.g., [1, 4, 9]).  
•	GET /api/next-floor  
    •	Purpose: Return the next floor to service.  
    •	Behavior: Picks and returns earliest FloorRequest by RequestedAt.
    •	Response:  
        •	If a floor exists: 200 OK { floor: <int>, status: "moving" }  
        •	If none: 200 OK { floor: null, status: "idle" }  
•	POST /api/serviced  
    •	Purpose: Report a floor has been serviced.  
    •	Body: FloorServiced (JSON: { "floor": <int> }).  
    •	Behavior: Removes the earliest matching FloorRequest for that floor.  
    •	Response: 204 No Content.  
