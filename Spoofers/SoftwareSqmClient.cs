using Microsoft.Win32;
using Shudow.Shared;

namespace Shudow.Spoofers;

internal class SoftwareSqmClient : ISpoofer {

  public string Name => "SoftwareSqmClient";

  public object Value {
    get {
      using var key = Registries.GetLocalMachineKey(Registries.SoftwareSqmClient);
      return key?.GetValue("MachineId");
    }
    set {
      using var key = Registries.GetLocalMachineKey(Registries.SoftwareSqmClient, true);
      key?.SetValue("MachineId", value, RegistryValueKind.String);
    }
  }
}