using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DebuggerLib.EngineSection.DSL_Classes
{
    class Data
    {
        public static bool IsValid(string line, out string error)
        {
            error = "";
            string _line = line.Trim();
            if (_line.StartsWith("Data.AddVar"))
            {
                string __line = _line.Replace("Data.AddVar(", "").Replace(");", "");
                string[] args = __line.Split(',');
                if (args.Length > 3 || args.Length < 3)
                {
                    error = "The function AddVar belonging to the Data class takes exactly 3 arguments.";
                    return false;
                }
                string arg1 = args[0].Replace("\"", "");
                if (!Regex.Match(arg1, "[_a-zA-Z][_a-zA-Z0-9]{0,}").Success)
                {
                    error = "The 1. argument scheme is wrong.";
                    return false;
                }
                string arg2 = args[1];
            }
            return true;
        }
    }
}
