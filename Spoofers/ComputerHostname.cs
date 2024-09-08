using System.Collections.Generic;
using Microsoft.Win32;
using Shudow.Shared;

namespace Shudow.Spoofers {

  internal class ComputerHostname : ISpoofer {

    public string Name => "ComputerHostname";

    public object Value {
      get {
        var value = new Dictionary<string, object>(2);

        using (var key = Registries.GetComputerHostname()) {
          value[Registries.ComputerHostname] = key.GetValue(Registries.ComputerHostname);
        }

        using (var key = Registries.GetComputerHostname()) {
          value[Registries.ComputerNvHostname] = key.GetValue(Registries.ComputerNvHostname);
        }

        return value;
      }
      set {
        if (!(value is Dictionary<string, object> keyValue)) {
          return;
        }

        if (keyValue.TryGetValue(Registries.ComputerHostname, out var hostname)) {
          using (var key = Registries.GetComputerHostname(true)) {
            key.SetValue(Registries.ComputerHostname, hostname, RegistryValueKind.String);
          }
        }

        if (keyValue.TryGetValue(Registries.ComputerNvHostname, out var nvHostname)) {
          using (var key = Registries.GetComputerHostname(true)) {
            key.SetValue(Registries.ComputerNvHostname, nvHostname, RegistryValueKind.String);
          }
        }
      }
    }
  }
}