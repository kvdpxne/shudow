using System.Collections.Generic;
using Microsoft.Win32;
using Shudow.Shared;

namespace Shudow.Spoofers {

  internal class WindowsInstallationDate : ISpoofer {

    public string Name => "WindowsInstallationDate";

    public object Value {
      get {
        var value = new Dictionary<string, object>(2);

        using (var key = Registries.GetSoftwareWindowsCurrentVersion()) {
          value[Registries.WindowsInstallDate] = key.GetValue(Registries.WindowsInstallDate);
          value[Registries.WindowsInstallTime] = key.GetValue(Registries.WindowsInstallTime);
        }

        return value;
      }
      set {
        if (!(value is Dictionary<string, object> keyValue)) {
          return;
        }

        if (keyValue.TryGetValue(Registries.WindowsInstallDate, out var installDate)) {
          using (var key = Registries.GetSoftwareWindowsCurrentVersion(true)) {
            key.SetValue(Registries.WindowsInstallDate, installDate, RegistryValueKind.DWord);
          }
        }

        if (keyValue.TryGetValue(Registries.WindowsInstallTime, out var installTime)) {
          using (var key = Registries.GetSoftwareWindowsCurrentVersion(true)) {
            key.SetValue(Registries.WindowsInstallTime, installTime, RegistryValueKind.QWord);
          }
        }
      }
    }
  }
}