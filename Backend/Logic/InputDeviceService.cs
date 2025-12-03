namespace Logic;

using Logic.Abstraction;

public class InputDeviceService : IInputDeviceService
{
    public async Task<IList<InputDevice>> GetAllInputDevicesAsync()
    {
        await Task.CompletedTask;

        foreach (var inputDevice in _allInputDevices)
        {
            inputDevice.Reservations = ReservationService
                .GetAllReservations().Where(r => r.DeviceId == inputDevice.Id)
                .OrderBy(r => TimeOnly.FromDateTime(r.ReservationFrom));
        }

        return _allInputDevices;
    }

    public async Task<InputDevice?> GetInputDeviceAsync(int id)
    {
        await Task.CompletedTask;
        return _allInputDevices.FirstOrDefault(d => d.Id == id);
    }

    private static InputDevice[] _allInputDevices = new[]
    {
        new InputDevice() { Id = 1, Name = "GameMad", Description = "GameMad", Category = "Input" },
        new InputDevice() { Id = 2, Name = "Wheel", Description = "DirectX Wheel", Category = "Input" },
        new InputDevice() { Id = 3, Name = "Gyro", Description = "Esp8266 Gyro", Category = "Input" },
        new InputDevice() { Id = 4, Name = "Force", Description = "Esp8266 HX771", Category = "Input" },
        new InputDevice() { Id = 5, Name = "Sonic", Description = "Esp8266 Sonic", Category = "Input" }
    };
}