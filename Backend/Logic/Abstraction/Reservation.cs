namespace Logic.Abstraction;

public class Reservation
{
    public int Id { get; set; }

    public required string Username { get; set; }

    public InputDevice? Device   { get; set; }
    public int          DeviceId { get; set; }

    public DateTime ReservationFrom { get; set; }

    public DateTime ReservationTo { get; set; }
}