using SessionTest.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SessionTest
{
    public static class AppContextRecord
    {
        private static MenuButtonPanel lastMenuItem = null;

        public static void ClickMenuButton(MenuButtonPanel sender)
        {
            if (lastMenuItem == null)
            {
                lastMenuItem = sender;
            }
            else if (lastMenuItem != sender)
            {
                lastMenuItem.ShowDefaultImage(null, null);
                lastMenuItem.IsDown = false;
                lastMenuItem = sender;
            }
        }
    }
}
