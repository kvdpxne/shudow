﻿using Microsoft.Win32;
using Shudow.Shared;

namespace Shudow.Spoofers;

internal class SystemBiosVersion : ISpoofer {

  public string Name => "SystemBIOSVersion";

  public object Value {
    get {
      using var key = Registries.GetSystemInformation();
      return key.GetValue(Registries.BiosVersion);
    }
    set {
      using var key = Registries.GetSystemInformation(true);
      key.SetValue(Registries.BiosVersion, value, RegistryValueKind.String);
    }
  }
}