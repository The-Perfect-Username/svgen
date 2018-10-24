using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace svgen
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Command requires one parameter (source file path)");
            } else
            {
                SVG svg = new SVG();
                svg.CreateSVG(args[0]);
            }
        }
    }
}
