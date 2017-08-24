//==========================
// Author: #author#         
// DateTime: #datetime#       
//==========================
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Mga.SHLib
{ 
    #region TableInfoRow

#if !USING_BINARY_TABLE
    class C#tablename#TableInfoRow
    {
        #tableinforow_fields#
        public C#tablename#TableInfoRow()
        {
            #tableinforow_init#
        }
    }

#endif
    #endregion

    #region Table

    public partial class C#tablename#Table : SingletonResetable<C#tablename#Table>, ITable
    {
        public string TableName
        {
            get { return "#tablename#.txt"; }
        }

#if !USING_BINARY_TABLE
        public bool LoadTable(string sTablePath)
        {
            ggc.Foundation.CMapT<int, C#tablename#TableInfoRow> rows;
            using (ggc.Foundation.CTable table = new ggc.Foundation.CTable())
            {
                rows = new ggc.Foundation.CMapT<int, C#tablename#TableInfoRow>();
                string sFilename = sTablePath + "#tablename#.txt";
                if (table.LoadFile<int, C#tablename#TableInfoRow>(sFilename, ref rows) == false)
                {
                    ggc.Foundation.Log.LogErrorMsg("Failed loading table - '" + sFilename + "'");
                    return false;
                }
                else
                {
                    ggc.Foundation.Log.LogMsg("Table - '" + sFilename + "' loaded.");
                }
            }
            return _PostLoad(rows);
        }

#else
        public bool LoadTable(string sTablePath)
        {
            return false;
        }

#endif

    };
    #endregion
}
