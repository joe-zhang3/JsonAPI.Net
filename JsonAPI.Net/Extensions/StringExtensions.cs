using System;
using System.IO;
using System.Text.RegularExpressions;
using Humanizer;

namespace JsonAPI.Net
{
    public static class StringExtensions
    {
        /// <summary>
        /// All three values below need to be evaulated:
        /// "first-name" : ""
        /// "first-name" : "{{}}"
        /// "first-name" : "{{FirstName}}"
        /// </summary>
        /// <returns><c>true</c>, if required was evaulationed, <c>false</c> otherwise.</returns>
        /// <param name="value">Value.</param>
        public static bool EvaulationRequired(this string value){
            return value.Length == 0 || value.Contains("{{") && value.Contains("}}");
        }

        public static bool HasRef(this string value)
		{
			return value.Contains("{%") && value.Contains("%}");
		}

        public static string GetEvaulationKey(this string value){
            return Regex.Match(value, @"{{(\S+)}}").Groups[1].Value.Trim().Replace("-", "_").Pascalize();
        }

        public static string GetKeyFromName(this string value){
            return value.Replace("-", "_").Pascalize();
        }

        public static string GetRefKey(this string value)
		{
            string key = Regex.Match(value, @"{%(\S+)%}").Groups[1].Value.Trim();

			return key.Pascalize();
		}

        public static bool IsId(this string value){
            return value.Equals("id");
        }

        public static string GetFileName(this string value){

            int index = value.LastIndexOf(Path.DirectorySeparatorChar);
            int dotIndex = value.LastIndexOf(".", StringComparison.CurrentCulture);

            return value.Substring(index + 1, dotIndex - index-1);
        }

        public static string ToPascal(this string value){
            return value.Replace("-", "_").Pascalize();
        }
    }
}
