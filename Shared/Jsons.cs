using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Shudow.Shared {

  /// <summary>
  /// A utility class providing methods for handling JSON data.
  /// This class is intended for internal use and provides functionality to parse and convert JSON elements
  /// into .NET types or manipulate JSON-related data.
  /// </summary>
  internal static class Jsons {

    /// <summary>
    /// Parses a <see cref="JsonElement"/> and converts it into its corresponding .NET type.
    /// If the input object is not a <see cref="JsonElement"/>, it is returned unchanged.
    /// </summary>
    /// <param name="o">
    /// The object to be parsed. It can be a <see cref="JsonElement"/> or any other type.
    /// </param>
    /// <returns>
    /// The parsed result as a .NET type, such as a string, boolean, long, dictionary, or array.
    /// If the input object is not a <see cref="JsonElement"/>, it is returned as is.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when the <see cref="JsonElement"/> is of an unsupported <see cref="JsonValueKind"/>
    /// or when <see cref="JsonValueKind"/> is <see cref="JsonValueKind.Undefined"/> or <see cref="JsonValueKind.Null"/>.
    /// </exception>
    public static object Parse(
      object o
    ) {
      if (!(o is JsonElement json)) {
        return o;
      }

      switch (json.ValueKind) {
        case JsonValueKind.String:
          return json.GetString();

        case JsonValueKind.True:
        case JsonValueKind.False:
          return json.GetBoolean();

        case JsonValueKind.Number:
          return json.GetInt64();

        case JsonValueKind.Object: {
          var dictionary = json.Deserialize<Dictionary<string, object>>();

          foreach (var entry in dictionary.ToList()) {
            var value = entry.Value;

            if (value is JsonElement) {
              dictionary[entry.Key] = Parse(value);
            }
          }

          return dictionary;
        }
        case JsonValueKind.Array:
          return json.Deserialize<string[]>();

        case JsonValueKind.Undefined:
        case JsonValueKind.Null:
        default:
          throw new ArgumentOutOfRangeException($"Unsupported {nameof(json)} encountered.");
      }
    }
  }
}