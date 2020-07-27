using DebuggerLib.InitSection.DSL_Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace DebuggerLib.InitSection
{
    public class Processing
    {
        public static string[] GetInitSection(List<string> contentOfFile)
        {
            List<string> lines = new List<string>();
            bool found = false;

            foreach (string line in contentOfFile)
            {
                if (line == "init:" && !found)
                    found = true;
                else if (line != "init:" && line != ":end" && found == true)
                    lines.Add(line);
                else if (line == ":end" && found == true)
                    found = false;
            }

            return lines.ToArray();
        }

        public static bool Debug(string[] initLines, out string error)
        {
            error = "";
            bool ok = false;

            foreach (string line in initLines)
            {
                if (Datapack.IsValid(line.Trim()) || Reference.IsValid(line.Trim()))
                {
                    error = "";
                    ok = true;
                }
                else
                {
                    error = $"The {initLines.ToList().IndexOf(line) + 1}. command ({line})!";
                    ok = false;
                }
            }

            return ok;
        }
    }
}

//C:\Users\patri\Documents\GitHub\mdscript\Test MDScript Files
