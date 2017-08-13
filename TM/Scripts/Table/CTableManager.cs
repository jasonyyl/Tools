using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

namespace TM
{
    public enum ETableFlag
    {
        Invaild,
        Open_Excel_Failure,
        Sheet_Not_Exist,
        Can_Not_Find_InitKey,
        Success,
    }
    public class CTableManager
    {
        #region Fields
        bool m_bIsExporting = false;
        #endregion

        public CTableManager()
        {
        }
        public bool ExportTable(string path)
        {
            if (m_bIsExporting)
            {
                Clog.Instance.LogError("请等待上一次导出完成");
                return false;
            }
            m_bIsExporting = true;
            if (!CExcelManager.Instance.Open(path))
            {
                m_bIsExporting = false;
                Clog.Instance.LogError("Excel打开失败");
                return false;
            }
            Sheets sheets = CExcelManager.Instance.GetSheets();
            if (sheets == null)
            {
                m_bIsExporting = false;
                Clog.Instance.LogError("Excel找不到有效页签供导出");
                return false;
            }
            int sheetsCount = sheets.Count;
            for (int i = 2; i <= sheetsCount; i++)
            {
                Worksheet sheet = sheets[i];
                if (sheet.Name.Contains("~"))
                    continue;
                string exportPath = CCommon.GetValue(CCommon.key_tableEP);
                if (!CFileManager.DirectorExist(exportPath))
                {
                    Clog.Instance.LogError("导出路径不存在:" + exportPath);
                    continue;
                }
                CTableTemplate tableTemplate = new CTableTemplate(sheet);
                if (tableTemplate == null)
                {
                    Clog.Instance.LogError(sheet.Name + "导出失败");
                    continue;
                }
                string excelFilePath = Path.Combine(exportPath, sheet.Name + ".txt");
                FileStream fs = CFileManager.Open(excelFilePath, FileMode.Create);
                StreamWriter st = new StreamWriter(fs);
                StringBuilder s = new StringBuilder();
                CCell sheetBound = tableTemplate.RangeBound;
                Char end = (Char)(sheetBound.Column + 64);
                string strEnd = end.ToString() + sheetBound.Row;
                Range cells = sheet.Range["A1", strEnd].Cells;
                object[,] realCells = cells.Value;
                for (int r = 1; r <= sheetBound.Row; r++)
                {
                    s.Clear();
                    string cellValue = string.Empty;
                    object o = realCells[r, 1];
                    if (o != null)
                        cellValue = o.ToString();
                    if (cellValue == CCommon.StrSkipRows)
                        continue;
                    for (int c = 1; c <= sheetBound.Column; c++)
                    {
                        cellValue = string.Empty;
                        o = realCells[r, c];
                        if (o != null)
                            cellValue = o.ToString();
                        s.Append(cellValue);
                        s.Append(",");
                    }
                    st.WriteLine(s.ToString());
                }
                st.Close();
                fs.Close();
                st.Dispose();
                fs.Dispose();
                Clog.Instance.Log(sheet.Name + "导出成功");
            }
            m_bIsExporting = false;
            return true;
        }

        public void ExportTables(string dir)
        {
            string[] files = CFileManager.GetFiles(dir, "*.xlsm", SearchOption.AllDirectories);
            foreach (var f in files)
            {
                if (f.Contains("~$"))
                    continue;
                ExportTable(f);
            }
        }
    }
}
