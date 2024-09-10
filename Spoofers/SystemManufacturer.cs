using Microsoft.Win32;
using Shudow.Shared;

namespace Shudow.Spoofers;

internal class SystemManufacturer : ISpoofer {

  public string Name => "SystemManufacturer";

  public object Value {
    get {
      using var key = Registries.GetSystemInformation();
      return key.GetValue(Registries.SystemManufacturer);
    }
    set {
      using var key = Registries.GetSystemInformation(true);
      key.SetValue(Registries.SystemManufacturer, value, RegistryValueKind.String);
    }
  }
}