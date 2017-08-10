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
using System.Collections.ObjectModel;
using System.Windows.Forms;
using System.Diagnostics;

namespace TM
{
    public partial class MainWindow : Window
    {
        #region Field
        CTableManager m_TableManager;
        CResourceManager m_ResManager;
        TreeViewItem m_CurSelectedResItem;
        ObservableCollection<CResourceItem> m_ResItems;
        #endregion

        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            m_ResManager = new CResourceManager();
            m_TableManager = new CTableManager();
            m_ResItems = new ObservableCollection<CResourceItem>();
            RefreshResource();
        }

        #region base
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
            if (m_CurSelectedResItem != null)
            {
                CResourceItem ri = m_CurSelectedResItem.DataContext as CResourceItem;
                if (ri != null)
                {
                    if (Directory.Exists(ri.Path))
                    {
                        
                    }
                    else
                    {
                        if (File.Exists(ri.Path))
                        {
                            m_TableManager.ExportTable(ri.Path);
                        }
                    }                 
                }
            }
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
        #endregion

        #region resource_manager

        #region Open
        private void MenuItem_OpenFileExplore(object sender, RoutedEventArgs e)
        {
            if (m_CurSelectedResItem != null)
            {
                CResourceItem ri = m_CurSelectedResItem.DataContext as CResourceItem;
                if (ri != null)
                {
                    string dir = System.IO.Path.GetDirectoryName(ri.Path);
                    Process.Start("explorer.exe ", dir);
                }
            }

        }
        private void ResItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is TreeViewItem)
                if (!((TreeViewItem)sender).IsSelected)
                    return;
            TreeViewItem tviSender = sender as TreeViewItem;
            if (tviSender != null)
            {
                CResourceItem ri = tviSender.DataContext as CResourceItem;
                if (ri != null)
                {
                    if (File.Exists(ri.Path))
                        Process.Start("explorer.exe ", ri.Path);
                }
            }
        }
        #endregion

        #region Create
        private void MenuItem_Create_Folder(object sender, RoutedEventArgs e)
        {
            if (m_CurSelectedResItem != null)
            {
                CResourceItem ri = m_CurSelectedResItem.DataContext as CResourceItem;
                if (ri != null)
                {
                    CreateNew c = new CreateNew();
                    c.ShowDialog();
                    if (!string.IsNullOrEmpty(c.InputName))
                    {
                        EFileFlag f = CFileManager.CreateDirectory(ri.Path, c.InputName);
                        _LogFileFlag(f);
                        if (f == EFileFlag.Success)
                            RefreshResource();
                    }

                }
            }
        }
        private void MenuItem_Create_Excel(object sender, RoutedEventArgs e)
        {
        }
        private void MenuItem_Create_Text(object sender, RoutedEventArgs e)
        {
            if (m_CurSelectedResItem != null)
            {
                CResourceItem ri = m_CurSelectedResItem.DataContext as CResourceItem;
                if (ri != null)
                {
                    CreateNew c = new CreateNew();
                    c.ShowDialog();
                    if (!string.IsNullOrEmpty(c.InputName))
                    {
                        EFileFlag f = CFileManager.CreateFile(ri.Path, c.InputName, EFileType.Text);
                        _LogFileFlag(f);
                        if (f == EFileFlag.Success)
                            RefreshResource();
                    }
                }
            }
        }

        #endregion

        #region Delete
        private void MenuItem_Delete(object sender, RoutedEventArgs e)
        {
            if (m_CurSelectedResItem != null)
            {
                CResourceItem ri = m_CurSelectedResItem.DataContext as CResourceItem;
                if (ri != null)
                {
                    EFileFlag f = CFileManager.Delete(ri.Path);
                    _LogFileFlag(f);
                    if (f == EFileFlag.Success)
                        RefreshResource();
                }
            }
        }
        #endregion

        #region Refresh
        private void MenuItem_Refresh(object sender, RoutedEventArgs e)
        {
            RefreshResource();
        }
        public void RefreshResource()
        {
            string v = CCommon.GetValue(CCommon.key_table);
            if (Directory.Exists(v))
            {
                #region 1.file ext
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
                #endregion

                #region 2. filter is change the root path
                if (m_ResItems.Count > 0)
                {
                    if (m_ResItems.First().Path != v)
                        m_ResItems.Clear();
                }
                CResourceItem resItemRoot;
                if (m_ResItems.Count > 0)
                {
                    resItemRoot = m_ResItems[0];
                }
                else
                {
                    resItemRoot = new CResourceItem();
                    string curName = System.IO.Path.GetFileName(v);
                    resItemRoot.Icon = CCommon.StrFolderIconPath;
                    resItemRoot.DisplayName = curName;
                    resItemRoot.Path = v;
                    m_ResItems.Add(resItemRoot);
                }
                m_ResManager.GetResources(v, resItemRoot, p.ToArray());
                tw_item.ItemsSource = m_ResItems;
                if (tw_item.ItemContainerGenerator.Items.Count > 0)
                {
                    TreeViewItem ti = (TreeViewItem)(tw_item.ItemContainerGenerator.ContainerFromIndex(0));
                    if (ti != null)
                        ti.IsExpanded = true;
                }
                #endregion
            }
        }
        #endregion

        #region Base
        private void ResItem_MouseRightButtonClick(object sender, MouseButtonEventArgs e)
        {
            var treeViewItem = _VisualUpwardSearch<TreeViewItem>(e.OriginalSource as DependencyObject) as TreeViewItem;
            if (treeViewItem != null)
            {
                m_CurSelectedResItem = treeViewItem;
                treeViewItem.Focus();
                e.Handled = true;
            }
        }
        DependencyObject _VisualUpwardSearch<T>(DependencyObject source)
        {
            while (source != null && source.GetType() != typeof(T))
                source = VisualTreeHelper.GetParent(source);
            return source;
        }

        private void _LogFileFlag(EFileFlag f)
        {
            string str_f = string.Empty;
            switch (f)
            {
                case EFileFlag.File_Exist:
                    str_f = "文件已存在";
                    break;
                case EFileFlag.Path_NotExist:
                    str_f = "路径不存在";
                    break;
                case EFileFlag.Directory_NotExist:
                    str_f = "文件夹不存在";
                    break;
                case EFileFlag.Success:
                    str_f = "操作成功";
                    break;
                default:
                    break;
            }
            Log(str_f);
        }
        #endregion

        #endregion

        #region common
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




        #endregion

        private void Window_Closed(object sender, EventArgs e)
        {
            CExcelManager.Instance.Close();
        }
    }
}
