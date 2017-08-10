using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace TM
{
    public class CTableManager
    {
        string[] m_Initkeys = new string[] { "NumDataRows", "Column", "DataBegin", "DataEnd" };
        Dictionary<string, CCell> m_KeysMatch = new Dictionary<string, CCell>();
        public CTableManager()
        {

        }
        public bool ExportTable(string path)
        {
            #region 1.find keys
            if (!CExcelManager.Instance.Open(path))
                return false;
            Sheets sheets = CExcelManager.Instance.GetSheets();
            if (sheets == null)
                return false;
            int sheetsCount = sheets.Count;
            for (int i = 2; i <= sheetsCount; i++)
            {
                Worksheet sheet = sheets[i];
                if (sheet.Name.Contains("~"))
                    continue;
                m_KeysMatch.Clear();

                int vaildRowIndex = 0;
                int vaildColoumIndex = 0;
                foreach (var k in m_Initkeys)
                {
                    Range r = sheet.UsedRange.Find(k, Missing.Value);
                    if (r != null)
                    {
                        if (!m_KeysMatch.ContainsKey(k))
                            m_KeysMatch.Add(k, new CCell(1, 1));
                        m_KeysMatch[k].Row = r.Row;
                        m_KeysMatch[k].Column = r.Row;
                    }
                    else
                    {
                        return false;
                    }
                }
                int row = 1;
                if (m_KeysMatch.ContainsKey("Column"))
                    row = m_KeysMatch["Column"].Row;
                int increaseColumns = 1;
                int allColumns = sheet.UsedRange.Columns.Count;
                while (increaseColumns < allColumns)
                {
                    Range r = sheet.UsedRange[row, increaseColumns];
                    if (r == null || string.IsNullOrEmpty(r.Text))
                        break;
                    increaseColumns++;
                }

                if (m_KeysMatch.ContainsKey("DataEnd"))
                    vaildRowIndex = m_KeysMatch["DataEnd"].Row;
                vaildColoumIndex = increaseColumns;

            }
            #endregion

            #region 2.

            #endregion
            return true;
        }

        public void ExportTables(string dir)
        {

        }
    }
}
