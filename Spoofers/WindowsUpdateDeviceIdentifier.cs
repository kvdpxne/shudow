using System.Text;
using Microsoft.Win32;
using Shudow.Shared;

namespace Shudow.Spoofers;

internal class WindowsUpdateDeviceIdentifier : ISpoofer {

  public string Name => "WindowsUpdateDeviceIdentifier";

  public object Value {
    get {
      var value = new Dictionary<string, object>(2);

      using var key = Registries.GetSoftwareWindowsUpdate();
      value[Registries.SusClientIdentifier] = key.GetValue(Registries.SusClientIdentifier);

      var bytes = key.GetValue(Registries.SusClientIdentifierValidation) as byte[];
      value[Registries.SusClientIdentifierValidation] = Encoding.UTF8.GetString(bytes);

      return value;
    }
    set {
      if (value is not Dictionary<string, object> keyValue) {
        return;
      }

      if (keyValue.TryGetValue(Registries.SusClientIdentifier, out var susClientIdentifier)) {
        using var key = Registries.GetSoftwareWindowsUpdate(true);
        key.SetValue(Registries.SusClientIdentifier, susClientIdentifier, RegistryValueKind.String);
      }

      if (keyValue.TryGetValue(Registries.SusClientIdentifierValidation, out var susClientIdentifierValidation)) {
        using var key = Registries.GetSoftwareWindowsUpdate(true);
        key.SetValue(
          Registries.SusClientIdentifierValidation,
          Encoding.UTF8.GetBytes($"{susClientIdentifierValidation}"),
          RegistryValueKind.Binary
        );
      }
    }
  }
}