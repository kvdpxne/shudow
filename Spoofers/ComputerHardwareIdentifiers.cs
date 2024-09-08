using System.Collections.Generic;
using Microsoft.Win32;
using Shudow.Shared;

namespace Shudow.Spoofers {

  internal class ComputerHardwareIdentifiers : ISpoofer {

    public string Name => "ComputerHardwareIdentifiers";

    public object Value {
      get {
        var value = new Dictionary<string, object>(2);

        using (var key = Registries.GetSystemInformation()) {
          value["ComputerHardwareId"] = key.GetValue("ComputerHardwareId");
        }

        using (var key = Registries.GetSystemInformation()) {
          value["ComputerHardwareIds"] = key.GetValue("ComputerHardwareIds");
        }

        return value;
      }
      set {
        if (!(value is Dictionary<string, object> keyValue)) {
          return;
        }

        if (keyValue.TryGetValue("ComputerHardwareId", out var computerHardwareId)) {
          using (var key = Registries.GetSystemInformation(true)) {
            key.SetValue("ComputerHardwareId", computerHardwareId, RegistryValueKind.String);
          }
        }

        if (keyValue.TryGetValue("ComputerHardwareIds", out var computerHardwareIds)) {
          using (var key = Registries.GetSystemInformation(true)) {
            key.SetValue("ComputerHardwareIds", computerHardwareIds, RegistryValueKind.MultiString);
          }
        }
      }
    }
  }

}