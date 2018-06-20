﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Web_Crawler
{
    class Utilities
    {
        /// <summary>
        /// Gets web file last modified date
        /// </summary>
        /// <param name="FileURL"></param>
        /// <returns></returns>
        public static DateTime FileLastModified(string FileURL)
        {
            try
            {
                var req = WebRequest.Create(FileURL);
                req.Method = "HEAD";
                req.Timeout = 300000;
                using (var fileResponse = (HttpWebResponse)req.GetResponse())
                    if (fileResponse.LastModified != null)
                        return fileResponse.LastModified;
                    else
                        return DateTime.MinValue;
            }
            catch { return DateTime.MinValue; }
        }

        /// <summary>
        /// Gets web file size in bytes
        /// </summary>
        /// <param name="FileURL"></param>
        /// <returns></returns>
        public static int FileSize(string FileURL)
        {
            try
            {
                var req = WebRequest.Create(FileURL);
                req.Method = "HEAD";
                req.Timeout = 300000;
                using (var fileResponse = (HttpWebResponse)req.GetResponse())
                    if (int.TryParse(fileResponse.Headers.Get("Content-Length"), out int ContentLength))
                        return ContentLength;
                    else
                        return 0;
            }
            catch { return 0; }
        }

        /// <summary>
        /// Checks if web file exists on server
        /// </summary>
        /// <param name="FileURL"></param>
        /// <returns></returns>
        public static bool URLExists(string URL)
        {
            try
            {
                var req = WebRequest.Create(URL);
                req.Timeout = 300000;

                try
                {
                    using (var fileResponse = (HttpWebResponse)req.GetResponse())
                        return true;
                }
                catch
                {
                    return false;
                }
            }
            catch { return false; }
        }

        /// <summary>
        /// Returns web text file contents in a list
        /// </summary>
        /// <param name="fileURL"></param>
        /// <param name="filePathToDownload"></param>
        /// <returns></returns>
        public static List<string> LoadWebTextFileItems(string fileURL, string filePathToDownload)
        {
            var textItems = new List<string>();
            var webClient = new WebClient();
            webClient.DownloadFile(fileURL, filePathToDownload + @"\web-servers.txt");
            textItems.AddRange(File.ReadAllLines(filePathToDownload + @"\web-servers.txt"));
            return textItems;
        }

        /// <summary>
        /// Checks if path is a local file
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool IsLocalFile(string filePath)
        {
            return File.Exists(filePath);
        }

        /// <summary>
        /// Checks if path is a web file
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool IsWebFile(string filePath)
        {
            try
            {
                Uri uri = new Uri(filePath);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}