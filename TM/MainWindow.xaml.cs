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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<CCustomTreeItem> trees = new List<CCustomTreeItem>();
            CCustomTreeItem i = new CCustomTreeItem() { Icon = CCommon.StrFolderIconPath, DisplayName = "文件夹" };
            i.Children.Add(new CCustomTreeItem() { Icon = CCommon.StrExcelIconPath, DisplayName = "123" });
            i.Children.Add(new CCustomTreeItem() { Icon = CCommon.StrExcelIconPath, DisplayName = "123" });
            i.Children.Add(new CCustomTreeItem() { Icon = CCommon.StrExcelIconPath, DisplayName = "123" });
            i.Children.Add(new CCustomTreeItem() { Icon = CCommon.StrExcelIconPath, DisplayName = "123" });
            trees.Add(i);
            tw_item.ItemsSource = trees;
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

        private void MenuItem_Base_Setting(object sender, RoutedEventArgs e)
        {
            Setting s = new Setting();
            s.ShowDialog();     
        }
    }
}
