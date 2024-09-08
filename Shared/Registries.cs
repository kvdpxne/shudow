using Microsoft.Win32;
using System;
using System.Security;

namespace Shudow.Shared {

  internal static class Registries {

    // System
    private const string ComputerNameKey = "SYSTEM\\CurrentControlSet\\Control\\ComputerName\\ComputerName";
    private const string ActiveComputerNameKey = "SYSTEM\\CurrentControlSet\\Control\\ComputerName\\ActiveComputerName";
    private const string TcpIpParametersKey = "SYSTEM\\CurrentControlSet\\Services\\Tcpip\\Parameters";
    private const string HardwareConfigurationIdentifierKey = "SYSTEM\\CurrentControlSet\\Control\\IDConfigDB\\Hardware Profiles\\0001";
    private const string SystemInformationKey = "SYSTEM\\CurrentControlSet\\Control\\SystemInformation";

    // Software
    private const string WindowsUpdateKey = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\WindowsUpdate";
    private const string WindowsCurrentVersionKey = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion";

    // Hardware
    private const string CentralProcessorKey = "HARDWARE\\DESCRIPTION\\System\\CentralProcessor";

    public const string ComputerName = "ComputerName";
    public const string ComputerHardwareIdentifier = "ComputerHardwareId";
    public const string ComputerHardwareIdentifiers = "ComputerHardwareIds";
    public const string ComputerHostname = "Hostname";
    public const string ComputerNvHostname = "NV Hostname";
    public const string HardwareProfileIdentifier = "HwProfileGuid";
    public const string BiosReleaseDate = "BIOSReleaseDate";
    public const string BiosVersion = "BIOSVersion";
    public const string SystemManufacturer = "SystemManufacturer";
    public const string SystemProductName = "SystemProductName";
    public const string WindowsInstallDate = "InstallDate";
    public const string WindowsInstallTime = "InstallTime";
    public const string SusClientIdentifier = "SusClientId";
    public const string SusClientIdentifierValidation = "SusClientIdValidation";

    public const string CpuName = "ProcessorNameString";

    private static RegistryKey GetLocalMachineKey(
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
      return GetLocalMachineKey(ComputerNameKey, writable);
    }

    public static RegistryKey GetComputerActiveName(bool writable = false) {
      return GetLocalMachineKey(ActiveComputerNameKey, writable);
    }

    public static RegistryKey GetComputerHostname(bool writable = false) {
      return GetLocalMachineKey(TcpIpParametersKey, writable);
    }

    public static RegistryKey GetComputerHardwareConfigurationIdentifier(bool writable = false) {
      return GetLocalMachineKey(HardwareConfigurationIdentifierKey, writable);
    }

    public static RegistryKey GetSystemInformation(bool writable = false) {
      return GetLocalMachineKey(SystemInformationKey, writable);
    }

    public static RegistryKey GetSoftwareWindowsUpdate(bool writable = false) {
      return GetLocalMachineKey(WindowsUpdateKey, writable);
    }

    public static RegistryKey GetSoftwareWindowsCurrentVersion(bool writable = false) {
      return GetLocalMachineKey(WindowsCurrentVersionKey, writable);
    }

    public static RegistryKey GetCentralProcessor(bool writable = false) {
      return GetLocalMachineKey(CentralProcessorKey, writable);
    }
  }
}