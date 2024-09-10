using Microsoft.Win32;
using Shudow.Shared;

namespace Shudow.Spoofers;

internal class ComputerName : ISpoofer {

  public string Name => "ComputerName";

  public object Value {
    get {
      using var key = Registries.GetComputerName();
      return key.GetValue(Registries.ComputerName);
    }
    set {
      using var key = Registries.GetComputerName(true);
      key.SetValue(Registries.ComputerName, value, RegistryValueKind.String);
    }
  }
}