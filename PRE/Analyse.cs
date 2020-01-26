using System;
using System.Collections.Generic;
using System.Text;

namespace PRE
{
    public static class Analyse
    {
        public static void Run()
        {
            Program.Reader reader = new Program.Reader();
            reader.Filename = Program.Config.PathIP;
            reader.ReadHeader();
        }
    }
}
