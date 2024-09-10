using Microsoft.Win32;
using Shudow.Shared;

namespace Shudow.Spoofers;

internal class ComputerHardwareConfigurationIdentifier : ISpoofer {

  public string Name => "ComputerHardwareConfigurationIdentifier";

  public object Value {
    get {
      using var key = Registries.GetComputerHardwareConfigurationIdentifier();
      return key.GetValue(Registries.HardwareProfileIdentifier);
    }
    set {
      using var key = Registries.GetComputerHardwareConfigurationIdentifier(true);
      key.SetValue(Registries.HardwareProfileIdentifier, value, RegistryValueKind.String);
    }
  }
}