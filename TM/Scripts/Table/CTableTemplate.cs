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
        public int DataCount;
    }
}
