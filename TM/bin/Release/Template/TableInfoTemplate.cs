//==============================================
// [Auto-Generated]
// DateTime: #datetime#
//==============================================
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Mga.SHLib
{
    #region TableInfo
    public class C#tablename#TableInfo : CBaseTableInfo
    {
        #region Fields
        public const int TableInfoVer = 1;
        #tableinfo_fields#

        #endregion

        #region Properties
        public override int Version { get { return TableInfoVer; } }
        #tableinfo_properties#
        #endregion

        #region Serializable

        public override bool SerializeTo(ggc.Foundation.CStream stream)
        {
            base.SerializeTo(stream);
            #tableinfo_serialize#
            return true;
        }

        public override bool UnserializeFrom(ggc.Foundation.CStream stream)
        {
            if (!base.UnserializeFrom(stream))
            {
                ggc.Foundation.Log.LogErrorMsg(string.Format("CFxsTableInfo Version is wrong: stream version {0}, local version {1}", StreamVersion, Version));
                return false;
            }
            #tableinfo_unserialize#
            return true;
        }
        #endregion

        #region Funcs
      
#if !USING_BINARY_TABLE
        internal bool AssignByTableData(C#tablename#TableInfoRow row)
        {
            #tableinfo_assign#
            return true;
        }

#endif
        #endregion

    }
    #endregion

    #region Table
    public partial class C#tablename#Table : SingletonResetable<C#tablename#Table>, ITable
        {
        #region Fields
        ggc.Foundation.CMapT<int, C#tablename#TableInfo> m_map#tablename#TableInfos;
            #endregion

            #region Funcs

        public C#tablename#Table()
            {
                m_map#tablename#TableInfos = new ggc.Foundation.CMapT<int, C#tablename#TableInfo>();
            }

    public C#tablename#TableInfo Get#tablename#TableInfo(int tableId)
            {
                return m_map#tablename#TableInfos.Find(tableId);
            }

public ggc.Foundation.CMapT<int, C#tablename#TableInfo>.Enumerator GetEnumerator()
            {
                return m_map#tablename#TableInfos.GetEnumerator();
            }

            public ggc.Foundation.CListT<C#tablename#TableInfo> Get#tablename#TableInfoList()
            {
                ggc.Foundation.CListT<C#tablename#TableInfo> list = new ggc.Foundation.CListT<C#tablename#TableInfo>();
                foreach (var item in m_map#tablename#TableInfos)
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
    m_map#tablename#TableInfos.Clear();
            }

#if !USING_BINARY_TABLE
bool _PostLoad(ggc.Foundation.CMapT<int, C#tablename#TableInfoRow> rows)
            {
    if (rows == null)
        return false;

    m_map#tablename#TableInfos.Clear();
                return true;
}
#endif
#endregion

#region Table Serialization
public bool LoadFromBinary(byte[] content)
{
    m_map#tablename#TableInfos.Clear();

                ggc.Foundation.CStream stream = new ggc.Foundation.CStream(content, 0, 0, (uint)content.Length);

    // 1. Get Version
    int iVersion = 0;
    stream.ReadT(out iVersion);
    if (iVersion != C#tablename#TableInfo.TableInfoVer)
                {
        ggc.Foundation.Log.LogErrorMsg(string.Format("[Table] {0} binary file version doesnt match", TableName));
        return false;
    }

    // 2. load entry
    int iCount = 0;
    stream.ReadT(out iCount);
    for (int i = 0; i < iCount; i++)
    {
        C#tablename#TableInfo tableInfo = new C#tablename#TableInfo();
                    if (!tableInfo.UnserializeFrom(stream))
        {
            ggc.Foundation.Log.LogErrorMsg("Fail to Unserialize From: " + TableName);
            return false;
        }
        m_map#tablename#TableInfos.Add(tableInfo.UniqueID, tableInfo);
                }

    return true;
}

public bool SaveToBinary(string tablePath)
{
    int buffSize = 8 * 1024 * 1024;
    byte[] buff = new byte[buffSize];
    ggc.Foundation.CStream stream = new ggc.Foundation.CStream(buff, 0);

    // 1. Get Version to stream
    stream.WriteT(C#tablename#TableInfo.TableInfoVer);

                // 2. Write entries to stream
    stream.WriteT(m_map#tablename#TableInfos.Count);
                foreach (var keyValuePair in m_map#tablename#TableInfos)
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

        };
    #endregion
}
