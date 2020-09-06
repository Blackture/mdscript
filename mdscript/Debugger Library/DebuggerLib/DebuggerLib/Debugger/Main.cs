using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace DebuggerLib.Debugger
{
    public class Main
    {
        public static IContent Project = new Content(null, null, null, null);

        public static string GetMainFile(string path, out string message)
        {
            List<string> mainFile = new List<string>();
            if (Directory.Exists(path))
            {
                string[] files = Directory.GetFiles(path);
                foreach (string file in files)
                {
                    if (Path.GetExtension(file) == ".mdscript" || Path.GetExtension(file) == ".mds")
                    {
                        string[] content = File.ReadAllLines(file);
                        foreach (string line in content)
                        {
                            if (line.Contains("init:"))
                            {
                                mainFile.Add(file);
                            }
                        }
                    }
                }
            }

            if (mainFile.Count <= 0 || mainFile.Count > 1)
            {
                message = "❌ The folder should contain exact 1 main file. No more no less!";
                return null;
            }
            
            else
            {
                message = "✓ Main file found";
                return mainFile[0];
            }
                
        }

        public static void Debug(string path, out string message)
        {
            message = "✓ Debug successful";
            string[] _lines = File.ReadAllLines(path);
            List<string> lines = new List<string>();
            bool blockComment = false;

            foreach (string line in _lines)
            {
                if (line.Trim().StartsWith(":("))
                {
                    blockComment = true;
                }
                else if (line.Trim().Contains("):") && blockComment)
                {
                    blockComment = false;
                }
                else if (line.Contains("::"))
                {
                    int index = line.IndexOf("::");
                    lines.Add(line.Remove(index).Trim());
                }
                else if (line.Trim() != null || line.Trim() != "")
                {
                    lines.Add(line.Trim());
                }
            }

            string[] initSection = InitSection.Processing.GetInitSection(lines);
            if (InitSection.Processing.Debug(InitSection.Processing.GetInitSection(lines), out string error))
            {
                Project.InitSection = initSection;
            }
            else
            {
                message = $"❌ Could not debug the init section. -> {error}";
                return;
            }

            string[] engineSection = EngineSection.Processing.GetEngineSection(lines);
            if (EngineSection.Processing.Debug(EngineSection.Processing.GetEngineSection(lines), out error))
            {
                Project.EngineSection = engineSection;
            }
            else
            {
                message = $"❌ Could not debug the engine section. -> {error}";
                return;
            }
        }
    }
}
