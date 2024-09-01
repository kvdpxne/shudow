using Microsoft.Win32;
using System;
using System.Security;

namespace Shudow.Shared {

  internal static class Registries {

    public const string COMPUTER_NAME = "SYSTEM\\CurrentControlSet\\Control\\ComputerName\\ComputerName";
    public const string ACTIVE_COMPUTER_NAME =
      "SYSTEM\\CurrentControlSet\\Control\\ComputerName\\ActiveComputerName";
    public const string LAST_COMPUTER_NAME = "SYSTEM\\CurrentControlSet\\Services\\EventLog\\State";
    public const string TCP_IP_PARAMETERS = "SYSTEM\\CurrentControlSet\\Services\\Tcpip\\Parameters";
    public const string COMPUTER_HW_ID_CONFIG =
      "SYSTEM\\CurrentControlSet\\Control\\IDConfigDB\\Hardware Profiles\\0001";
    public const string SYSTEM_INFORMATION = "SYSTEM\\CurrentControlSet\\Control\\SystemInformation";

    public const string SOFTWARE_WINDOWS_UPDATE = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\WindowsUpdate";
    public const string SOFTWARE_WINDOWS_CURRENT_VERSION = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion";

    public static RegistryKey GetLocalMachineKey(
      string name,
      bool writable = false
    ) {
      try {
        return Registry.LocalMachine.OpenSubKey(name, writable);
      }
      catch (SecurityException exception) {
        Console.Error.WriteLine($"No authority to modify the register: {exception.Message}");
        return Registry.LocalMachine.OpenSubKey(name, false);
      }
    }

    public static RegistryKey GetComputerName(bool writable = false) {
      return GetLocalMachineKey(COMPUTER_NAME, writable);
    }

    public static RegistryKey GetComputerActiveName(bool writable = false) {
      return GetLocalMachineKey(ACTIVE_COMPUTER_NAME, writable);
    }

    public static RegistryKey GetComputerHostname(bool writable = false) {
      return GetLocalMachineKey(TCP_IP_PARAMETERS, writable);
    }

    public static RegistryKey GetComputerHardwareConfigurationIdentifier(bool writable = false) {
      return GetLocalMachineKey(COMPUTER_HW_ID_CONFIG, writable);
    }

    public static RegistryKey GetSystemInformation(bool writable = false) {
      return GetLocalMachineKey(SYSTEM_INFORMATION, writable);
    }

    public static RegistryKey GetSoftwareWindowsUpdate(bool writable = false) {
      return GetLocalMachineKey(SOFTWARE_WINDOWS_UPDATE, writable);
    }

    public static RegistryKey GetSoftwareWindowsCurrentVersion(bool writable = false) {
      return GetLocalMachineKey(SOFTWARE_WINDOWS_CURRENT_VERSION, writable);
    }
  }

}