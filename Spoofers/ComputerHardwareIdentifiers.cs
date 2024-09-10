using Microsoft.Win32;
using Shudow.Shared;

namespace Shudow.Spoofers;

internal class ComputerHardwareIdentifiers : ISpoofer {

  public string Name => "ComputerHardwareIdentifiers";

  public object Value {
    get {
      var value = new Dictionary<string, object>(2);

      using (var key = Registries.GetSystemInformation()) {
        value[Registries.ComputerHardwareIdentifier] = key.GetValue(Registries.ComputerHardwareIdentifier);
      }

      using (var key = Registries.GetSystemInformation()) {
        value[Registries.ComputerHardwareIdentifiers] = key.GetValue(Registries.ComputerHardwareIdentifiers);
      }

      return value;
    }
    set {
      if (value is not Dictionary<string, object> keyValue) {
        return;
      }

      if (keyValue.TryGetValue(Registries.ComputerHardwareIdentifier, out var computerHardwareId)) {
        using var key = Registries.GetSystemInformation(true);
        key.SetValue(Registries.ComputerHardwareIdentifier, computerHardwareId, RegistryValueKind.String);
      }

      if (keyValue.TryGetValue(Registries.ComputerHardwareIdentifiers, out var computerHardwareIds)) {
        using var key = Registries.GetSystemInformation(true);
        key.SetValue(Registries.ComputerHardwareIdentifiers, computerHardwareIds, RegistryValueKind.MultiString);
      }
    }
  }
}