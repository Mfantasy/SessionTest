using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SessionTest
{
    public class CncRowModel
    {
        public CncRowModel(string name, string typ, string adr, string time, DataGridViewRow row)
        {
            this.Name = name;
            this.Typ = typ;
            this.Adr = adr;
            this.Time = time;
            this.Row = row;
        }
        public string Name { get; set; }
        public string Typ { get; set; }
        public string Adr { get; set; }
        public string Time { get; set; }
        public DataGridViewRow Row { get; set; }
    }
}
