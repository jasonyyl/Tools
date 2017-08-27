using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TM
{
    public class CCell
    {
        public int Row;
        public int Column;
        public string Content;
        public CCell(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
    public class CRow
    {
        public int RowIndex;
        public string RowKey;
        public List<string> RowContent;
        public CRow()
        {
            RowIndex = 0;
            RowKey = string.Empty;
            RowContent = new List<string>();
        }
    }
    public class CTableTemplate
    {
        #region Fields
        public static string KeyBinary = "Binary:";
        public static string KeyColumn = "Column:";
        public static string KeyCheckRule = "CheckRule:";
        public static string KeyDefault = "Default:";
        public static string KeyDataType = "Datatype:";
        public static string KeyDataTypeDefine = "DataTypeDefine:";
        public static string KeyDataBegin = "DataBegin:";
        public static string KeyDataEnd = "DataEnd:";
        public static string KeyExportType = "ExportTarget:";
        public static string KeyNumDataRows = "NumDataRows:";
        public static string KeyNotNull = "NotNull:";
        public static string KeySchemaVersion = "SchemaVersion:";
        public static string KeyUnique = "Unique:";
        public static string KeyUnsigned = "Unsigned:";
        public static string KeyZeroFill = "ZeroFill:";


        public CCell RangeBound;
        public Dictionary<int, CRow> DataContent;

        object[,] m_UsedDataContent;
        List<List<string>> m_DataContent;
        #endregion

        #region Public
        public CTableTemplate(Worksheet sheet)
        {
            RangeBound = new CCell(1, 1);
            DataContent = new Dictionary<int, CRow>();
            m_UsedDataContent = sheet.UsedRange.Value;
            _Init();
        }
        public List<string> GetRowContent(string key)
        {
            foreach (var row in DataContent)
            {
                if (row.Value.RowKey == key)
                {
                    List<string> d = row.Value.RowContent;
                    return d.GetRange(1, RangeBound.Column - 1);
                }
            }
            return null;
        }
        public List<List<string>> GetDataConetent(string key)
        {
            if (m_DataContent == null)
                m_DataContent = new List<List<string>>();
            m_DataContent.Clear();
            int len = RangeBound.Row;
            bool isStart = false;
            for (int i = 0; i < len; i++)
            {
                if (DataContent[i].RowKey == KeyDataBegin)
                    isStart = true;
                if (isStart)
                {
                    List<string> d = DataContent[i].RowContent;
                    m_DataContent.Add(d.GetRange(1, RangeBound.Column - 1));
                }
            }
            return null;
        }
        #endregion

        #region Private
        void _Init()
        {
            int rLength = m_UsedDataContent.GetLength(0);
            int cLength = m_UsedDataContent.GetLength(1);
            for (int i = 1; i <= rLength; i++)
            {
                object r = m_UsedDataContent[i, 1];
                CRow row = new CRow();
                row.RowIndex = i;
                if (r != null)
                    row.RowKey = r.ToString();
                DataContent.Add(i, row);
                for (int j = 1; j <= cLength; j++)
                {
                    object c = m_UsedDataContent[i, j];
                    if (c != null)
                    {
                        row.RowContent.Add(c.ToString());
                        if (r != null && KeyColumn == r.ToString())
                        {
                            RangeBound.Column = j;
                        }
                        if (KeyDataEnd == c.ToString())
                        {
                            RangeBound.Row = i;
                        }
                    }
                    else
                    {
                        row.RowContent.Add(string.Empty);
                    }

                }
            }
            Clog.Instance.LogError("行:" + RangeBound.Row + "列:" + RangeBound.Column);
        }
        #endregion
    }
}
