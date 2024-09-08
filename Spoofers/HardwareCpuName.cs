using System;
using System.Collections.Generic;
using Shudow.Shared;

namespace Shudow.Spoofers {

  internal class HardwareCpuName : ISpoofer {

    public string Name => "HardwareCPUName";

    public object Value {
      get {
        var count = Environment.ProcessorCount;
        var value = new Dictionary<string, object>(count);

        for (var i = 0; count > i; ++i) {
          var keyName = i.ToString();

          using (var key = Registries.GetCentralProcessor().OpenSubKey(keyName)) {
            if (null == key) {
              continue;
            }

            value[keyName] = key.GetValue(Registries.CpuName);
          }
        }

        return value;
      }
      set {
        if (!(value is Dictionary<string, object> keyValue)) {
          return;
        }

        for (var i = 0; Environment.ProcessorCount > i; ++i) {
          var keyName = i.ToString();

          if (!keyValue.TryGetValue(keyName, out var cpuName)) {
            continue;
          }

          using (var key = Registries.GetCentralProcessor().OpenSubKey(keyName, true)) {
            if (null == key) {
              continue;
            }

            key.SetValue(Registries.CpuName, cpuName);
          }
        }
      }
    }
  }
}