using System.Text;

namespace Shudow.Shared;

/// <summary>
/// Provides methods for generating random values, including integers, bytes, and strings.
/// This class includes functionality for creating random numbers within a specified range,
/// generating random byte arrays, and producing random alphanumeric strings.
/// </summary>
internal static class Randoms {

  /// <summary>
  /// A constant string containing all alphanumeric characters,
  /// including uppercase letters (A-Z), lowercase letters (a-z), and digits (0-9).
  /// </summary>
  private const string AlphanumericCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

  private const string MacCharacters = "";

  /// <summary>
  /// A static instance of the Random class used for generating random values.
  /// </summary>
  private static readonly Random Instance = new Random();

  /// <summary>
  /// Generates a random byte array of the specified size.
  /// </summary>
  /// <param name="size">The number of bytes to generate.</param>
  /// <returns>A byte array filled random bytes.</returns>
  private static byte[] GenerateRandomBytes(
    int size
  ) {
    var buffer = new byte[size];
    Instance.NextBytes(buffer);

    return buffer;
  }

  /// <summary>
  /// Generates a random 32-bit integer within a specified range.
  /// </summary>
  /// <param name="min">The inclusive lower bound of the random number returned.</param>
  /// <param name="max">The inclusive upper bound of the random number returned.</param>
  /// <returns>A random 32-bit integer between <paramref name="min"/> and <paramref name="max"/> (inclusive).</returns>
  /// <exception cref="ArgumentException">Thrown when <paramref name="min"/> is greater than <paramref name="max"/>.</exception>
  public static int GenerateRandomInt(
    int min,
    int max
  ) {
    if (min > max) {
      throw new ArgumentException("Min value should be less than or equal to max value.");
    }

    var buffer = GenerateRandomBytes(sizeof(int));
    var randomValue = BitConverter.ToInt32(buffer, 0);

    // Map the random value to the specified range
    return min + Math.Abs(randomValue) % (max - min + 1);
  }

  /// <summary>
  /// Generates a random 64-bit integer within a specified range.
  /// </summary>
  /// <param name="min">The inclusive lower bound of the random number returned.</param>
  /// <param name="max">The inclusive upper bound of the random number returned.</param>
  /// <returns>A random 64-bit integer between <paramref name="min"/> and <paramref name="max"/> (inclusive).</returns>
  /// <exception cref="ArgumentException">Thrown when <paramref name="min"/> is greater than <paramref name="max"/>.</exception>
  public static long GenerateRandomLong(
    long min,
    long max
  ) {
    if (min > max) {
      throw new ArgumentException("Min value should be less than or equal to max value.");
    }

    var buffer = GenerateRandomBytes(sizeof(long));
    var randomValue = BitConverter.ToInt64(buffer, 0);

    // Map the random value to the specified range
    return min + Math.Abs(randomValue) % (max - min + 1);
  }

  /// <summary>
  /// Generates a random alphanumeric string of a specified length.
  /// </summary>
  /// <param name="min">The minimum length of the generated string.</param>
  /// <param name="max">The maximum length of the generated string (default is 15).</param>
  /// <param name="digits">
  /// A boolean flag indicating whether digits (0-9) should be included in the string.
  /// If set to <c>true</c>, the generated string will include digits; otherwise, only alphabetic characters will be used.
  /// </param>
  /// <returns>
  /// A random alphanumeric string with a length between <paramref name="min"/> and <paramref name="max"/> (inclusive).
  /// </returns>
  /// <exception cref="ArgumentException">
  /// Thrown when <paramref name="min"/> is greater than <paramref name="max"/>.
  /// </exception>
  public static string GenerateRandomString(
    int min,
    int max = 15,
    bool digits = true
  ) {
    if (min > max) {
      throw new ArgumentException("Min value should be less than or equal to max value.");
    }

    var length = Instance.Next(min, max + 1);
    var builder = new StringBuilder(length);

    var lastIndex = digits
      ? AlphanumericCharacters.Length
      : AlphanumericCharacters.Length - 10;

    for (var i = 0; length > i; ++i) {
      var index = Instance.Next(0, lastIndex);
      var character = AlphanumericCharacters[index];

      builder.Append(character);
    }

    return builder.ToString();
  }

  public static DateTime GenerateRandomDateTime(
    DateTime start,
    DateTime end
  ) {
    if (end <= start) {
      throw new ArgumentException("The end date must be later than the start date.");
    }

    var range = (end - start).Days;
    var randomDays = Instance.Next(range);

    return start.AddDays(randomDays);
  }

  public static DateTime GenerateRandomDateTime(
    DateTime start
  ) {
    return GenerateRandomDateTime(start, DateTime.Now);
  }
}