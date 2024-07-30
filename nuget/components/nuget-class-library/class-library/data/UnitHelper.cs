using System;
using nuget_class_library.Properties;

namespace nuget_class_library.class_library.data
{
	/// <summary>
	/// Helper class for conversion between measurement units.
	/// </summary>
	public static class UnitHelper
	{
		/// <summary>
		/// Takes a Unix timestamp and returns the equivalent ISO string value.
		/// </summary>
		/// <param name="unixTime">The unix timestamp given as a string, in milliseconds.</param>
		/// <returns>The timestamp as an ISO string.</returns>
		public static string UnixTimeToIsoTime(string unixTime)
		{
			return DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(unixTime)).ToUniversalTime().ToString(Resources.isoFormat);
		}

		/// <summary>
		/// Takes a speed measurement in kilometers per hour and returns the equivalent miles per hour value.
		/// </summary>
		/// <param name="kilometersPerHour">The speed measurement to convert to miles per hour.</param>
		/// <returns>The speed measurement in miles per hour.</returns>
		public static int GetMilesPerHourFromKilometersPerHour(int kilometersPerHour)
		{
			return Convert.ToInt32(Math.Round(kilometersPerHour / 1.609344));
		}
		
		/// <summary>
		/// Takes a distance measurement in kilometers and returns the equivalent miles value.
		/// </summary>
		/// <param name="kilometers">The distance measurement to convert to miles.</param>
		/// <returns>The distance measurement in miles.</returns>
		public static decimal GetMilesFromKilometers(decimal kilometers)
		{
			return kilometers / (decimal)1.609344;
		}
		
		/// <summary>
		/// Takes a distance measurement in kilometers and returns the equivalent meters value.
		/// </summary>
		/// <param name="kilometers">The distance measurement to convert to meters.</param>
		/// <returns>The distance measurement in meters.</returns>
		public static decimal GetMetersFromKilometers(decimal kilometers)
		{
			return kilometers * 1000;
		}

		/// <summary>
		/// Takes a <see cref="DateTime"/> object and returns an ISO 8601 string in format yyyy-MM-ddTHH:mm:ss.sssZ.
		/// </summary>
		/// <param name="dateTime">The <see cref="DateTime"/> object to convert.</param>
		/// <returns>ISO 8601 string representation of the supplied <see cref="DateTime"/> object.</returns>
		public static string DateTimeToIsoString(DateTime dateTime)
		{
			return dateTime.ToString(Resources.isoFormat);
		}

		/// <summary>
		/// Takes a <see cref="DateTime"/> object and returns string in format dd/MM/yyyy.
		/// </summary>
		/// <param name="dateTime">The <see cref="DateTime"/> object to convert.</param>
		/// <returns>String representation of the supplied <see cref="DateTime"/> object.</returns>
		public static string DateTimeToString(DateTime dateTime)
		{
			return dateTime.ToString("ddMMyyyy");
		}

        /// <summary>
        /// Takes a <see cref="DateTime"/> object and returns string in format dd/MM/yyyy.
        /// </summary>
        /// <param name="dateTime">The <see cref="DateTime"/> object to convert.</param>
        /// <returns>String representation of the supplied <see cref="DateTime"/> object.</returns>
        public static string DateTimeToIsoStringCustom(DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-ddTHH-mm-ss-sssZ");
        }

		/// <summary>
		/// Takes a <see cref="DateTime"/> object and returns string in format yyyy-MM-ddTHH:mm:ssZ.
		/// </summary>
		/// <param name="dateTime">The <see cref="DateTime"/> object to convert.</param>
		/// <returns>String representation of the supplied <see cref="DateTime"/> object.</returns>
		public static string DateTimeToStringCustom(DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-ddTHH:mm:ssZ");
        }
		
		/// <summary>
		/// Takes a <see cref="DateTime"/> object and returns string in format yyyy-MM-ddTHH:mm:ss.
		/// </summary>
		/// <param name="dateTime">The <see cref="DateTime"/> object to convert.</param>
		/// <returns>String representation of the supplied <see cref="DateTime"/> object.</returns>
		public static string DateTimeToStringCustomShort(DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-ddTHH:mm:ss");
        }

        /// <summary>
        /// Takes a <see cref="DateTime"/> object and returns an ISO 8601 string in format yyyy-MM-ddTHH:mm:ss.sssZ.
        /// </summary>
        /// <param name="dateTime">The <see cref="string"/> object to convert.</param>
        /// <returns>DateTime representation of the supplied ISO 8601 <see cref="string"/>.</returns>
        public static DateTime IsoStringToDateTime(string dateTime)
		{
			return Convert.ToDateTime(dateTime);
		}

		/// <summary>
		/// Takes a <see cref="string"/> object and returns it in a Base64 encoded format.
		/// </summary>
		/// <param name="plainText">The <see cref="string"/> object to convert.</param>
		/// <returns>Base64 encoded string representation of the supplied <see cref="string"/>.</returns>
		public static string Base64Encode(string plainText)
		{
			var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
			return Convert.ToBase64String(plainTextBytes);
		}
	}
}
