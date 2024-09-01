﻿using System.Collections.Generic;
using Shudow.Shared;

namespace Shudow.Spoofers {

  internal class WindowsInstallationDate : ISpoofer {

    public string Name => "WindowsInstallationDate";

    public object Value {
      get {
        var value = new Dictionary<string, object>(2);

        using (var key = Registries.GetSoftwareWindowsCurrentVersion()) {
          value["InstallDate"] = key.GetValue("InstallDate");
          value["InstallTime"] = key.GetValue("InstallTime");
        }

        return value;
      }
      set {
        if (!(value is Dictionary<string, object> keyValue)) {
          return;
        }

        if (keyValue.TryGetValue("InstallDate", out var installDate)) {
          using (var key = Registries.GetSoftwareWindowsCurrentVersion()) {
            key.SetValue("InstallDate", installDate);
          }
        }

        if (keyValue.TryGetValue("InstallTime", out var installTime)) {
          using (var key = Registries.GetSoftwareWindowsCurrentVersion()) {
            key.SetValue("InstallTime", installTime);
          }
        }
      }
    }
  }

}