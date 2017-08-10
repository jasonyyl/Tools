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
    public class CExcelManager
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

        public void Open(string path)
        {
            if (File.Exists(path))
            {
                m_WorkBook = m_App.Workbooks.Open(path, m_Nothing, m_Nothing, m_Nothing, m_Nothing, m_Nothing, m_Nothing, m_Nothing, m_Nothing, m_Nothing, m_Nothing, m_Nothing, m_Nothing, m_Nothing, m_Nothing);
            }
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
        public void Close()
        {
            try
            {
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
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("close failure");
                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadKey();
            }
        }
    }
}
