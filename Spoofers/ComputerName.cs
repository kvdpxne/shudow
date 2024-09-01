using Shudow.Shared;

namespace Shudow.Spoofers {

  internal class ComputerName : ISpoofer {

    public string Name => "ComputerName";

    public object Value {
      get {

        using (var key = Registries.GetComputerName(false)) {
          return key.GetValue("ComputerName");
        }

      }
      set {
        using (var key = Registries.GetComputerName(true)) {
          key.SetValue("ComputerName", value);
        }
      }
    }
  }

}