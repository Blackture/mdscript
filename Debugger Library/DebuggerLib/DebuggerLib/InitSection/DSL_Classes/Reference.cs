using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Linq;

namespace DebuggerLib.InitSection.DSL_Classes
{
    public struct Ref
    {
        public string name;
        public string path;
    }

    public class Reference
    {
        public static List<Ref> References = new List<Ref>();

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

        public static void GetReferences(string line)
        {
            string[] splitems = line.Split('"', '=');

            string RefName = splitems[0].Trim();
            string RefPath = splitems[2].Replace('"', ' ').Trim();

            References.Add(new Ref() { name = RefName, path = RefPath });
        }

        public static bool ValidReferences(out string error)
        {
            List<string> paths = new List<string>();
            List<string> names = new List<string>();
            error = "";
            bool valid = true;

            for (int i = 0; i < References.Count; i++)
            {
                names.Add(References[i].name);
                paths.Add(References[i].path);
            }

            for (int j = 0; j < paths.Count; j++)
            {
                if (names.Take(j).Contains(names[j]))
                {
                    error = "2 or more references got the same name!";
                    valid = false;
                }
                else if (paths.Take(j).Contains(paths[j]))
                {
                    error = "2 or more references got the same path!";
                    valid = false;
                }
                else
                {
                    error = "";
                }
            }

            return valid;
        }
    }
}
