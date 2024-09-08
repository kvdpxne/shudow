using Microsoft.Win32;
using Shudow.Shared;

namespace Shudow.Spoofers {

  internal class ComputerActiveName : ISpoofer {

    public string Name => "ComputerActiveName";

    public object Value {
      get {
        using (var key = Registries.GetComputerActiveName()) {
          return key.GetValue("ComputerName");
        }
      }
      set {
        using (var key = Registries.GetComputerActiveName(true)) {
          key.SetValue("ComputerName", value, RegistryValueKind.String);
        }
      }
    }
  }

}