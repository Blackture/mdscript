using System;
using System.Collections.Generic;
using System.Text;

namespace DebuggerLib.EngineSection
{
    public class Processing
    {
        public static string[] GetEngineSection(List<string> contentOfFile)
        {
            List<string> lines = new List<string>();
            bool found = false;

            foreach (string line in contentOfFile)
            {
                if (line == "engine:" && !found)
                    found = true;
                else if (line != "init:" && line != ":end" && found == true)
                    lines.Add(line);
                else if (line == ":end" && found == true)
                    found = false;
            }

            return lines.ToArray();
        }

        public static bool Debug(string[] engineSection, out string error)
        {
            error = "";
            return false;
        }
    }
}
