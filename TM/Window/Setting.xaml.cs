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
using System.Windows.Shapes;
using System.Configuration;
namespace TM
{
    /// <summary>
    /// Setting.xaml 的交互逻辑
    /// </summary>
    public partial class Setting : Window
    {
        public Setting()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tb_code_path.Text = CCommon.GetValue(CCommon.key_codeEP);
            tb_table_path.Text = CCommon.GetValue(CCommon.key_tableP);
            tb_table_exopt_path.Text = CCommon.GetValue(CCommon.key_tableEP);
            tb_table_exopt_binary_path.Text = CCommon.GetValue(CCommon.key_tableEBP);
            cb_excel.IsChecked = CCommon.GetValue(CCommon.key_fileExcel) == "1";
            cb_txt.IsChecked = CCommon.GetValue(CCommon.key_fileTxt) == "1";
            cb_code.IsChecked = CCommon.GetValue(CCommon.key_fileCode) == "1";

        }
        private void bt_code_setting_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                string path = folderBrowserDialog.SelectedPath;
                tb_code_path.Text = path;
                CCommon.SetValue(CCommon.key_codeEP, path);
            }
        }
        private void bt_table_exopt_binary_setting_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                string path = folderBrowserDialog.SelectedPath;
                tb_table_exopt_binary_path.Text = path;
                CCommon.SetValue(CCommon.key_tableEBP, path);
            }
        }

        private void bt_table_exopt_setting_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                string path = folderBrowserDialog.SelectedPath;
                tb_table_exopt_path.Text = path;
                CCommon.SetValue(CCommon.key_tableEP, path);
            }
        }

        private void bt_table_setting_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                string path = folderBrowserDialog.SelectedPath;
                tb_table_path.Text = path;
                CCommon.SetValue(CCommon.key_tableP, path);

            }
        }

        private void cb_fileType_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox c = sender as CheckBox;
            switch (c.Name)
            {
                case "cb_excel": CCommon.SetValue(CCommon.key_fileExcel, "1"); break;
                case "cb_txt": CCommon.SetValue(CCommon.key_fileTxt, "1"); break;
                case "cb_code": CCommon.SetValue(CCommon.key_fileCode, "1"); break;
                default:
                    break;
            }
        }

        private void cb_fileType_UnChecked(object sender, RoutedEventArgs e)
        {
            CheckBox c = sender as CheckBox;
            switch (c.Name)
            {
                case "cb_excel": CCommon.SetValue(CCommon.key_fileExcel, "0"); break;
                case "cb_txt": CCommon.SetValue(CCommon.key_fileTxt, "0"); break;
                case "cb_code": CCommon.SetValue(CCommon.key_fileCode, "0"); break;
                default:
                    break;
            }
        }
    }
}
