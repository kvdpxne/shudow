using Shudow.Shared;

namespace Shudow.Spoofers {

  internal class ComputerHardwareConfigurationIdentifier : ISpoofer {

    public string Name => "ComputerHardwareConfigurationIdentifier";

    public object Value {
      get {
        using (var key = Registries.GetComputerHardwareConfigurationIdentifier()) {
          return key.GetValue("HwProfileGuid");
        }
      }
      set {
        using (var key = Registries.GetComputerHardwareConfigurationIdentifier(true)) {
          key.SetValue("HwProfileGuid", value);
        }
      }
    }
  }

}