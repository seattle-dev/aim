namespace ElevatorAPI.Models
{
    public record PickupRequest(int Floor);
    public record FloorServiced(int Floor);

    public class FloorRequest
    {
        public int Id { get; set; }
        public int Floor { get; set; }
        public DateTime RequestedAt { get; set; }
    }
}
