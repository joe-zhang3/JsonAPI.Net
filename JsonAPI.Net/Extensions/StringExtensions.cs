using System;
using System.IO;
using System.Text.RegularExpressions;
using Humanizer;

namespace JsonAPI.Net.Extensions
{
    public static class StringExtensions
    {
        public static bool EvaulationRequired(this string value){
            return value.Contains("{{") && value.Contains("}}");
        }

        public static bool HasRef(this string value)
		{
			return value.Contains("{%") && value.Contains("%}");
		}

        public static string GetEvaulationKey(this string value){

            string key = Regex.Match(value, @"{{(\S+)}}").Groups[1].Value.Trim();

            return key.Pascalize();
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
    }
}
