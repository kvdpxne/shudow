using Microsoft.Win32;
using Shudow.Shared;

namespace Shudow.Spoofers;

internal class SoftwareCryptography : ISpoofer {

  public string Name => "SoftwareCryptography";

  public object Value {
    get {
      using var key = Registries.GetLocalMachineKey(Registries.SoftwareCryptography);
      return key?.GetValue("MachineGuid");
    }
    set {
      using var key = Registries.GetLocalMachineKey(Registries.SoftwareCryptography, true);
      key?.SetValue("MachineGuid", value, RegistryValueKind.String);
    }
  }
}