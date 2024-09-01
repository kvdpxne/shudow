using System;
using System.Text;

namespace Shudow.Shared {

  internal static class Randoms {

    private const string ALPHANUMERIC_CHARACTERS = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    private const string MAC_CHARACTERS = "";

    /// <summary>
    /// A static instance of the Random class used for generating random values.
    /// </summary>
    private static readonly Random Instance;

    /// <summary>
    ///
    /// </summary>
    static Randoms() {
      Instance = new Random();
    }

    /// <summary>
    ///
    /// </summary>
    public static int GenerateRandomNumber(
      int min,
      int max
    ) {
      return Instance.Next(min, 1 + max);
    }

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
    /// Generates a random unsigned 32-bit integer within a specified range.
    /// </summary>
    /// <param name="min">The inclusive lower bound of the random number returned.</param>
    /// <param name="max">The inclusive upper bound of the random number returned.</param>
    /// <returns>A random unsigned 32-bit integer between <paramref name="min"/> and <paramref name="max"/> (inclusive).</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="min"/> is greater than <paramref name="max"/>.</exception>
    public static uint GenerateRandomUnsignedInt(
      uint min,
      uint max
    ) {
      if (min > max) {
        throw new ArgumentException("Min value should be less than or equal to max value.");
      }

      var buffer = GenerateRandomBytes(sizeof(uint));
      var randomValue = BitConverter.ToUInt32(buffer, 0);

      // Map the random value to the specified range
      return min + randomValue % (max - min + 1);
    }

    /// <summary>
    /// Generates a random unsigned 64-bit integer within a specified range.
    /// </summary>
    /// <param name="min">The inclusive lower bound of the random number returned.</param>
    /// <param name="max">The inclusive upper bound of the random number returned.</param>
    /// <returns>A random unsigned 64-bit integer between <paramref name="min"/> and <paramref name="max"/> (inclusive).</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="min"/> is greater than <paramref name="max"/>.</exception>
    public static ulong GenerateRandomUnsignedLong(
      ulong min,
      ulong max
    ) {
      if (min > max) {
        throw new ArgumentException("Min value should be less than or equal to max value.");
      }

      var buffer = GenerateRandomBytes(sizeof(uint));
      var randomValue = BitConverter.ToUInt32(buffer, 0);

      // Map the random value to the specified range
      return min + randomValue % (max - min + 1);
    }

    public static string GenerateRandomAlphanumericText(
      int minLength,
      int maxLength = 15
    ) {
      var length = Instance.Next(minLength, 1 + maxLength);
      var builder = new StringBuilder(length);

      for (var i = 0; length > i; ++i) {
        builder.Append(ALPHANUMERIC_CHARACTERS[Instance.Next(ALPHANUMERIC_CHARACTERS.Length)]);
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

}