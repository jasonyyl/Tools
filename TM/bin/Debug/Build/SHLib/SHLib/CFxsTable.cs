//==========================
// Author: TM         
// DateTime: 2017-08-25       
//==========================
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Mga.SHLib
{ 
    #region TableInfoRow

#if !USING_BINARY_TABLE
    class CFxsTableInfoRow
    {
        public int ID;
        public string Fx;
        public int LoopType;
        public int IsAttach;
        public int CameraType;
        public int Duration;
        public float Scale;
        public bool IsStop;
        public string Bone;
        public string Offset;
        public string Rotate;
        public CFxsTableInfoRow()
        {
            ID = 0;
            Fx = string.Empty;
            LoopType = 0;
            IsAttach = 0;
            CameraType = 0;
            Duration = 0;
            Scale = 0;
            IsStop = false;
            Bone = string.Empty;
            Offset = string.Empty;
            Rotate = string.Empty;
        }
    }

#endif
    #endregion

    #region Table

    public partial class CFxsTable : SingletonResetable<CFxsTable>, ITable
    {
        public string TableName
        {
            get { return "Fxs.txt"; }
        }

#if !USING_BINARY_TABLE
        public bool LoadTable(string sTablePath)
        {
            ggc.Foundation.CMapT<int, CFxsTableInfoRow> rows;
            using (ggc.Foundation.CTable table = new ggc.Foundation.CTable())
            {
                rows = new ggc.Foundation.CMapT<int, CFxsTableInfoRow>();
                string sFilename = sTablePath + "Fxs.txt";
                if (table.LoadFile<int, CFxsTableInfoRow>(sFilename, ref rows) == false)
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
