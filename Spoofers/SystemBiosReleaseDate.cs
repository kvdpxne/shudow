using Shudow.Shared;

namespace Shudow.Spoofers {

  internal class SystemBiosReleaseDate : ISpoofer {

    public string Name => "SystemBIOSReleaseDate";

    public object Value {
      get {
        using (var key = Registries.GetSystemInformation()) {
          return key.GetValue("BIOSReleaseDate");
        }
      }
      set {
        using (var key = Registries.GetSystemInformation(true)) {
          key.SetValue("BIOSReleaseDate", value);
        }
      }
    }
  }

}