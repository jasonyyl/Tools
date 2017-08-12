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
                string excelFilePath = Path.Combine(exportPath, sheet.Name + ".txt");
                using (FileStream fs = File.Open(excelFilePath, FileMode.Create))
                {
                    StreamWriter st = new StreamWriter(fs);
                    StringBuilder s = new StringBuilder();
                    CCell sheetBound = tableTemplate.RangeBound;
                    Char end = (Char)(sheetBound.Column + 64);
                    string strEnd = end.ToString() + sheetBound.Row;
                    Range cells = sheet.Range["A1", strEnd].Cells;
                    object[,] realCells = cells.Value;
                    for (int r = 1; r <= sheetBound.Row; r++)
                    {
                        object o = realCells[r, 1];
                        if (o == null)
                            continue;
                        string cellValue = o.ToString();
                        if (cellValue == "SkipRow")
                            continue;
                        s.Clear();
                        for (int c = 1; c <= sheetBound.Column; c++)
                        {
                            o = realCells[r, c];
                            if (o == null)
                                continue;
                            cellValue = o.ToString();
                            s.Append(cellValue);
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
            Range usedRange = sheet.UsedRange;
            foreach (var k in m_KeysInit)
            {
                Range r = usedRange.Find(k, Missing.Value, Missing.Value, Missing.Value, Missing.Value, XlSearchDirection.xlPrevious, true, true);
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
                    Range rColumn = usedRange[rowIndexOfColumn, increaseColumns];

                    if (rColumn == null || rColumn.Value == null)
                        break;
                    string cellValue = rColumn.Value.ToString();
                    if (string.IsNullOrEmpty(cellValue))
                        break;
                    if (rowIndexOfExportTarget > 0)
                    {
                        if (increaseColumns > 1)
                        {

                            Range rExportTarget = usedRange[rowIndexOfExportTarget, increaseColumns];
                            if (rExportTarget.Value != null)
                            {
                                int result = 0;
                                cellValue = rExportTarget.Value.ToString();
                                int.TryParse(cellValue, out result);
                                template.ExportTarget.Add((CTableTemplate.EExportTarget)result);
                            }
                        }
                    }
                    if (rowIndexOfDataType > 0)
                    {
                        Range rDataType = usedRange[rowIndexOfDataType, increaseColumns];
                        if (rDataType.Value != null)
                        {
                            cellValue = rDataType.Value.ToString();
                            if (increaseColumns > 1)
                                template.DataType.Add(cellValue);
                        }

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
