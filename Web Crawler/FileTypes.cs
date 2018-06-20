﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_Crawler
{
    class FileTypes
    {
        public static List<string> All = new List<string>();
        public static List<string> Video { get; } = new List<string>() { "M2TS", "MP4", "MKV", "AVI", "MPEG", "MPG", "MOV", "M4V" };
        public static List<string> Audio { get; } = new List<string>() { "MP3", "WMA", "WAV", "M3U", "APE", "AIF", "MPA", "CDA", "AC3", "OGG", "FLAC", "M4A" };
        public static List<string> Image { get; } = new List<string>() { "TIFF", "TIF", "JPEG", "JPG", "BMP", "GIF", "PNG", "EPS", "RAW", "SVG" };
        public static List<string> Book { get; } = new List<string>() { "MOBI", "CBZ", "CBR", "CBC", "CHM", "EPUB", "FB2", "LIT", "LRF", "ODT", "PDF", "PRC", "PDB", "PML", "RB", "RTF", "TCR", "DOC", "DOCX" };
        public static List<string> Software { get; } = new List<string>() { "EXE", "VOB", "ZIP", "TAR", "RAR", "7Z", "ISO", "PKG", "TAR.GZ", "APK", "IPA", "APPX", "XAP", "JAR" };
        public static List<string> Torrent { get; } = new List<string>() { "TORRENT" };
        public static List<string> Subtitle { get; } = new List<string>() { "SRT", "SUB", "VTT" };
        public static List<string> Other { get; } = new List<string>() { "MQ4", "NDS", "JSP", "GG", "SWF", "PS", "RTF", "BAS", "CC", "C", "CPP", "CXX", "H", "HPP", "CS", "JAVA", "PY", "PL", "PHP", "HTML", "ASPX", "XML", "TXT", "SQL", "CSV", "PSD", "GZ", "BIN", "PAR", "PAR2", "PYK", "PK3", "PK4", "SKB", "IMG", "HGZ", "AI", "OTF", "TTF", "CSS", "PLS", "Z64" };

    }
}
