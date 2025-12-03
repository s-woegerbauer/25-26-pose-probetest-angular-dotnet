namespace Logic.Abstraction;

using System.Collections.Generic;
using System.Threading.Tasks;

public interface IInputDeviceService
{
    Task<IList<InputDevice>> GetAllInputDevicesAsync();

    Task<InputDevice?> GetInputDeviceAsync(int id);
}