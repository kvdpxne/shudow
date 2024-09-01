using Shudow.Shared;

namespace Shudow.Spoofers {

  internal class SystemBiosVersion : ISpoofer {

    public string Name => "SystemBIOSVersion";

    public object Value {
      get {
        using (var key = Registries.GetSystemInformation()) {
          return key.GetValue("BIOSVersion");
        }
      }
      set {
        using (var key = Registries.GetSystemInformation(true)) {
          key.SetValue("BIOSVersion", value);
        }
      }
    }
  }

}