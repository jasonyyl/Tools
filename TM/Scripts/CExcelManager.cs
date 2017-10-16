using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace TM
{
    public class CExcelManager : CSingleton<CExcelManager>
    {
        #region Field
        readonly object m_Nothing = System.Reflection.Missing.Value;
        Application m_App = null;
        Workbook m_WorkBook = null;
        bool m_IsOpened = false;
        #endregion
        public CExcelManager()
        {
            try
            {
                m_App = (Application)Marshal.GetActiveObject("Excel.Application");
                m_App.Visible = false;
                m_IsOpened = true;
                m_App.DisplayAlerts = false;
            }
            catch
            {
                m_App = new Application();
                m_IsOpened = true;
            }
        }

        public bool Open(string path)
        {
            string fullPath = Path.GetFullPath(path);
            if (!CFileManager.FileExist(fullPath))
                return false;
            try
            {
                m_WorkBook = m_App.Workbooks.Open(fullPath, m_Nothing, m_Nothing, m_Nothing, m_Nothing, m_Nothing, m_Nothing, m_Nothing, m_Nothing, m_Nothing, m_Nothing, m_Nothing, m_Nothing, m_Nothing, m_Nothing);

            }
            catch (Exception)
            {
                return false;
            }
            return m_WorkBook != null;
        }
        public Worksheet GetSheet(string sheetName)
        {
            if (m_WorkBook != null)
            {
                Worksheet ws = (Worksheet)m_WorkBook.Worksheets[sheetName];
                return ws;

            }
            return null;

        }
        public Worksheet GetSheet(int index)
        {
            if (index <= 0)
                index = 1;
            if (m_WorkBook != null)
            {
                Worksheet ws = (Worksheet)m_WorkBook.Worksheets[index];
                return ws;

            }
            return null;

        }

        public Sheets GetSheets()
        {
            if (m_WorkBook != null)
            {
                return m_WorkBook.Worksheets;
            }
            return null;
        }
        public void Close()
        {
            try
            {
                if (m_WorkBook != null)
                    m_WorkBook.Save();
                if (m_IsOpened)
                {
                    m_WorkBook.Close(true);
                    m_WorkBook = null;
                    m_App.Quit();
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
