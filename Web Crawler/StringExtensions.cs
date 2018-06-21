﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Web_Crawler
{
    class StringExtensions
    {
        /// <summary>
        /// Returns all sub strings in string
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Sub strings in string</returns>
        public static string[] GetWords(string input)
        {
            var matches = Regex.Matches(input, @"\b[\w']*\b");

            var words = from m in matches.Cast<Match>()
                        where !string.IsNullOrEmpty(m.Value)
                        select TrimSuffix(m.Value);

            return words.ToArray();
        }

        public static string TrimSuffix(string word)
        {
            int apostropheLocation = word.IndexOf('\'');
            if (apostropheLocation != -1)
                word = word.Substring(0, apostropheLocation);

            return word;
        }

        /// <summary>
        /// If string contains all sub strings
        /// </summary>
        /// <param name="source"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static bool ContainsAll(string source, params string[] values)
        {
            return values.All(x => source.ToLower().Contains(x));
        }

        /// <summary>
        /// Get random string from a collection of strings
        /// </summary>
        /// <param name="Items"></param>
        /// <returns></returns>
        public static string Random(ICollection<string> items)
        {
            var Rndm = new Random();
            var StringList = new List<string>(items);
            return StringList[Rndm.Next(0, items.Count)];
        }

        /// <summary>
        /// Return number represented as string with comma's for thousands
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string FormatNumber(string value)
        {
            return string.Format("{0:n0}", Convert.ToInt32(value));
        }

        /// <summary>
        /// Return file size with suffix e.g. Bytes, MB, GB
        /// </summary>
        /// <param name="byteCount"></param>
        /// <returns>Bytes in string format</returns>
        public static String BytesToPrefix(long byteCount)
        {
            string[] suf = { "Bytes", "KB", "MB", "GB", "TB", "PB", "EB" }; // Longs run out around EB
            if (byteCount == 0)
                return "0" + " " + suf[0];
            long bytes = Math.Abs(byteCount);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return (Math.Sign(byteCount) * num).ToString() + " " + suf[place];
        }

        /// <summary>
        /// Get age from DateTime
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string TimeSpanAge(DateTime date)
        {
            const int SECOND = 1;
            const int MINUTE = 60 * SECOND;
            const int HOUR = 60 * MINUTE;
            const int DAY = 24 * HOUR;
            const int MONTH = 30 * DAY;

            var ts = new TimeSpan(DateTime.UtcNow.Ticks - date.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);

            if (delta < 1 * MINUTE)
                return ts.Seconds == 1 ? "1 second" : ts.Seconds + " seconds";

            if (delta < 2 * MINUTE)
                return "1 minute";

            if (delta < 45 * MINUTE)
                return ts.Minutes + " minutes";

            if (delta < 90 * MINUTE)
                return "1 hour";

            if (delta < 24 * HOUR)
                return ts.Hours + " hours";

            if (delta < 48 * HOUR)
                return "1 day";

            if (delta < 30 * DAY)
                return ts.Days + " days";

            if (delta < 12 * MONTH)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "1 month" : months + " months";
            }
            else
            {
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? "1 year" : years + " years";
            }
        }

        /// <summary>
        /// Get parent folder path in the uri structure
        /// </summary>
        /// <param name="uri"></param>
        /// <returns>Parent folder url</returns>
        public static string ParentDirectory(Uri uri)
        {
            StringBuilder parentName = new StringBuilder();
            parentName.Append(uri.Scheme);
            parentName.Append("://");
            parentName.Append(uri.Host);
            for (int i = 0; i < uri.Segments.Length - 1; i++)
                parentName.Append(uri.Segments[i]);
            return parentName.ToString();
        }

        /// <summary>
        /// Checks if string is valid Json
        /// </summary>
        /// <param name="json"></param>
        /// <returns>If valid json</returns>
        public static bool IsValidJSON(string json)
        {
            try
            {
                JToken token = JObject.Parse(json);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    static class ThreadSafeRandom
    {
        [ThreadStatic] private static Random Local;

        public static Random ThisThreadsRandom
        {
            get { return Local ?? (Local = new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId))); }
        }
    }

    static class ListExtensions
    {
        /// <summary>
        /// Shuffle array list items
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
