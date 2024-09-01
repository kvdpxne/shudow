using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.Json;
using Shudow.Shared;
using Shudow.Spoofers;

namespace Shudow {

  public partial class Form1 : Form {

    private readonly ISpoofer _computerNameSpoofer;
    private readonly ISpoofer _computerActiveNameSpoofer;
    private readonly ISpoofer _computerHostnameSpoofer;
    private readonly ISpoofer _computerHardwareConfigurationIdentifierSpoofer;
    private readonly ISpoofer _computerHardwareIdentifiersSpoofer;
    private readonly ISpoofer _systemManufacturerSpoofer;
    private readonly ISpoofer _systemProductNameSpoofer;
    private readonly ISpoofer _systemBiosReleaseDateSpoofer;
    private readonly ISpoofer _systemBiosVersionSpoofer;
    private readonly ISpoofer _windowsUpdateDeviceIdentifierSpoofer;
    private readonly ISpoofer _windowsInstallationDateSpoofer;

    public Form1() {
      InitializeComponent();

      _computerNameSpoofer = new ComputerName();
      _computerActiveNameSpoofer = new ComputerActiveName();
      _computerHostnameSpoofer = new ComputerHostname();
      _computerHardwareConfigurationIdentifierSpoofer = new ComputerHardwareConfigurationIdentifier();
      _computerHardwareIdentifiersSpoofer = new ComputerHardwareIdentifiers();
      _systemManufacturerSpoofer = new SystemManufacturer();
      _systemProductNameSpoofer = new SystemProductName();
      _systemBiosReleaseDateSpoofer = new SystemBiosReleaseDate();
      _systemBiosVersionSpoofer = new SystemBiosVersion();
      _windowsUpdateDeviceIdentifierSpoofer = new WindowsUpdateDeviceIdentifier();
      _windowsInstallationDateSpoofer = new WindowsInstallationDate();
    }

    private void HandleChangeButtonClick(object sender, EventArgs e) {
      var randomComputerName = Randoms.GenerateRandomAlphanumericText(5);

      if (changeComputerName.Checked) {
        _computerNameSpoofer.Value = randomComputerName;
      }

      if (changeComputerActiveName.Checked) {
        _computerActiveNameSpoofer.Value = randomComputerName;
      }

      // if (changeComputerLastName.Checked)
      // {
      //     ComputerLastName.Value = randomComputerName;
      // }

      if (changeComputerHostname.Checked) {
        _computerHostnameSpoofer.Value = randomComputerName;
      }

      if (changeComputerHardwareConfigurationIdentifier.Checked) {
        _computerHardwareConfigurationIdentifierSpoofer.Value = Guid.NewGuid();
      }

      if (changeComputerHardwareIdentifiers.Checked) {
        _computerHardwareIdentifiersSpoofer.Value = new Dictionary<string, object> {
          { "ComputerHardwareId", Guid.NewGuid() },
          { "ComputerHardwareIds", Enumerable.Repeat(Guid.NewGuid(), 10).ToString() }
        };
      }

      if (changeSystemManufacturer.Checked) {
        var first = Randoms.GenerateRandomNumber(3, 11);
        var second = Randoms.GenerateRandomNumber(2, first);
        var third = Randoms.GenerateRandomNumber(5, 10);

        var name = "";
        name += Randoms.GenerateRandomAlphanumericText(first, third);
        name += $" {Randoms.GenerateRandomAlphanumericText(second, 10)}";
        name += $" {Randoms.GenerateRandomAlphanumericText(3, third)}";

        _systemManufacturerSpoofer.Value = name;
      }

      if (changeSystemProductName.Checked) {
        var model = Randoms.GenerateRandomAlphanumericText(3, 6);
        var type = Randoms.GenerateRandomAlphanumericText(2, 7);

        _systemProductNameSpoofer.Value = $"{model}-{type}";
      }

      // if (changeSystemProductVersion.Checked)
      // {
      //
      // }

      if (changeSystemBiosReleaseDate.Checked) {
        var now = DateTime.Now;
        var lastYear = new DateTime(now.Year - 1);

        var date = Randoms.GenerateRandomDateTime(
          new DateTime(1999, 1, 1),
          lastYear
        );

        _systemBiosReleaseDateSpoofer.Value =
          date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
      }

      if (changeSystemBiosVersion.Checked) {
        _systemBiosVersionSpoofer.Value = Randoms.GenerateRandomNumber(10, 10000);
      }

      if (changeWindowsUpdateDeviceIdentifier.Checked) {
        _windowsUpdateDeviceIdentifierSpoofer.Value = new Dictionary<string, object> {
          { "SusClientId", Guid.NewGuid() }, {
            "SusClientIdValidation", Encoding.UTF8.GetBytes(
              Randoms.GenerateRandomAlphanumericText(25, 25)
            )
          }
        };
      }

      if (changeWindowsInstallationDate.Checked) {
        var now = DateTime.Now;
        var lastYear = new DateTime(now.Year - 1);

        _windowsInstallationDateSpoofer.Value = new Dictionary<string, object> {
          {
            "InstallDate",
            Randoms.GenerateRandomUnsignedInt(
              915145200,
              (uint)lastYear.Second
            )
          }, {
            "InstallTime",
            Randoms.GenerateRandomUnsignedLong(
              915145200000,
              (ulong)lastYear.Millisecond
            )
          }
        };
      }
    }

