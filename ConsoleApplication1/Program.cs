using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            string x = "23/tcp     open  telnet     ";
            int i = x.IndexOf('/');
            string y1 = x.Remove(i);
            string x2 = x.Remove(0, i+1).Trim();
            string y2 = x2.Remove(3);
            string x3 = x2.Remove(0, 3 + 1).Trim();
            int i3 = x3.IndexOf(' ');
            string y3 = x3.Remove(i3);
            string x4 = x3.Remove(0,i3+1).Trim();
            int i4 = x4.IndexOf(' ');
            string y4 = "";
            string y5 = "";
            if (i4 != -1)
            {
                y4 = x4.Remove(i4);
                y5 = x4.Remove(0, i4 + 1).Trim();
            }
            else
            {
                y4 = x4;
            }

            Console.WriteLine("{0}\r\n{1}\r\n{2}\r\n{3}\r\n{4}",y1,y2,y3,y4,y5);
            Console.ReadKey();
        }
    }
}
