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
using System.IO;

namespace TM
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public CResourceManager ResManager;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ResManager = new CResourceManager();
            RefreshResource();
        }

        private void Command_GenerateCode(object sender, ExecutedRoutedEventArgs e)
        {
            Log("导出代码成功");
        }
        private void Command_ExportTable(object sender, ExecutedRoutedEventArgs e)
        {
            Log("导出表格成功");
        }
        private void Command_ExportTableBinary(object sender, ExecutedRoutedEventArgs e)
        {
            Log("导出二进制表成功");
        }

        private void MenuItem_CodeGenerate_Click(object sender, RoutedEventArgs e)
        {
            Log("导出代码成功");
        }
        private void MenuItem_ExportTable_Click(object sender, RoutedEventArgs e)
        {
            Log("导出表格成功");
        }
        private void MenuItem_ExportTableBinary_Click(object sender, RoutedEventArgs e)
        {
            Log("导出二进制表成功");
        }
        private void MenuItem_Base_Setting(object sender, RoutedEventArgs e)
        {
            Setting s = new Setting();
            s.ShowDialog();
            RefreshResource();
        }
        public void Log(string log, bool isClear = false)
        {
            if (isClear)
            {
                sv_output.Content = string.Empty;
            }
            else
            {
                StringBuilder s = new StringBuilder();
                s.Append("  ");
                s.Append(log);
                s.Append("\n");
                sv_output.Content += s.ToString();
            }
            sv_output.ScrollToBottom();
        }

        public void RefreshResource()
        {
            string v = CCommon.GetValue(CCommon.key_table);
            if (Directory.Exists(v))
            {
                List<string> p = new List<string>();
                bool isExcel = CCommon.GetValue(CCommon.key_fileExcel) == "1";
                bool isTxt = CCommon.GetValue(CCommon.key_fileTxt) == "1";
                bool isCode = CCommon.GetValue(CCommon.key_fileCode) == "1";
                if (isExcel)
                    p.Add(".xlsm");
                if (isTxt)
                    p.Add(".txt");
                if (isCode)
                    p.Add(".cs");
                List<CCustomTreeItem> resItems = new List<CCustomTreeItem>();
                CCustomTreeItem resItem = new CCustomTreeItem();
                string curName = System.IO.Path.GetFileName(v);
                resItem.Icon = CCommon.StrFolderIconPath;
                resItem.DisplayName = curName;
                ResManager.GetResources(v, resItem, p.ToArray());
                resItems.Add(resItem);
                tw_item.ItemsSource = resItems;
            }
        }

    }
}