    private void button4_Click(object sender, EventArgs e) {
      changeComputerName.Checked = true;
      changeComputerActiveName.Checked = true;
      // changeComputerLastName.Checked = true;
      changeComputerHostname.Checked = true;
      changeComputerHardwareConfigurationIdentifier.Checked = true;
      changeComputerHardwareIdentifiers.Checked = true;
      changeSystemManufacturer.Checked = true;
      changeSystemProductName.Checked = true;
      // changeSystemProductVersion.Checked = true;
      changeSystemBiosReleaseDate.Checked = true;
      changeSystemBiosVersion.Checked = true;
      // changeSystemBiosVendor.Checked = true;
      changeWindowsUpdateDeviceIdentifier.Checked = true;
      changeWindowsInstallationDate.Checked = true;

      //
      //HandleChangeButtonClick(sender, e);
    }

    private void HandleBackupButtonClick(object sender, EventArgs e) {
      var data = new Dictionary<string, object> {
        { _computerNameSpoofer.Name, _computerNameSpoofer.Value },
        { _computerActiveNameSpoofer.Name, _computerActiveNameSpoofer.Value },
        { _computerHostnameSpoofer.Name, _computerHostnameSpoofer.Value }, {
          _computerHardwareConfigurationIdentifierSpoofer.Name,
          _computerHardwareConfigurationIdentifierSpoofer.Value
        },
        { _computerHardwareIdentifiersSpoofer.Name, _computerHardwareIdentifiersSpoofer.Value },
        { _systemManufacturerSpoofer.Name, _systemManufacturerSpoofer.Value },
        { _systemProductNameSpoofer.Name, _systemProductNameSpoofer.Value },
        { _systemBiosReleaseDateSpoofer.Name, _systemBiosReleaseDateSpoofer.Value },
        { _systemBiosVersionSpoofer.Name, _systemBiosVersionSpoofer.Value },
        { _windowsUpdateDeviceIdentifierSpoofer.Name, _windowsUpdateDeviceIdentifierSpoofer.Value },
        { _windowsInstallationDateSpoofer.Name, _windowsInstallationDateSpoofer.Value }
      };

      var json = JsonSerializer.Serialize(data);
      File.WriteAllText("save.json", json);
    }

    private void HandleRestoreButtonClick(object sender, EventArgs e) {
      var text = File.ReadAllText("save.json");
      var data = JsonSerializer.Deserialize<Dictionary<string, object>>(text);

      if (data.TryGetValue("ComputerName", out var computerName)) {
        _computerNameSpoofer.Value = computerName;
      }

      if (data.TryGetValue("ComputerActiveName", out var computerActiveName)) {
        _computerActiveNameSpoofer.Value = computerActiveName;
      }

      if (data.TryGetValue("ComputerHostname", out var computerHostname)) {
        _computerHostnameSpoofer.Value = computerHostname;
      }

      if (data.TryGetValue("ComputerHardwareConfigurationIdentifier",
            out var computerHardwareConfigurationIdentifier)) {
        _computerHardwareConfigurationIdentifierSpoofer.Value = computerHardwareConfigurationIdentifier;
      }

      if (data.TryGetValue("ComputerHardwareIdentifiers", out var computerHardwareIdentifiers)) {
        _computerHardwareConfigurationIdentifierSpoofer.Value = computerHardwareIdentifiers;
      }

      if (data.TryGetValue("SystemManufacturer", out var systemManufacturer)) {
        _systemManufacturerSpoofer.Value = systemManufacturer;
      }

      if (data.TryGetValue("SystemProductName", out var systemProductName)) {
        _systemProductNameSpoofer.Value = systemProductName;
      }

      if (data.TryGetValue("SystemBIOSReleaseDate", out var systemBiosReleaseDate)) {
        _systemBiosReleaseDateSpoofer.Value = systemBiosReleaseDate;
      }

      if (data.TryGetValue("SystemBIOSVersion", out var systemBiosVersion)) {
        _systemBiosVersionSpoofer.Value = systemBiosVersion;
      }

      if (data.TryGetValue("WindowsUpdateDeviceIdentifier", out var windowsUpdateDeviceIdentifier)) {
        _windowsUpdateDeviceIdentifierSpoofer.Value = windowsUpdateDeviceIdentifier;
      }

      if (data.TryGetValue("WindowsInstallationDate", out var windowsInstallationDate)) {
        _windowsInstallationDateSpoofer.Value = windowsInstallationDate;
      }
    }
  }

}