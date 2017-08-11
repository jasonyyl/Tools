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
        public CCell RangeBound;
        public List<string> DataType;
        public List<EExportTarget> ExportTarget;

        public CTableTemplate()
        {
            RangeBound = new CCell(1, 1);
            DataType = new List<string>();
            ExportTarget = new List<EExportTarget>();

        }
    }
}
