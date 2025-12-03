namespace WebAPI.Controllers;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using Base.Web.Controller;

using Logic.Abstraction;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Reservation Controller.
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Guest")]
public class ReservationController : Controller
{
    private readonly IReservationService _reservationService;

    /// <summary>
    /// Constructor of Reservation Controller.
    /// </summary>
    /// <param name="reservationService"></param>
    public ReservationController(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    #region Dto

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="UserName"></param>
    /// <param name="DeviceId"></param>
    /// <param name="ReservationFrom"></param>
    /// <param name="ReservationTo"></param>
    public record ReservationDto(
        int      Id,
        string   UserName,
        int      DeviceId,
        DateTime ReservationFrom,
        DateTime ReservationTo
    );

    Reservation ToEntity(ReservationDto dto)
    {
        return new Reservation()
        {
            Id              = dto.Id,
            Username        = dto.UserName,
            DeviceId        = dto.DeviceId,
            ReservationFrom = dto.ReservationFrom,
            ReservationTo   = dto.ReservationTo,
        };
    }

    ReservationDto? ToDto(Reservation? entity)
    {
        if (entity is null)
        {
            return null;
        }

        return new ReservationDto(
            entity.Id,
            entity.Username,
            entity.DeviceId,
            entity.ReservationFrom,
            entity.ReservationTo
        );
    }

    IList<ReservationDto>? ToDto(IEnumerable<Reservation>? list)
    {
        if (list is null)
        {
            return null;
        }

        return list.Select(x => ToDto(x)!).ToList();
    }

    /// <summary>
    /// Create a new reservation
    /// </summary>
    /// <param name="inputDeviceId"></param>
    /// <param name="reservationFrom"></param>
    public record CreateReservationDto(
        int      inputDeviceId,
        DateTime reservationFrom);

    #endregion

    #region Default REST methods

    /// <summary>
    /// Get customer Reservations.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReservationDto>>> GetAllAsync(int? inputDeviceId)
    {
        var currentUser = HttpContext.User;
        var userName    = currentUser.Identity?.Name;

        IList<Reservation> reservations;

        if (inputDeviceId is not null)
        {
            reservations = await _reservationService.GetReservationsForInputDeviceAsync(inputDeviceId.Value);
        }
        else
        {
            reservations = await _reservationService.GetAllReservationsAsync();
        }

        return Ok(ToDto(reservations));
    }

    /// <summary>
    /// Add a new reservation.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<ReservationDto>> AddAsync([FromBody] CreateReservationDto value)
    {
        var currentUser = HttpContext.User;
        var userName    = currentUser.FindFirstValue("Name");

        if (string.IsNullOrEmpty(userName))
        {
            return BadRequest("No user name found in token.");
        }

        var entity      = await _reservationService.AddAsync(value.inputDeviceId, userName, value.reservationFrom);

        if (entity is null)
        {
            return BadRequest("Cannot create reservation");
        }

        var newId  = entity.Id;
        var newUri = this.GetCurrentUri() + "/" + newId;
        return Created(newUri, ToDto(await _reservationService.GetByIdAsync(newId)));
    }

    #endregion
}