using System.Collections.Generic;
using Shudow.Shared;

namespace Shudow.Spoofers {

  internal class WindowsUpdateDeviceIdentifier : ISpoofer {

    public string Name => "WindowsUpdateDeviceIdentifier";

    public object Value {
      get {
        var value = new Dictionary<string, object>(2);

        using (var key = Registries.GetSoftwareWindowsUpdate()) {
          value["SusClientId"] = key.GetValue("SusClientId");
          value["SusClientIdValidation"] = key.GetValue("SusClientIdValidation");
        }

        return value;
      }
      set {
        if (!(value is Dictionary<string, object> keyValue)) {
          return;
        }

        if (keyValue.TryGetValue("SusClientId", out var susClientIdentifier)) {
          using (var key = Registries.GetSoftwareWindowsUpdate(true)) {
            key.SetValue("SusClientId", susClientIdentifier);
          }
        }

        if (keyValue.TryGetValue("SusClientIdValidation", out var susClientIdentifierValidation)) {
          using (var key = Registries.GetSoftwareWindowsUpdate(true)) {
            key.SetValue("SusClientIdValidation", susClientIdentifierValidation);
          }
        }
      }
    }
  }

}