namespace Logic.Abstraction;

public class InputDevice
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public required string Description { get; set; }

    public required string Category { get; set; }

    public IEnumerable<Reservation> Reservations { get; set; } = new List<Reservation>();
}