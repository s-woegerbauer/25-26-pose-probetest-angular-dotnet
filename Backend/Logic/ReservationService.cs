namespace Logic;

using Logic.Abstraction;

using Microsoft.Extensions.Configuration;

public class ReservationService : IReservationService
{
    private IConfiguration _configuration;

    public ReservationService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<IList<Reservation>> GetAllReservationsAsync()
    {
        await Task.CompletedTask;
        return _allReservations;
    }

    public async Task<IList<Reservation>> GetReservationsForInputDeviceAsync(int inputDeviceId)
    {
        await Task.CompletedTask;
        return _allReservations.Where(r => r.DeviceId == inputDeviceId).ToList();
    }

    public async Task<Reservation?> GetByIdAsync(int id)
    {
        await Task.CompletedTask;
        return _allReservations.FirstOrDefault(r => r.Id == id);
    }

    public async Task<Reservation?> AddAsync(int inputDeviceId, string userName, DateTime fromDateTime)
    {
        await Task.CompletedTask;

        // check, if reservation is allowed

        var fromTime = TimeOnly.FromDateTime(fromDateTime);
        var toTime = TimeOnly.FromDateTime(fromDateTime.AddMinutes(10));

        var overlappingReservations = _allReservations
            .Where(r => r.DeviceId == inputDeviceId)
            .Where(r => TimeOnly.FromDateTime(r.ReservationFrom) < toTime && TimeOnly.FromDateTime(r.ReservationTo) > fromTime);

        if (overlappingReservations.Any())
        {
            return null;
        }

        var add = new Reservation()
        {
            Id              = _allReservations.Max(r => r.Id) + 1,
            DeviceId        = inputDeviceId,
            Username        = userName,
            ReservationFrom = fromDateTime,
            ReservationTo   = fromDateTime.AddMinutes(10)
        };
        _allReservations.Add(add);
        return add;
    }
    public static IEnumerable<Reservation> GetAllReservations()
    {
        return _allReservations;
    }

    private static IList<Reservation> _allReservations = new List<Reservation>()
    {
        new Reservation() { Id = 1, Username = "Matteo", DeviceId = 1, ReservationFrom = new DateTime(2024, 6, 11, 8,  0,  0), ReservationTo = new DateTime(2024, 6, 11, 8,  10, 0) },
        new Reservation() { Id = 2, Username = "Noah", DeviceId   = 1, ReservationFrom = new DateTime(2024, 6, 11, 8,  10, 0), ReservationTo = new DateTime(2024, 6, 11, 8,  20, 0) },
        new Reservation() { Id = 3, Username = "Matteo", DeviceId   = 1, ReservationFrom = new DateTime(2024, 6, 11, 9,  10, 0), ReservationTo = new DateTime(2024, 6, 11, 9,  20, 0) },
        new Reservation() { Id = 4, Username = "Leon", DeviceId   = 2, ReservationFrom = new DateTime(2024, 6, 11, 8,  0,  0), ReservationTo = new DateTime(2024, 6, 12, 10, 0,  0) },
        new Reservation() { Id = 5, Username = "Finn", DeviceId   = 2, ReservationFrom = new DateTime(2024, 6, 11, 11, 0,  0), ReservationTo = new DateTime(2024, 6, 12, 12, 0,  0) },
        new Reservation() { Id = 6, Username = "Leon", DeviceId   = 3, ReservationFrom = new DateTime(2024, 6, 11, 8, 10, 0), ReservationTo = new DateTime(2024, 6, 12, 8, 20, 0) },
    };
}