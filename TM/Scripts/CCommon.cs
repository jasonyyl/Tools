using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TM
{
    public static class CCommon
    {
        public static string key_tableP = "tableExcelPath";
        public static string key_codeEP = "codeExportPath";
        public static string key_tableEP = "tableExportPath";
        public static string key_tableEBP = "tableExportBinaryPath";
        public static string key_fileExcel = "ftExcel";
        public static string key_fileTxt = "ftText";
        public static string key_fileCode = "ftCode";
        public static string StrFolderIconPath = "Images/Icons/folder.png";
        public static string StrExcelIconPath = "Images/Icons/excel.png";
        public static string StrSkipRows = "SkipRow";

        public static string GetValue(string key)
        {
            string v = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrEmpty(v))
                return string.Empty;
            return v;
        }
        public static void SetValue(string key, string value)
        {
            Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            cfa.AppSettings.Settings[key].Value = value;
            cfa.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
