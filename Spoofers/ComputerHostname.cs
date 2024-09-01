using System.Collections.Generic;
using Shudow.Shared;

namespace Shudow.Spoofers {

  internal class ComputerHostname : ISpoofer {

    public string Name => "ComputerHostname";

    public object Value {
      get {
        var value = new Dictionary<string, object>(2);

        using (var key = Registries.GetComputerHostname()) {
          value["Hostname"] = key.GetValue("Hostname");
        }

        using (var key = Registries.GetComputerHostname()) {
          value["NV Hostname"] = key.GetValue("NV Hostname");
        }

        return value;
      }
      set {
        if (!(value is Dictionary<string, object> keyValue)) {
          return;
        }

        if (keyValue.TryGetValue("Hostname", out var hostname)) {
          using (var key = Registries.GetComputerHostname(true)) {
            key.SetValue("Hostname", hostname);
          }
        }

        if (keyValue.TryGetValue("NV Hostname", out var nvHostname)) {
          using (var key = Registries.GetComputerHostname(true)) {
            key.SetValue("NV Hostname", nvHostname);
          }
        }
      }
    }
  }

}