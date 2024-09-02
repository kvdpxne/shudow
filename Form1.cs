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

    private static void Store(
      Dictionary<string, object> data,
      ISpoofer spoofer
    ) {
      data[spoofer.Name] = spoofer.Value;
    }

    private static void Restore(
      Dictionary<string, object> data,
      ISpoofer spoofer
    ) {
      if (data.TryGetValue(spoofer.Name, out var value)) {
        spoofer.Value = value;
      }
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
        _computerHardwareConfigurationIdentifierSpoofer.Value = $"{{{Guid.NewGuid()}}}";
      }

      if (changeComputerHardwareIdentifiers.Checked) {
        _computerHardwareIdentifiersSpoofer.Value = new Dictionary<string, object> {
          { "ComputerHardwareId", $"{{{Guid.NewGuid()}}}" },
          { "ComputerHardwareIds", string.Join("", Enumerable.Range(0, 10).Select(_ => $"{{{Guid.NewGuid()}}}\n")) }
        };
      }

      if (changeSystemManufacturer.Checked) {
        var name = "";
        name += Randoms.GenerateRandomAlphanumericText(3, 11);
        name += $" {Randoms.GenerateRandomAlphanumericText(2, 6)}";
        name += $" {Randoms.GenerateRandomAlphanumericText(9, 13)}";

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
        var lastYear = new DateTime(now.Year - 1, 1, 1);

        var date = Randoms.GenerateRandomDateTime(
          new DateTime(1999, 1, 1),
          lastYear
        );

        _systemBiosReleaseDateSpoofer.Value =
          date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
      }

      if (changeSystemBiosVersion.Checked) {
        _systemBiosVersionSpoofer.Value = $"{Randoms.GenerateRandomNumber(10, 10000)}";
      }

      if (changeWindowsUpdateDeviceIdentifier.Checked) {
        _windowsUpdateDeviceIdentifierSpoofer.Value = new Dictionary<string, object> {
          { "SusClientId", Guid.NewGuid() }, {
            "SusClientIdValidation", Encoding.UTF8.GetBytes(
              $"{Randoms.GenerateRandomAlphanumericText(200, 300)}=="
            )
          }
        };
      }

      if (changeWindowsInstallationDate.Checked) {
        var now = DateTime.Now;
        var lastYear = new DateTime(now.Year - 1, 1, 1);

        var begin = new DateTime(1999, 1, 1) - DateTime.MinValue;
        var end = lastYear - DateTime.MinValue;

        _windowsInstallationDateSpoofer.Value = new Dictionary<string, object> {
          {
            "InstallDate",
            (int)Randoms.GenerateRandomUnsignedInt(
              (uint)begin.TotalSeconds,
              (uint)end.TotalSeconds
            )
          }, {
            "InstallTime",
            (long)Randoms.GenerateRandomUnsignedLong(
              (ulong)begin.TotalMilliseconds,
              (ulong)end.TotalMilliseconds
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
      var data = new Dictionary<string, object>();

      Store(data, _computerNameSpoofer);
      Store(data, _computerActiveNameSpoofer);
      Store(data, _computerHostnameSpoofer);
      Store(data, _computerHardwareConfigurationIdentifierSpoofer);
      Store(data, _computerHardwareIdentifiersSpoofer);
      Store(data, _systemManufacturerSpoofer);
      Store(data, _systemProductNameSpoofer);
      Store(data, _systemBiosReleaseDateSpoofer);
      Store(data, _systemBiosVersionSpoofer);
      Store(data, _windowsUpdateDeviceIdentifierSpoofer);
      Store(data, _windowsInstallationDateSpoofer);

      var json = JsonSerializer.Serialize(data);
      File.WriteAllText("save.json", json);
    }

    private void HandleRestoreButtonClick(object sender, EventArgs e) {
      var text = File.ReadAllText("save.json");
      var data = JsonSerializer.Deserialize<Dictionary<string, object>>(text);

      Restore(data, _computerNameSpoofer);
      Restore(data, _computerActiveNameSpoofer);
      Restore(data, _computerHostnameSpoofer);
      Restore(data, _computerHardwareConfigurationIdentifierSpoofer);
      Restore(data, _computerHardwareIdentifiersSpoofer);
      Restore(data, _systemManufacturerSpoofer);
      Restore(data, _systemProductNameSpoofer);
      Restore(data, _systemBiosReleaseDateSpoofer);
      Restore(data, _systemBiosVersionSpoofer);
      Restore(data, _windowsUpdateDeviceIdentifierSpoofer);
      Restore(data, _windowsInstallationDateSpoofer);
    }
  }

}