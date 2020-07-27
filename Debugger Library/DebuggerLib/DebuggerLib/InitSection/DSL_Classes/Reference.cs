using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace DebuggerLib.InitSection.DSL_Classes
{
    public class Reference
    {
        public static bool IsValid(string line)
        {
            if (Regex.Match(line, "[A-Za-z_][_A-Za-z0-9]{0,} = \\bReferenceFile\\b\\.\\bImport\\b[(]\".+\"[)];").Success)
            {
                string[] split = line.Split('"');
                if (File.Exists(split[1].Replace('"', ' ').Trim()) && (Path.GetExtension(split[1].Replace('"', ' ').Trim()) == ".mdscript" ^ Path.GetExtension(split[1].Replace('"', ' ').Trim()) == ".mds"))
                {
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }
    }
}
