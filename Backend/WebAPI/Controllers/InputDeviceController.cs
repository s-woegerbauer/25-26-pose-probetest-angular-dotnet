namespace WebAPI.Controllers;

using Base.Web.Controller;

using Logic.Abstraction;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// InputDevice Controller.
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class InputDeviceController : Controller
{
    private readonly IInputDeviceService _inputDeviceService;

    /// <summary>
    /// Constructor of InputDevice Controller.
    /// </summary>
    /// <param name="inputDeviceService"></param>
    public InputDeviceController(IInputDeviceService inputDeviceService)
    {
        _inputDeviceService = inputDeviceService;
    }

    #region Dto

    /// <summary>
    /// InputDevice Dto
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="Name"></param>
    /// <param name="Description"></param>
    /// <param name="Category"></param>
    /// <param name="Reservations"></param>
    public record InputDeviceDto(
        int                          Id,
        string                       Name,
        string                       Description,
        string                       Category,
        IEnumerable<ReservationDateDto>? Reservations);

    /// <summary>
    /// Short version of Reservation
    /// </summary>
    /// <param name="FromTime"></param>
    /// <param name="ToTime"></param>
    public record ReservationDateDto(
        DateTime FromTime,
        DateTime ToTime,
        string UserName
    );

    InputDevice ToEntity(InputDeviceDto dto)
    {
        return new InputDevice()
        {
            Id          = dto.Id,
            Name        = dto.Name,
            Description = dto.Description,
            Category    = dto.Category
        };
    }

    InputDeviceDto? ToDto(InputDevice? entity)
    {
        if (entity is null)
        {
            return null;
        }

        return new InputDeviceDto(entity.Id,
            entity.Name,
            entity.Description,
            entity.Category,
            ToDto(entity.Reservations)
        );
    }

    IList<InputDeviceDto>? ToDto(IList<InputDevice>? list)
    {
        if (list is null)
        {
            return null;
        }

        return list.Select(x => ToDto(x)!).ToList();
    }

    ReservationDateDto? ToDto(Reservation? entity)
    {
        if (entity is null)
        {
            return null;
        }

        return new ReservationDateDto(
            entity.ReservationFrom,
            entity.ReservationTo,
            entity.Username
        );
    }

    IList<ReservationDateDto>? ToDto(IEnumerable<Reservation>? list)
    {
        if (list is null)
        {
            return null;
        }

        return list.Select(x => ToDto(x)!).ToList();
    }

    #endregion

    #region Default REST methods

    /// <summary>
    /// This endpoint is for all authenticated users.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<InputDeviceDto>>> GetAsync()
    {
        var inputDevices = await _inputDeviceService.GetAllInputDevicesAsync();
        return await this.NotFoundOrOk(ToDto(inputDevices));
    }

    /// <summary>
    /// This endpoint is for all authenticated users.
    /// </summary>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<InputDeviceDto>> GetByIdAsync(int id)
    {
        var inputDevices = await _inputDeviceService.GetInputDeviceAsync(id);
        return await this.NotFoundOrOk(ToDto(inputDevices));
    }

    #endregion
}