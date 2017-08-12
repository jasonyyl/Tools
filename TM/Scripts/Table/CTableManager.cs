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

        string m_KeyNumDataRows = "NumDataRows";
        string m_KeyColumn = "Column";
        string m_KeyDataType = "Datatype";
        string m_KeyExportType = "ExportTarget";
        string m_KeyDataBegin = "DataBegin";
        string m_KeyDataEnd = "DataEnd";
        string[] m_KeysInit;
        bool m_bIsExporting = false;
        Dictionary<string, CCell> m_KeysMatch = new Dictionary<string, CCell>();

        #endregion

        public CTableManager()
        {
            m_KeysInit = new string[] { m_KeyNumDataRows, m_KeyColumn, m_KeyDataType, m_KeyExportType, m_KeyDataBegin, m_KeyDataEnd };
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
                CTableTemplate tableTemplate = _GetTableTemplate(sheet);
                if (tableTemplate == null)
                {
                    Clog.Instance.LogError(sheet.Name + "导出失败");
                    continue;
                }
                using (FileStream fs = CFileManager.Open(Path.Combine(exportPath, sheet.Name + ".txt"), FileMode.Create))
                {
                    StreamWriter st = new StreamWriter(fs);
                    StringBuilder s = new StringBuilder();
                    CCell sheetBound = tableTemplate.RangeBound;
                    for (int r = 1; r <= sheetBound.Row; r++)
                    {
                        Range cellRowStart = sheet.UsedRange.Cells[r, 1];
                        if (cellRowStart != null && cellRowStart.Text == "SkipRow")
                            continue;
                        s.Clear();
                        for (int c = 1; c <= sheetBound.Column; c++)
                        {
                            Range cell = sheet.UsedRange.Cells[r, c];
                            #region for filter datatype process
                            string dataType = string.Empty;
                            if (c >= 2)
                                dataType = tableTemplate.DataType[c - 2].ToLower();
                            if (dataType == CDataType.DataType_STRING)
                            {
                                s.Append("\"");
                                s.Append(cell.Text);
                                s.Append("\"");
                            }
                            else
                            {
                                s.Append(cell.Text);
                            }
                            #endregion
                            s.Append(",");
                        }
                        st.WriteLine(s.ToString());
                    }
                }
                Clog.Instance.Log(sheet.Name + "导出成功");
            }
            m_bIsExporting = false;
            return true;
        }

        public void ExportTables(string dir)
        {

        }
        CTableTemplate _GetTableTemplate(Worksheet sheet)
        {
            CTableTemplate template = new CTableTemplate();
            m_KeysMatch.Clear();
            int vaildRowIndex = 0;
            int vaildColoumIndex = 0;
            foreach (var k in m_KeysInit)
            {
                Range r = sheet.UsedRange.Find(k, Missing.Value, Missing.Value, Missing.Value, Missing.Value, XlSearchDirection.xlPrevious, true, true);
                if (r == null)
                {
                    if (k == m_KeyExportType)
                        continue;
                    Clog.Instance.LogError("Excel页签" + sheet.Name + "不存在必须初始化字段:" + k);
                    return null;
                }
                if (!m_KeysMatch.ContainsKey(k))
                    m_KeysMatch.Add(k, new CCell(1, 1));
                m_KeysMatch[k].Row = r.Row;
                m_KeysMatch[k].Column = r.Column;
            }
            int rowIndexOfColumn = 0;
            int rowIndexOfExportTarget = 0;
            int rowIndexOfDataType = 0;

            if (m_KeysMatch.ContainsKey(m_KeyColumn))
                rowIndexOfColumn = m_KeysMatch[m_KeyColumn].Row;
            if (m_KeysMatch.ContainsKey(m_KeyExportType))
                rowIndexOfExportTarget = m_KeysMatch[m_KeyExportType].Row;
            if (m_KeysMatch.ContainsKey(m_KeyDataType))
                rowIndexOfDataType = m_KeysMatch[m_KeyDataType].Row;
            if (m_KeysMatch.ContainsKey(m_KeyDataEnd))
                vaildRowIndex = m_KeysMatch[m_KeyDataEnd].Row;

            if (rowIndexOfColumn > 0)
            {
                int increaseColumns = 1;
                int allColumns = sheet.UsedRange.Columns.Count;
                while (increaseColumns < allColumns)
                {
                    Range rColumn = sheet.UsedRange[rowIndexOfColumn, increaseColumns];
                    if (rColumn == null || string.IsNullOrEmpty(rColumn.Text))
                        break;
                    if (rowIndexOfExportTarget > 0)
                    {
                        if (increaseColumns > 1)
                        {
                            Range rExportTarget = sheet.UsedRange[rowIndexOfExportTarget, increaseColumns];
                            int result = 0;
                            int.TryParse(rExportTarget.Text, out result);
                            template.ExportTarget.Add((CTableTemplate.EExportTarget)result);
                        }
                    }
                    if (rowIndexOfDataType > 0)
                    {
                        Range rDataType = sheet.UsedRange[rowIndexOfDataType, increaseColumns];
                        if (increaseColumns > 1)
                            template.DataType.Add(rDataType.Text);

                    }
                    increaseColumns++;
                }
                vaildColoumIndex = increaseColumns - 1;
            }
            template.RangeBound.Row = vaildRowIndex;
            template.RangeBound.Column = vaildColoumIndex;
            return template;
        }
    }
}
