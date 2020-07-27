using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DebuggerLib.InitSection.DSL_Classes
{
    public class Datapack
    {
        public static bool IsValid(string line)
        {
            if (Regex.Match(line, "\\b(Datapack)\\b\\.\\b(Description|Version|PackFormat|Name)\\b = \\S+;").Success)
            {
                if (line.Contains("PackFormat = "))
                {
                    string[] _line = line.Split(' ');
                    bool check = int.TryParse(_line[2].Replace(";", " ").Trim(), out int intCheck);
                    if (check == true && intCheck < 6 && intCheck > 0)
                    {
                        return true;
                    }
                    else
                        return false;
                }
                return true;
            }
            else
                return false;
        }

        public void Description()
        {

        }

        public void Version()
        {

        }

        public void PackFormat()
        {

        }

        public void Name()
        {

        }
    }
}
