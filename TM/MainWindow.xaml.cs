using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TM
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_ExportTable_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Table");
        }
        private void Command_ExportTable(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Table");
        }
        private void MenuItem_ExportTableBinary_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("TableBinary");
        }
        private void Command_ExportTableBinary(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("TableBinary");
        }
    }
}
