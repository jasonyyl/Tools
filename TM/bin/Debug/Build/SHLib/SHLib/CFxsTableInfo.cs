//==========================
// Author: TM         
// DateTime: 2017-08-25       
//==========================
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Mga.SHLib
{
    #region TableInfo
    public class CFxsTableInfo : CBaseTableInfo
    {
        #region Fields

        public const int TableInfoVer = 1;
        int m_ID;
        string m_Fx;
        int m_LoopType;
        int m_IsAttach;
        int m_CameraType;
        int m_Duration;
        float m_Scale;
        bool m_IsStop;
        string m_Bone;
        string m_Offset;
        string m_Rotate;

        #endregion

        #region Properties

        public override int Version { get { return TableInfoVer; } }
        public int ID { get { return m_ID; } private set { m_ID = value; }}
        public string Fx { get { return m_Fx; } private set { m_Fx = value; }}
        public int LoopType { get { return m_LoopType; } private set { m_LoopType = value; }}
        public int IsAttach { get { return m_IsAttach; } private set { m_IsAttach = value; }}
        public int CameraType { get { return m_CameraType; } private set { m_CameraType = value; }}
        public int Duration { get { return m_Duration; } private set { m_Duration = value; }}
        public float Scale { get { return m_Scale; } private set { m_Scale = value; }}
        public bool IsStop { get { return m_IsStop; } private set { m_IsStop = value; }}
        public string Bone { get { return m_Bone; } private set { m_Bone = value; }}
        public string Offset { get { return m_Offset; } private set { m_Offset = value; }}
        public string Rotate { get { return m_Rotate; } private set { m_Rotate = value; }}
        
        #endregion

        #region Serializable

        public override bool SerializeTo(ggc.Foundation.CStream stream)
        {
            base.SerializeTo(stream);
            stream.WriteT(m_ID);
            stream.WriteT(m_Fx);
            stream.WriteT(m_LoopType);
            stream.WriteT(m_IsAttach);
            stream.WriteT(m_CameraType);
            stream.WriteT(m_Duration);
            stream.WriteT(m_Scale);
            stream.WriteT(m_IsStop);
            stream.WriteT(m_Bone);
            stream.WriteT(m_Offset);
            stream.WriteT(m_Rotate);
            return true;
        }

        public override bool UnserializeFrom(ggc.Foundation.CStream stream)
        {
            if (!base.UnserializeFrom(stream))
            {
                ggc.Foundation.Log.LogErrorMsg(string.Format("CFxsTableInfo Version is wrong: stream version {0}, local version {1}", StreamVersion, Version));
                return false;
            }
            stream.ReadT(out m_ID);
            stream.ReadT(out m_Fx);
            stream.ReadT(out m_LoopType);
            stream.ReadT(out m_IsAttach);
            stream.ReadT(out m_CameraType);
            stream.ReadT(out m_Duration);
            stream.ReadT(out m_Scale);
            stream.ReadT(out m_IsStop);
            stream.ReadT(out m_Bone);
            stream.ReadT(out m_Offset);
            stream.ReadT(out m_Rotate);
            return true;
        }
        #endregion

        #region Funcs
      
#if !USING_BINARY_TABLE
        internal bool AssignByTableData(CFxsTableInfoRow row)
        {
            m_ID = row.ID;
            m_Fx = row.Fx;
            m_LoopType = row.LoopType;
            m_IsAttach = row.IsAttach;
            m_CameraType = row.CameraType;
            m_Duration = row.Duration;
            m_Scale = row.Scale;
            m_IsStop = row.IsStop;
            m_Bone = row.Bone;
            m_Offset = row.Offset;
            m_Rotate = row.Rotate;
            return true;
        }

#endif
        #endregion

    }
    #endregion

    #region Table
    public partial class CFxsTable : SingletonResetable<CFxsTable>, ITable
    {
        #region Fields
        ggc.Foundation.CMapT<int, CFxsTableInfo> m_mapFxsTableInfos;
        #endregion

        #region Funcs

        public CFxsTable()
        {
            m_mapFxsTableInfos = new ggc.Foundation.CMapT<int, CFxsTableInfo>();
        }

        public CFxsTableInfo GetFxsTableInfo(int tableId)
        {
            return m_mapFxsTableInfos.Find(tableId);
        }

        public ggc.Foundation.CMapT<int, CFxsTableInfo>.Enumerator GetEnumerator()
        {
            return m_mapFxsTableInfos.GetEnumerator();
        }

        public ggc.Foundation.CListT<CFxsTableInfo> GetFxsTableInfoList()
        {
            ggc.Foundation.CListT<CFxsTableInfo> list = new ggc.Foundation.CListT<CFxsTableInfo>();
            foreach (var item in m_mapFxsTableInfos)
            {
                if (item.Value != null)
                    list.Add(item.Value);
            }
            return list;
        }

        public bool CheckTable()
        {
            bool bRet = true;
            return bRet;
        }

        public void Dispose()
        {
            m_mapFxsTableInfos.Clear();
        }

#if !USING_BINARY_TABLE
        bool _PostLoad(ggc.Foundation.CMapT<int, CFxsTableInfoRow> rows)
        {
            if (rows == null)
                return false;
            m_mapFxsTableInfos.Clear();
            return true;
        }
#endif
        #endregion

        #region Table Serialization
        public bool LoadFromBinary(byte[] content)
        {
            m_mapFxsTableInfos.Clear();
            ggc.Foundation.CStream stream = new ggc.Foundation.CStream(content, 0, 0, (uint)content.Length);

            // 1. Get Version
            int iVersion = 0;
            stream.ReadT(out iVersion);
            if (iVersion != CFxsTableInfo.TableInfoVer)
            {
                ggc.Foundation.Log.LogErrorMsg(string.Format("[Table] {0} binary file version doesnt match", TableName));
                return false;
            }

            // 2. load entry
            int iCount = 0;
            stream.ReadT(out iCount);
            for (int i = 0; i < iCount; i++)
            {
                CFxsTableInfo tableInfo = new CFxsTableInfo();
                if (!tableInfo.UnserializeFrom(stream))
                {
                    ggc.Foundation.Log.LogErrorMsg("Fail to Unserialize From: " + TableName);
                    return false;
                }
                m_mapFxsTableInfos.Add(tableInfo.UniqueID, tableInfo);
             }
            return true;
        }

        public bool SaveToBinary(string tablePath)
        {
            int buffSize = 8 * 1024 * 1024;
            byte[] buff = new byte[buffSize];
            ggc.Foundation.CStream stream = new ggc.Foundation.CStream(buff, 0);

            // 1. Get Version to stream
            stream.WriteT(CFxsTableInfo.TableInfoVer);

            // 2. Write entries to stream
            stream.WriteT(m_mapFxsTableInfos.Count);
            foreach (var keyValuePair in m_mapFxsTableInfos)
            {
                var entry = keyValuePair.Value;
                if (!entry.SerializeTo(stream))
                {
                    ggc.Foundation.Log.LogErrorMsg("Fail to Serialize " + TableName);
                    return false;
                }
            }
            // 3. write to file
            System.IO.FileStream fs = new System.IO.FileStream(tablePath + TableName, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            System.IO.BinaryWriter writer = new System.IO.BinaryWriter(fs);
            writer.Write(stream.ByteData, 0, (int)stream.TotalSizeWritten());
            writer.Close();
            fs.Close();

            return true;
        }
        #endregion
    }
    #endregion
}
