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
    public class CTableTemplate
    {
        public enum EExportTarget
        {
            Both = 0,
            Client,
            Server,
        }

        #region Fields
        string m_KeyNumDataRows = "NumDataRows:";
        string m_KeyColumn = "Column:";
        string m_KeyDataType = "Datatype:";
        string m_KeyExportType = "ExportTarget:";
        string m_KeyDataBegin = "DataBegin:";
        string m_KeyDataEnd = "DataEnd:";
        string[] m_Keys;

        public CCell RangeBound;
        public List<string> DataName;
        public List<string> DataType;
        public List<EExportTarget> ExportTarget;
        public Dictionary<string, CCell> KeysInit;
        #endregion

        public CTableTemplate(Worksheet sheet)
        {
            RangeBound = new CCell(1, 1);
            DataName = new List<string>();
            DataType = new List<string>();
            ExportTarget = new List<EExportTarget>();
            KeysInit = new Dictionary<string, CCell>();
            m_Keys = new string[] { m_KeyNumDataRows, m_KeyColumn, m_KeyDataType, m_KeyExportType, m_KeyDataBegin, m_KeyDataEnd };
            Init(sheet);
        }

        void Init(Worksheet sheet)
        {
            #region 1.find key
            object[,] usedRange = sheet.UsedRange.Value;
            int rLength = usedRange.GetLength(0);
            int cLength = usedRange.GetLength(1);
            for (int i = 1; i <= rLength; i++)
            {
                for (int j = 1; j <= cLength; j++)
                {
                    object o = usedRange[i, j];
                    if (o != null)
                    {
                        foreach (var initKey in m_Keys)
                        {
                            if (initKey == o.ToString())
                            {
                                if (!KeysInit.ContainsKey(initKey))
                                    KeysInit.Add(initKey, new CCell(1, 1));
                                KeysInit[initKey].Row = i;
                                KeysInit[initKey].Column = j;
                            }
                        }
                    }

                }
            }
            #endregion

            #region 2.init key
            int rowIndexOfColumn = 0;
            int rowIndexOfExportTarget = 0;
            int rowIndexOfDataType = 0;
            if (KeysInit.ContainsKey(m_KeyColumn))
                rowIndexOfColumn = KeysInit[m_KeyColumn].Row;
            if (KeysInit.ContainsKey(m_KeyExportType))
                rowIndexOfExportTarget = KeysInit[m_KeyExportType].Row;
            if (KeysInit.ContainsKey(m_KeyDataType))
                rowIndexOfDataType = KeysInit[m_KeyDataType].Row;
            if (KeysInit.ContainsKey(m_KeyDataEnd))
                RangeBound.Row = KeysInit[m_KeyDataEnd].Row;
            if (rowIndexOfColumn > 0)
            {
                int increaseColumns = 1;
                int allColumns = sheet.UsedRange.Columns.Count;
                while (increaseColumns < allColumns)
                {
                    object rColumn = usedRange[rowIndexOfColumn, increaseColumns];
                    if (rColumn == null)
                        break;
                    string cellValue = rColumn.ToString();
                    if (string.IsNullOrEmpty(cellValue))
                        break;
                    //1.export type
                    if (rowIndexOfExportTarget > 0)
                    {
                        if (increaseColumns > 1)
                        {
                            object rExportTarget = usedRange[rowIndexOfExportTarget, increaseColumns];
                            if (rExportTarget != null)
                            {
                                int result = 0;
                                cellValue = rExportTarget.ToString();
                                int.TryParse(cellValue, out result);
                                ExportTarget.Add((CTableTemplate.EExportTarget)result);
                            }
                        }
                    }
                    //2.data type
                    if (rowIndexOfDataType > 0)
                    {
                        object rDataName = usedRange[rowIndexOfColumn, increaseColumns];
                        object rDataType = usedRange[rowIndexOfDataType, increaseColumns];
                        if (rDataName != null)
                        {
                            cellValue = rDataName.ToString();
                            if (increaseColumns > 1)
                                DataName.Add(cellValue);
                        }
                        if (rDataType != null)
                        {
                            cellValue = rDataType.ToString();
                            if (increaseColumns > 1)
                                DataType.Add(cellValue);
                        }


                    }
                    increaseColumns++;
                }
                RangeBound.Column = increaseColumns - 1;
            }
            #endregion
        }
    }
}
