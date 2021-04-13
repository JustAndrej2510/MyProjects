using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Lab_6_7
{
    public class WindowCommands
    {
        static WindowCommands()
        {
            Add = new RoutedCommand("Exit", typeof(MainWindow));
        }
        public static RoutedCommand Add { get; set; }
    }
}
