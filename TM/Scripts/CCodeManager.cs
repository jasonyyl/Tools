using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TM
{
    public class CCodeManager
    {
        bool m_bIsGenerate = false;
        public bool CreateCode(string path)
        {
            if (m_bIsGenerate)
            {
                Clog.Instance.LogError("请等待上一次生成代码完成");
                return false;
            }
            m_bIsGenerate = true;
            if (!CExcelManager.Instance.Open(path))
            {
                m_bIsGenerate = false;
                Clog.Instance.LogError("Excel打开失败");
                return false;
            }
            Sheets sheets = CExcelManager.Instance.GetSheets();
            if (sheets == null)
            {
                m_bIsGenerate = false;
                Clog.Instance.LogError("Excel找不到有效页签供生成代码");
                return false;
            }
            int sheetsCount = sheets.Count;
            for (int i = 2; i <= sheetsCount; i++)
            {
                Worksheet sheet = sheets[i];
                if (sheet.Name.Contains("~"))
                    continue;
                string exportPath = CCommon.GetValue(CCommon.key_codeEP);
                if (!CFileManager.DirectorExist(exportPath))
                {
                    Clog.Instance.LogError("导出路径不存在:" + exportPath);
                    continue;
                }
                CTableTemplate tableTemplate = new CTableTemplate(sheet);
                if (tableTemplate == null)
                {
                    Clog.Instance.LogError(sheet.Name + "生成失败");
                    continue;
                }
                #region 1.write table

                FileStream fs;
                StreamWriter st;
                bool isOverride = true;
                List<string> dataName = tableTemplate.GetRowContent(CTableTemplate.KeyColumn);
                List<string> dataType = tableTemplate.GetRowContent(CTableTemplate.KeyDataType);
                if (dataName == null || dataType == null)
                {
                    Clog.Instance.LogError("列名和列类型获取失败");
                    return false;
                }
                string fileName = "C" + sheet.Name + "Table.cs";
                string excelFilePath = Path.Combine(exportPath, fileName);
                if (File.Exists(excelFilePath))
                {
                    DialogResult r = MessageBox.Show("是否覆盖文件 " + fileName, "", MessageBoxButtons.YesNo);
                    isOverride = r == DialogResult.Yes;
                }
                if (isOverride)
                {
                    string tableScript = CFileManager.ReadAllText("Template/TableTemplate.cs");
                    tableScript = tableScript.Replace("#author#", "TM");
                    tableScript = tableScript.Replace("#datetime#", DateTime.Now.ToString("yyyy-MM-dd"));
                    tableScript = tableScript.Replace("#tablename#", sheet.Name);
                    tableScript = tableScript.Replace("#tableinforow_fields#", GetTableRowInfoFields(dataName, dataType));
                    tableScript = tableScript.Replace("#tableinforow_init#", GetTableRowInfoInit(dataName, dataType));

                    fs = CFileManager.Open(excelFilePath, FileMode.Create);
                    st = new StreamWriter(fs);
                    st.Write(tableScript);
                    st.Close();
                    fs.Close();
                    st.Dispose();
                    fs.Dispose();
                }
                #endregion

                #region 2.write tableinfo

                isOverride = true;
                fileName = "C" + sheet.Name + "TableInfo.cs";
                excelFilePath = Path.Combine(exportPath, fileName);
                string matchInit = string.Empty;
                string matchPro = string.Empty;
                if (File.Exists(excelFilePath))
                {
                    DialogResult r = MessageBox.Show("是否覆盖文件 " + fileName, "", MessageBoxButtons.YesNo);
                    isOverride = r == DialogResult.Yes;
                    string input = File.ReadAllText(excelFilePath);

                    Match match = Regex.Match(input, @"#region Init([\s\S]*)#endregion Init");
                    if (match.Groups.Count >= 2)
                    {
                        matchInit = match.Groups[1].ToString();
                        matchInit = matchInit.Replace("\r\n\r\n", "");
                        int idx = matchInit.IndexOf("public");
                        matchInit = matchInit.Substring(idx);
                    }
                    match = Regex.Match(input, @"#region Properties([\s\S]*)#endregion Properties");
                    if (match.Groups.Count >= 2)
                    {
                        matchPro = match.Groups[1].ToString();
                        matchPro = matchPro.Replace("\r\n\r\n", "");
                        int idx = matchPro.IndexOf("public");
                        matchPro = matchPro.Substring(idx);
                    }
                }
                if (isOverride)
                {
                    string tableInfoScript = CFileManager.ReadAllText("Template/TableInfoTemplate.cs");
                    tableInfoScript = tableInfoScript.Replace("#author#", "TM");
                    tableInfoScript = tableInfoScript.Replace("#datetime#", DateTime.Now.ToString("yyyy-MM-dd"));
                    tableInfoScript = tableInfoScript.Replace("#tablename#", sheet.Name);
                    tableInfoScript = tableInfoScript.Replace("#tableinfo_fields#", GetFields(dataName, dataType));
                    tableInfoScript = tableInfoScript.Replace("#tableinfo_serialize#", GetSerialize(dataName));
                    tableInfoScript = tableInfoScript.Replace("#tableinfo_unserialize#", GetUnSerialize(dataName));
                    tableInfoScript = tableInfoScript.Replace("#tableinfo_assign#", GetAssign(dataName));

                    if (string.IsNullOrEmpty(matchPro))
                        tableInfoScript = tableInfoScript.Replace("#tableinfo_properties#", GetPropertiess(dataName, dataType));
                    else
                        tableInfoScript = tableInfoScript.Replace("#tableinfo_properties#", matchPro);

                    if (string.IsNullOrEmpty(matchInit))
                        tableInfoScript = tableInfoScript.Replace("#tableinfo_init#", GetInit());
                    else
                        tableInfoScript = tableInfoScript.Replace("#tableinfo_init#", matchInit);

                    fs = CFileManager.Open(excelFilePath, FileMode.Create);
                    st = new StreamWriter(fs);
                    st.Write(tableInfoScript);
                    st.Close();
                    fs.Close();
                    st.Dispose();
                    fs.Dispose();
                }
                #endregion
                Clog.Instance.Log(sheet.Name + "生成成功");
            }
            m_bIsGenerate = false;
            return true;
        }

        #region table

        public string GetTableRowInfoInit(List<string> dataName, List<string> dataType)
        {
            int len = Math.Min(dataName.Count, dataType.Count);
            StringBuilder s = new StringBuilder();
            for (int i = 0; i < len; i++)
            {
                string equalsValue = string.Empty;
                switch (CDataType.ChangeToDataType(dataType[i]).ToLower())
                {
                    case CDataType.DataType_INT: equalsValue = "0"; break;
                    case CDataType.DataType_FLOAT: equalsValue = "0"; break;
                    case CDataType.DataType_STRING: equalsValue = "string.Empty"; break;
                    case CDataType.DataType_BOOL: equalsValue = "false"; break;
                    case CDataType.DataType_VECTOR2: equalsValue = "new Vector2(0,0)"; break;
                    case CDataType.DataType_VECTOR3: equalsValue = "new Vector3(0,0,0)"; break;
                    default:
                        equalsValue = string.Empty;
                        break;
                }
                s.Append(dataName[i] + " = " + equalsValue + ";");
                if (i == len - 1)
                    continue;
                s.Append("\n");
                s.Append("            ");
            }
            return s.ToString();

        }

        public string GetTableRowInfoFields(List<string> dataName, List<string> dataType)
        {
            int len = Math.Min(dataName.Count, dataType.Count);
            StringBuilder s = new StringBuilder();
            for (int i = 0; i < len; i++)
            {
                s.Append("public " + CDataType.ChangeToDataType(dataType[i]) + " " + dataName[i] + ";");
                if (i == len - 1)
                    continue;
                s.Append("\n");
                s.Append("        ");
            }
            return s.ToString();

        }
        #endregion

        #region table info

        public string GetFields(List<string> dataName, List<string> dataType)
        {
            int len = Math.Min(dataName.Count, dataType.Count);
            StringBuilder s = new StringBuilder();
            for (int i = 0; i < len; i++)
            {
                s.Append(CDataType.ChangeToDataType(dataType[i]) + " m_" + dataName[i] + ";");
                if (i == len - 1)
                    continue;
                s.Append("\n");
                s.Append("        ");
            }
            return s.ToString();
        }
        public string GetPropertiess(List<string> dataName, List<string> dataType)
        {
            int len = Math.Min(dataName.Count, dataType.Count);
            StringBuilder s = new StringBuilder();
            s.Append("public override int Version { get { return TableInfoVer; } }\n        ");
            for (int i = 0; i < len; i++)
            {
                if (i == 0 && CDataType.ChangeToDataType(dataType[i]) == "int")
                    s.Append("public int UniqueID { get { return " + dataName[i] + "; } set { " + dataName[i] + " = value; } }\n        ");
                s.Append("public " + CDataType.ChangeToDataType(dataType[i]) + " " + dataName[i] + " { get { return m_" + dataName[i] + "; } private set { m_" + dataName[i] + " = value; }}");
                if (i == len - 1)
                    continue;
                s.Append("\n");
                s.Append("        ");
            }
            return s.ToString();
        }

        public string GetInit()
        {
            StringBuilder b = new StringBuilder();
            b.Append("public void Initialize()");
            b.Append("\n        {");
            b.Append("\n        }");
            return b.ToString();
        }
        public string GetSerialize(List<string> dataName)
        {
            int len = dataName.Count;
            StringBuilder s = new StringBuilder();
            for (int i = 0; i < len; i++)
            {
                s.Append("stream.WriteT(m_" + dataName[i] + ");");
                if (i == len - 1)
                    continue;
                s.Append("\n");
                s.Append("            ");
            }
            return s.ToString();
        }
        public string GetUnSerialize(List<string> dataName)
        {
            int len = dataName.Count;
            StringBuilder s = new StringBuilder();
            for (int i = 0; i < len; i++)
            {
                s.Append("stream.ReadT(out m_" + dataName[i] + ");");
                if (i == len - 1)
                    continue;
                s.Append("\n");
                s.Append("            ");
            }
            return s.ToString();
        }
        public string GetAssign(List<string> dataName)
        {
            int len = dataName.Count;
            StringBuilder s = new StringBuilder();
            for (int i = 0; i < len; i++)
            {
                s.Append("m_" + dataName[i] + " = row." + dataName[i] + ";");
                if (i == len - 1)
                    continue;
                s.Append("\n");
                s.Append("            ");
            }
            return s.ToString();
        }
        #endregion
    }
}
