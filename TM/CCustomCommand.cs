using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TM
{
    public static class CCustomCommand
    {
        public static RoutedCommand ExportTable = new RoutedCommand();
        public static RoutedCommand ExportTableBinary = new RoutedCommand();
        public static RoutedCommand GenerateCode = new RoutedCommand();

    }
}
