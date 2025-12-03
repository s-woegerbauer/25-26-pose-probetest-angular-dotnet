namespace Logic.Abstraction;

using System.Collections.Generic;
using System.Threading.Tasks;

public interface IReservationService
{
    Task<IList<Reservation>> GetAllReservationsAsync();
    Task<IList<Reservation>> GetReservationsForInputDeviceAsync(int inputDeviceId);
    Task<Reservation?>       GetByIdAsync(int                       id);

    Task<Reservation?> AddAsync(int inputDeviceId, string userName, DateTime fromTime);
}