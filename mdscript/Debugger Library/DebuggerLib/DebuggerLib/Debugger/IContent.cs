using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DebuggerLib.Debugger
{
    public interface IContent
    {
        string Path { get; set; }
        string[] InitSection { get; set; }
        string[] EngineSection { get; set; }
        string OutputPath { get; set; }
    }

    public class Content : IContent
    {
        public Content(string path, string[] initSection, string[] engineSection, string outputSection)
        {
            if (File.Exists(path))
                Path = path;
            InitSection = initSection;
            EngineSection = engineSection;
            if (Directory.Exists(path))
                OutputPath = outputSection;
        }

        public string Path { get; set; }
        public string[] InitSection { get; set; }
        public string[] EngineSection { get; set; }
        public string OutputPath { get; set; }
    }
}
