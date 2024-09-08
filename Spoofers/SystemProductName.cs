using Microsoft.Win32;
using Shudow.Shared;

namespace Shudow.Spoofers {

  internal class SystemProductName : ISpoofer {

    public string Name => "SystemProductName";

    public object Value {
      get {
        using (var key = Registries.GetSystemInformation()) {
          return key.GetValue("SystemProductName");
        }
      }
      set {
        using (var key = Registries.GetSystemInformation(true)) {
          key.SetValue("SystemProductName", value, RegistryValueKind.String);
        }
      }
    }
  }

}