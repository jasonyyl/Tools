
using System.Collections.Generic;

namespace Mga.SHLib
{
    #region StarStreamExt
    public static class StarStreamExt
    {
        //! 扩展基本类型
        #region
        public static bool WriteT<valueT>(this ggc.Foundation.CStream stream, valueT value)
            where valueT : struct, System.IConvertible
        {
            switch (value.GetTypeCode())
            {
                case System.TypeCode.Boolean:
                    {
                        bool val = (bool)System.Convert.ChangeType(value, System.TypeCode.Boolean);
                        if (!stream.WriteBool(val)) { return false; }
                    }
                    break;
                case System.TypeCode.Char:
                    {
                        char val = (char)System.Convert.ChangeType(value, System.TypeCode.Char);
                        if (!stream.WriteChar(val)) { return false; }
                    }
                    break;
                case System.TypeCode.SByte:
                    {
                        sbyte val = (sbyte)System.Convert.ChangeType(value, System.TypeCode.SByte);
                        if (!stream.WriteSByte(val)) { return false; }
                    }
                    break;
                case System.TypeCode.Byte:
                    {
                        byte val = (byte)System.Convert.ChangeType(value, System.TypeCode.Byte);
                        if (!stream.WriteByte(val)) { return false; }
                    }
                    break;
                case System.TypeCode.Int16:
                    {
                        short val = (short)System.Convert.ChangeType(value, System.TypeCode.Int16);
                        if (!stream.WriteShort(val)) { return false; }
                    }
                    break;
                case System.TypeCode.UInt16:
                    {
                        ushort val = (ushort)System.Convert.ChangeType(value, System.TypeCode.UInt16);
                        if (!stream.WriteUShort(val)) { return false; }
                    }
                    break;
                case System.TypeCode.Int32:
                    {
                        int val = (int)System.Convert.ChangeType(value, System.TypeCode.Int32);
                        if (!stream.WriteInt(val)) { return false; }
                    }
                    break;
                case System.TypeCode.UInt32:
                    {
                        uint val = (uint)System.Convert.ChangeType(value, System.TypeCode.UInt32);
                        if (!stream.WriteUInt(val)) { return false; }
                    }
                    break;
                case System.TypeCode.Int64:
                    {
                        long val = (long)System.Convert.ChangeType(value, System.TypeCode.Int64);
                        if (!stream.WriteLong(val)) { return false; }
                    }
                    break;
                case System.TypeCode.UInt64:
                    {
                        ulong val = (ulong)System.Convert.ChangeType(value, System.TypeCode.UInt64);
                        if (!stream.WriteULong(val)) { return false; }
                    }
                    break;
                case System.TypeCode.Single:
                    {
                        float val = (float)System.Convert.ChangeType(value, System.TypeCode.Single);
                        if (!stream.WriteFloat(val)) { return false; }
                    }
                    break;
                case System.TypeCode.Double:
                    {
                        double val = (double)System.Convert.ChangeType(value, System.TypeCode.Double);
                        if (!stream.WriteDouble(val)) { return false; }
                    }
                    break;
                case System.TypeCode.Decimal:
                    {
                        throw new System.MissingMethodException("Unimplemented WriteT for " + value.GetType());
                    }
                //break;
                case System.TypeCode.DateTime:
                    {
                        System.DateTime val = (System.DateTime)System.Convert.ChangeType(value, System.TypeCode.DateTime);
                        if (!stream.WriteLong(val.Ticks)) { return false; }
                    }
                    break;
                default:
                    {
                        throw new System.MissingMethodException("Unimplemented WriteT for " + value.GetType());
                    }
            }
            return true;
        }
        public static bool ReadT<valueT>(this ggc.Foundation.CStream stream, out valueT value)
            where valueT : struct, System.IConvertible
        {
            value = default(valueT);
            switch (value.GetTypeCode())
            {
                case System.TypeCode.Boolean:
                    {
                        bool val;
                        if (!stream.Read(out val)) { return false; }
                        value = (valueT)System.Convert.ChangeType(val, System.TypeCode.Boolean);
                    }
                    break;
                case System.TypeCode.Char:
                    {
                        char val;
                        if (!stream.Read(out val)) { return false; }
                        value = (valueT)System.Convert.ChangeType(val, System.TypeCode.Char);
                    }
                    break;
                case System.TypeCode.SByte:
                    {
                        sbyte val;
                        if (!stream.Read(out val)) { return false; }
                        value = (valueT)System.Convert.ChangeType(val, System.TypeCode.SByte);
                    }
                    break;
                case System.TypeCode.Byte:
                    {
                        byte val;
                        if (!stream.Read(out val)) { return false; }
                        value = (valueT)System.Convert.ChangeType(val, System.TypeCode.Byte);
                    }
                    break;
                case System.TypeCode.Int16:
                    {
                        short val;
                        if (!stream.Read(out val)) { return false; }
                        value = (valueT)System.Convert.ChangeType(val, System.TypeCode.Int16);
                    }
                    break;
                case System.TypeCode.UInt16:
                    {
                        ushort val;
                        if (!stream.Read(out val)) { return false; }
                        value = (valueT)System.Convert.ChangeType(val, System.TypeCode.UInt16);
                    }
                    break;
                case System.TypeCode.Int32:
                    {
                        int val;
                        if (!stream.Read(out val)) { return false; }
                        value = (valueT)System.Convert.ChangeType(val, System.TypeCode.Int32);
                    }
                    break;
                case System.TypeCode.UInt32:
                    {
                        uint val;
                        if (!stream.Read(out val)) { return false; }
                        value = (valueT)System.Convert.ChangeType(val, System.TypeCode.UInt32);
                    }
                    break;
                case System.TypeCode.Int64:
                    {
                        long val;
                        if (!stream.Read(out val)) { return false; }
                        value = (valueT)System.Convert.ChangeType(val, System.TypeCode.Int64);
                    }
                    break;
                case System.TypeCode.UInt64:
                    {
                        ulong val;
                        if (!stream.Read(out val)) { return false; }
                        value = (valueT)System.Convert.ChangeType(val, System.TypeCode.UInt64);
                    }
                    break;
                case System.TypeCode.Single:
                    {
                        float val;
                        if (!stream.Read(out val)) { return false; }
                        value = (valueT)System.Convert.ChangeType(val, System.TypeCode.Single);
                    }
                    break;
                case System.TypeCode.Double:
                    {
                        double val;
                        if (!stream.Read(out val)) { return false; }
                        value = (valueT)System.Convert.ChangeType(val, System.TypeCode.Double);
                    }
                    break;
                case System.TypeCode.Decimal:
                    {
                        throw new System.MissingMethodException("Unimplemented ReadT for " + value.GetType());
                    }
                //break;
                case System.TypeCode.DateTime:
                    {
                        long val;
                        if (!stream.Read(out val)) { return false; }
                        System.DateTime dateTime = new System.DateTime(val);
                        value = (valueT)System.Convert.ChangeType(dateTime, System.TypeCode.DateTime);
                    }
                    break;
                default:
                    {
                        throw new System.MissingMethodException("Unimplemented ReadT for " + value.GetType());
                    }
            }
            return true;
        }
        #endregion

        //! 扩展string
        #region 
        public static bool WriteT(this ggc.Foundation.CStream stream, string value)
        {
            if (value != null)
            {
                if (!stream.Write(value, value.Length)) { return false; }
            }
            else
            {
                if (!stream.Write(value)) { return false; }
            }
            return true;
        }
        public static bool ReadT(this ggc.Foundation.CStream stream, out string value)
        {
            if (!stream.Read(out value, ggc.Foundation.CStream.MAX_STRING_LEN)) { return false; }
            return true;
        }
        #endregion

        //! 扩展string[]
        #region 
        public static bool WriteT(this ggc.Foundation.CStream stream, string[] values)
        {
            stream.WriteT((int)values.Length);
            foreach (var value in values)
            {
                if (!stream.WriteT(value)) { return false; }
            }
            return true;
        }
        public static bool ReadT(this ggc.Foundation.CStream stream, out string[] values)
        {
            values = null;
            int iCount = 0;
            if (!stream.ReadT(out iCount)) { return false; }

            values = new string[iCount];
            for (int i = 0; i < iCount; i++)
            {
                if (!stream.ReadT(out values[i])) { return false; }
            }
            return true;
        }
        #endregion

        ////! 扩展byte[]
        #region 扩展byte[]
        public static bool WriteT(this ggc.Foundation.CStream stream, byte[] value)
        {
            if (!stream.Write(value)) { return false; }
            return true;
        }
        public static bool ReadT(this ggc.Foundation.CStream stream, out byte[] value)
        {
            if (!stream.Read(out value)) { return false; }
            return true;
        }
        #endregion

        ////! 扩展int[]
        //#region
        //public static bool WriteT(this ggc.Foundation.CStream stream, int[] value)
        //{
        //    if (stream.WriteT((int)value.Length)) { return false; }
        //    for (int i = 0; i < value.Length; i++)
        //    {
        //        if (stream.WriteT(value[i]))
        //            return false;
        //    }
        //    return true;
        //}
        //public static bool ReadT(this ggc.Foundation.CStream stream, out int[] value)
        //{
        //    value = null;
        //    int iLen = 0;
        //    if (!stream.ReadT(out iLen)) { return false; }

        //    value = new int[iLen];
        //    for (int i = 0; i < value.Length; i++)
        //    {
        //        if (stream.ReadT(out value[i]))
        //            return false;
        //    }
        //    return true;
        //}
        //#endregion

        //! 扩展基本类型[]
        #region
        public static bool WriteT<T>(this ggc.Foundation.CStream stream, T[] value)
            where T : struct, System.IConvertible
        {
            if (!stream.WriteT((int)value.Length)) { return false; }
            for (int i = 0; i < value.Length; i++)
            {
                if (!stream.WriteT(value[i]))
                    return false;
            }
            return true;
        }
        public static bool ReadT<T>(this ggc.Foundation.CStream stream, out T[] value)
            where T : struct, System.IConvertible
        {
            value = null;
            int iLen = 0;
            if (!stream.ReadT(out iLen)) { return false; }

            value = new T[iLen];
            for (int i = 0; i < value.Length; i++)
            {
                if (!stream.ReadT(out value[i]))
                    return false;
            }
            return true;
        }
        #endregion

        //! 扩展Vector2
        #region 
        public static bool WriteT(this ggc.Foundation.CStream stream, UnityEngine.Vector2 value)
        {
            if (!stream.WriteT(value.x)) { return false; }
            if (!stream.WriteT(value.y)) { return false; }
            return true;
        }
        public static bool ReadT(this ggc.Foundation.CStream stream, out UnityEngine.Vector2 value)
        {
            value = UnityEngine.Vector3.zero;
            if (!stream.ReadT(out value.x)) { return false; }
            if (!stream.ReadT(out value.y)) { return false; }
            return true;
        }
        #endregion

        //! 扩展Vector3
        #region
        public static bool WriteT(this ggc.Foundation.CStream stream, UnityEngine.Vector3 value)
        {
            if (!stream.WriteT(value.x)) { return false; }
            if (!stream.WriteT(value.y)) { return false; }
            if (!stream.WriteT(value.z)) { return false; }
            return true;
        }
        public static bool ReadT(this ggc.Foundation.CStream stream, out UnityEngine.Vector3 value)
        {
            value = UnityEngine.Vector3.zero;
            if (!stream.ReadT(out value.x)) { return false; }
            if (!stream.ReadT(out value.y)) { return false; }
            if (!stream.ReadT(out value.z)) { return false; }
            return true;
        }
        #endregion

        //! 扩展List<T>
        #region 
        public static bool WriteT<T>(this ggc.Foundation.CStream stream, List<T> list) where T : class, ISerializable, new()
        {
            if (!stream.WriteT((int)list.Count)) { return false; }
            foreach (var entry in list)
            {
                if (!entry.SerializeTo(stream))
                    return false;
            }
            return true;
        }
        public static bool ReadT<T>(this ggc.Foundation.CStream stream, out List<T> list) where T : class, ISerializable, new()
        {
            list = new List<T>();

            int iCount = 0;
            if (!stream.ReadT(out iCount)) { return false; }
            list.Capacity = iCount;
            for (int i = 0; i < iCount; i++)
            {
                var entry = new T();
                if (!entry.UnserializeFrom(stream))
                    return false;
                list.Add(entry);
            }
            return true;
        }
        #endregion

        //! 扩展List<string>
        #region 
        public static bool WriteT(this ggc.Foundation.CStream stream, List<string> list)
        {
            if (!stream.WriteT((int)list.Count)) { return false; }
            foreach (var entry in list)
            {
                if (!stream.WriteT(entry)) { return false; }
            }
            return true;
        }
        public static bool ReadT(this ggc.Foundation.CStream stream, out List<string> list)
        {
            list = new List<string>();

            int iCount = 0;
            if (!stream.ReadT(out iCount)) { return false; }
            for (int i = 0; i < iCount; i++)
            {
                string value = "";
                if (!stream.ReadT(out value)) { return false; }
                list.Add(value);
            }
            return true;
        }
        #endregion

        //! 扩展List<int>
        #region 
        public static bool WriteT(this ggc.Foundation.CStream stream, List<int> list)
        {
            if (!stream.WriteT((int)list.Count)) { return false; }
            foreach (var entry in list)
            {
                if (!stream.WriteT(entry)) { return false; }
            }
            return true;
        }
        public static bool ReadT(this ggc.Foundation.CStream stream, out List<int> list)
        {
            list = new List<int>();

            int iCount = 0;
            if (!stream.ReadT(out iCount)) { return false; }
            for (int i = 0; i < iCount; i++)
            {
                int value = 0;
                if (!stream.ReadT(out value)) { return false; }
                list.Add(value);
            }
            return true;
        }
        #endregion

        //! 扩展List<int>[]
        #region 
        public static bool WriteT(this ggc.Foundation.CStream stream, List<int>[] lists)
        {
            if (!stream.WriteT((int)lists.Length)) { return false; }
            foreach (var entry in lists)
            {
                if (!stream.WriteT(entry)) { return false; }
            }
            return true;
        }
        public static bool ReadT(this ggc.Foundation.CStream stream, out List<int>[] lists)
        {
            lists = null;
            int iCount = 0;
            if (!stream.ReadT(out iCount)) { return false; }
            lists = new List<int>[iCount];

            for (int i = 0; i < iCount; i++)
            {
                if (!stream.ReadT(out lists[i])) { return false; }
            }
            return true;
        }
        #endregion

        //! 扩展ISerializable Class
        #region 
        public static bool WriteObjT<T>(this ggc.Foundation.CStream stream, T value) where T : ISerializable, new()
        {
            if (!value.SerializeTo(stream)) { return false; }
            return true;
        }
        public static bool ReadObjT<T>(this ggc.Foundation.CStream stream, out T value) where T : ISerializable, new()
        {
            value = new T();
            if (!value.UnserializeFrom(stream)) { return false; }
            return true;
        }
        #endregion
        //! 扩展 Nullable ISerializable Class
        #region
        public static bool WriteNullableObjT<T>(this ggc.Foundation.CStream stream, T value) where T : ISerializable, new()
        {
            if (value != null)
            {
                if (!stream.WriteT(true)) { return false; }
                if (!value.SerializeTo(stream)) { return false; }
            }
            else
            {
                if (!stream.WriteT(false)) { return false; }
            }
            return true;
        }
        public static bool ReadNullableObjT<T>(this ggc.Foundation.CStream stream, out T value) where T : ISerializable, new()
        {
            value = default(T);

            bool bHasValue = true;
            if (!stream.ReadT(out bHasValue)) { return false; }
            if (!bHasValue)
                return true;

            value = new T();
            if (!value.UnserializeFrom(stream)) { return false; }
            return true;
        }
        #endregion

        //! 扩展ISerializable Class[]
        #region
        public static bool WriteObjT<T>(this ggc.Foundation.CStream stream, T[] values) where T : ISerializable, new()
        {
            if (!stream.WriteT((int)values.Length)) { return false; }
            for (int i = 0; i < values.Length; i++)
                if (!values[i].SerializeTo(stream)) { return false; }
            return true;
        }
        public static bool ReadObjT<T>(this ggc.Foundation.CStream stream, out T[] values) where T : ISerializable, new()
        {
            values = null;
            int cnt = 0;
            if (!stream.ReadT(out cnt)) { return false; }
            values = new T[cnt];
            for (int i = 0; i < cnt; i++)
            {
                values[i] = new T();
                if (!values[i].UnserializeFrom(stream)) { return false; }
            }
            return true;
        }
        #endregion

        //! 扩展CChaoXXX
        #region
        public static bool WriteT(this ggc.Foundation.CStream stream, CChaosInt value)
        {
            return stream.WriteT((int)value);
        }
        public static bool ReadT(this ggc.Foundation.CStream stream, out CChaosInt value)
        {
            value = 0;
            int temp = 0;
            if (!stream.ReadT(out temp)) { return false; }
            value = temp;
            return true;
        }

        public static bool WriteT(this ggc.Foundation.CStream stream, CChaosIntEx value)
        {
            return stream.WriteT((int)value);
        }
        public static bool ReadT(this ggc.Foundation.CStream stream, out CChaosIntEx value)
        {
            value = 0;
            int temp = 0;
            if (!stream.ReadT(out temp)) { return false; }
            value = temp;
            return true;
        }

        public static bool WriteT(this ggc.Foundation.CStream stream, CChaosByte value)
        {
            return stream.WriteT((byte)value);
        }
        public static bool ReadT(this ggc.Foundation.CStream stream, out CChaosByte value)
        {
            value = 0;
            byte temp = 0;
            if (!stream.ReadT(out temp)) { return false; }
            value = temp;
            return true;
        }

        public static bool WriteT(this ggc.Foundation.CStream stream, CChaosByteEx value)
        {
            return stream.WriteT((byte)value);
        }
        public static bool ReadT(this ggc.Foundation.CStream stream, out CChaosByteEx value)
        {
            value = 0;
            byte temp = 0;
            if (!stream.ReadT(out temp)) { return false; }
            value = temp;
            return true;
        }

        public static bool WriteT(this ggc.Foundation.CStream stream, CChaosFloat value)
        {
            return stream.WriteT((float)value);
        }
        public static bool ReadT(this ggc.Foundation.CStream stream, out CChaosFloat value)
        {
            value = 0;
            float temp = 0;
            if (!stream.ReadT(out temp)) { return false; }
            value = temp;
            return true;
        }
        #endregion
    }
    #endregion

    #region CStreamingUtility
    public static class CStreamingUtility
    {
        #region 容器的流化/反流化

        //! 数值集合
        #region 数值集合
        #region valueT[]
        public static bool SerializeNumGroupTo<valueT>(valueT[] array, ggc.Foundation.CStream stream)
            where valueT : struct, System.IConvertible
        {
            int cnt = array.Length;
            if (!stream.WriteT(cnt)) { return false; }
            foreach (valueT node in array)
            {
                if (!stream.WriteT(node)) { return false; }
            }
            return true;
        }
        public static bool UnserializeNumGroupFrom<valueT>(out valueT[] array, ggc.Foundation.CStream stream)
            where valueT : struct, System.IConvertible
        {
            array = null;
            int cnt = 0;
            if (!stream.ReadT(out cnt)) { return false; }
            array = new valueT[cnt];
            for (int i = 0; i < cnt; ++i)
            {
                if (!stream.ReadT(out array[i])) { return false; }
            }
            return true;
        }
        #endregion

        #region CMapT<keyT, valueT>
        public static bool SerializeNumGroupTo<keyT, valueT>(ggc.Foundation.CMapT<keyT, valueT> map, ggc.Foundation.CStream stream)
            where keyT : struct, System.IConvertible
            where valueT : struct, System.IConvertible
        {
            int cnt = map.Count;
            if (!stream.WriteT(cnt)) { return false; }
            var ie = map.GetEnumerator();
            while (ie.MoveNext())
            {
                if (!stream.WriteT(ie.Current.Key)) { return false; }
                if (!stream.WriteT(ie.Current.Value)) { return false; }
            }
            return true;
        }
        public static bool UnserializeNumGroupFrom<keyT, valueT>(out ggc.Foundation.CMapT<keyT, valueT> map, ggc.Foundation.CStream stream)
            where keyT : struct, System.IConvertible
            where valueT : struct, System.IConvertible
        {
            map = new ggc.Foundation.CMapT<keyT, valueT>();
            int cnt = 0;
            if (!stream.ReadT(out cnt)) { return false; }
            for (int i = 0; i < cnt; ++i)
            {
                keyT key = new keyT();
                valueT value = new valueT();
                if (!stream.ReadT(out key)) { return false; }
                if (!stream.ReadT(out value)) { return false; }
                map.Add(key, value);
            }
            return true;
        }
        #endregion

        #region HashSet<keyT>
        public static bool SerializeNumGroupTo<keyT>(System.Collections.Generic.HashSet<keyT> set, ggc.Foundation.CStream stream)
            where keyT : struct, System.IConvertible
        {
            int cnt = set.Count;
            if (!stream.WriteT(cnt)) { return false; }
            var ie = set.GetEnumerator();
            while (ie.MoveNext())
            {
                if (!stream.WriteT(ie.Current)) { return false; }
            }
            return true;
        }
        public static bool UnserializeNumGroupFrom<keyT>(out System.Collections.Generic.HashSet<keyT> set, ggc.Foundation.CStream stream)
            where keyT : struct, System.IConvertible
        {
            set = new System.Collections.Generic.HashSet<keyT>();
            int cnt = 0;
            if (!stream.ReadT(out cnt)) { return false; }
            for (int i = 0; i < cnt; ++i)
            {
                keyT key = new keyT();
                if (!stream.ReadT(out key)) { return false; }
                set.Add(key);
            }
            return true;
        }
        #endregion

        #region CListT<valueT>
        public static bool SerializeNumGroupTo<valueT>(ggc.Foundation.CListT<valueT> list, ggc.Foundation.CStream stream)
            where valueT : struct, System.IConvertible
        {
            int cnt = list.Count;
            if (!stream.WriteT(cnt)) { return false; }
            foreach (valueT node in list)
            {
                if (!stream.WriteT(node)) { return false; }
            }
            return true;
        }
        public static bool UnserializeNumGroupFrom<valueT>(out ggc.Foundation.CListT<valueT> list, ggc.Foundation.CStream stream)
            where valueT : struct, System.IConvertible
        {
            list = new ggc.Foundation.CListT<valueT>();
            int cnt = 0;
            if (!stream.ReadT(out cnt)) { return false; }
            for (int i = 0; i < cnt; ++i)
            {
                valueT node = new valueT();
                if (!stream.ReadT(out node)) { return false; }
                list.Add(node);
            }
            return true;
        }
        #endregion

        #region List<valueT>
        public static bool SerializeNumGroupTo<valueT>(List<valueT> list, ggc.Foundation.CStream stream)
            where valueT : struct, System.IConvertible
        {
            int cnt = list.Count;
            if (!stream.WriteT(cnt)) { return false; }
            foreach (valueT node in list)
            {
                if (!stream.WriteT(node)) { return false; }
            }
            return true;
        }
        public static bool UnserializeNumGroupFrom<valueT>(out List<valueT> list, ggc.Foundation.CStream stream)
            where valueT : struct, System.IConvertible
        {
            list = new List<valueT>();
            int cnt = 0;
            if (!stream.ReadT(out cnt)) { return false; }
            for (int i = 0; i < cnt; ++i)
            {
                valueT node = new valueT();
                if (!stream.ReadT(out node)) { return false; }
                list.Add(node);
            }
            return true;
        }
        #endregion

        #region ListT<ListT<valueT>>
        public static bool SerializeNumGroupTo<valueT>(List<List<valueT>> list, ggc.Foundation.CStream stream)
            where valueT : struct, System.IConvertible
        {
            int cnt = list.Count;
            if (!stream.WriteT(cnt)) { return false; }
            foreach (List<valueT> node in list)
            {
                if (node == null) { throw new System.NullReferenceException("SerializeObjGroupTo, null obj in container"); }
                if (!SerializeNumGroupTo(node, stream)) { return false; }
            }
            return true;
        }
        public static bool UnserializeNumGroupFrom<valueT>(out List<List<valueT>> list, ggc.Foundation.CStream stream)
            where valueT : struct, System.IConvertible
        {
            list = new List<List<valueT>>();
            int cnt = 0;
            if (!stream.ReadT(out cnt)) { return false; }
            for (int i = 0; i < cnt; ++i)
            {
                List<valueT> node = new List<valueT>();
                if (!UnserializeNumGroupFrom(out node, stream)) { return false; }
                list.Add(node);
            }
            return true;
        }
        #endregion

        #region CListT<string>
        public static bool SerializeNumGroupTo(ggc.Foundation.CListT<string> list, ggc.Foundation.CStream stream)
        {
            int cnt = list.Count;
            if (!stream.WriteT(cnt)) { return false; }
            foreach (string node in list)
            {
                if (!stream.WriteT(node)) { return false; }
            }
            return true;
        }
        public static bool UnserializeNumGroupFrom(out ggc.Foundation.CListT<string> list, ggc.Foundation.CStream stream)
        {
            list = new ggc.Foundation.CListT<string>();
            int cnt = 0;
            if (!stream.ReadT(out cnt)) { return false; }
            for (int i = 0; i < cnt; ++i)
            {
                string node = "";
                if (!stream.ReadT(out node)) { return false; }
                list.Add(node);
            }
            return true;
        }
        #endregion

        #region valueT[][]
        public static bool SerializeNumGroupTo<valueT>(valueT[][] array, ggc.Foundation.CStream stream)
            where valueT : struct, System.IConvertible
        {
            int cnt = array.Length;
            if (!stream.WriteT(cnt)) { return false; }
            foreach (valueT[] node in array)
            {
                if (!SerializeNumGroupTo(node, stream)) { return false; }
            }
            return true;
        }
        public static bool UnserializeNumGroupFrom<valueT>(out valueT[][] array, ggc.Foundation.CStream stream)
            where valueT : struct, System.IConvertible
        {
            array = null;
            int cnt = 0;
            if (!stream.ReadT(out cnt)) { return false; }
            array = new valueT[cnt][];
            for (int i = 0; i < cnt; ++i)
            {
                valueT[] node = null;
                if (!UnserializeNumGroupFrom(out node, stream)) { return false; }
                array[i] = node;
            }
            return true;
        }
        #endregion

        #region CListT<TValue>[]
        public static bool SerializeNumGroupTo<valueT>(ggc.Foundation.CListT<valueT>[] lists, ggc.Foundation.CStream stream)
            where valueT : struct, System.IConvertible
        {
            int cnt = lists.Length;
            if (!stream.WriteT(cnt)) { return false; }
            foreach (var entry in lists)
            {
                if (entry == null) { throw new System.NullReferenceException("SerializeObjGroupTo, null obj in container"); }
                if (!SerializeNumGroupTo(entry, stream)) { return false; }
            }
            return true;
        }
        public static bool UnserializeNumGroupFrom<valueT>(out ggc.Foundation.CListT<valueT>[] lists, ggc.Foundation.CStream stream)
            where valueT : struct, System.IConvertible
        {
            lists = null;
            int cnt = 0;
            if (!stream.ReadT(out cnt)) { return false; }
            lists = new ggc.Foundation.CListT<valueT>[cnt];
            for (int i = 0; i < cnt; ++i)
            {
                if (!UnserializeNumGroupFrom(out lists[i], stream)) { return false; }
            }
            return true;
        }
        #endregion

        #region byte[]特化
        public static bool SerializeNumGroupTo(byte[] array, ggc.Foundation.CStream stream)
        {
            return stream.Write(array);
        }
        public static bool UnserializeNumGroupFrom(out byte[] array, ggc.Foundation.CStream stream)
        {
            return stream.Read(out array);
        }
        #endregion
        #endregion

        //! 对象集合(对象非空)
        #region 对象集合(对象非空)
        #region LinkedList<nodeT>
        public static bool SerializeObjGroupTo<nodeT>(System.Collections.Generic.LinkedList<nodeT> list, ggc.Foundation.CStream stream)
            where nodeT : ISerializable, new()
        {
            int cnt = list.Count;
            if (!stream.WriteT(cnt)) { return false; }
            foreach (nodeT node in list)
            {
                if (node == null) { throw new System.NullReferenceException("SerializeObjGroupTo, null obj in container"); }
                if (!node.SerializeTo(stream)) { return false; }
            }
            return true;
        }
        public static bool UnserializeObjGroupFrom<nodeT>(out System.Collections.Generic.LinkedList<nodeT> list, ggc.Foundation.CStream stream)
            where nodeT : ISerializable, new()
        {
            list = new System.Collections.Generic.LinkedList<nodeT>();
            int cnt = 0;
            if (!stream.ReadT(out cnt)) { return false; }
            for (int i = 0; i < cnt; ++i)
            {
                nodeT node = new nodeT();
                if (!node.UnserializeFrom(stream)) { return false; }
                list.AddLast(node);
            }
            return true;
        }
        #endregion

        #region nodeT[]
        public static bool SerializeObjGroupTo<nodeT>(nodeT[] array, ggc.Foundation.CStream stream)
            where nodeT : ISerializable, new()
        {
            int cnt = array.Length;
            if (!stream.WriteT(cnt)) { return false; }
            foreach (nodeT node in array)
            {
                if (node == null) { throw new System.NullReferenceException("SerializeObjGroupTo, null obj in container"); }
                if (!node.SerializeTo(stream)) { return false; }
            }
            return true;
        }
        public static bool UnserializeObjGroupFrom<nodeT>(out nodeT[] array, ggc.Foundation.CStream stream)
            where nodeT : ISerializable, new()
        {
            array = null;
            int cnt = 0;
            if (!stream.ReadT(out cnt)) { return false; }
            array = new nodeT[cnt];
            for (int i = 0; i < cnt; ++i)
            {
                nodeT node = new nodeT();
                if (!node.UnserializeFrom(stream)) { return false; }
                array[i] = node;
            }
            return true;
        }
        #endregion

        #region CMapT<keyT, nodeT>
        public static bool SerializeObjGroupTo<keyT, nodeT>(ggc.Foundation.CMapT<keyT, nodeT> map, ggc.Foundation.CStream stream)
            where keyT : struct, System.IConvertible
            where nodeT : ISerializable, new()
        {
            int cnt = map.Count;
            if (!stream.WriteT(cnt)) { return false; }
            var ie = map.GetEnumerator();
            while (ie.MoveNext())
            {
                if (ie.Current.Value == null) { throw new System.NullReferenceException("SerializeObjGroupTo, null obj in container"); }
                if (!stream.WriteT(ie.Current.Key)) { return false; }
                if (!ie.Current.Value.SerializeTo(stream)) { return false; }
            }
            return true;
        }
        public static bool UnserializeObjGroupFrom<keyT, nodeT>(out ggc.Foundation.CMapT<keyT, nodeT> map, ggc.Foundation.CStream stream)
            where keyT : struct, System.IConvertible
            where nodeT : ISerializable, new()
        {
            map = new ggc.Foundation.CMapT<keyT, nodeT>();
            int cnt = 0;
            if (!stream.ReadT(out cnt)) { return false; }
            for (int i = 0; i < cnt; ++i)
            {
                keyT key = new keyT();
                nodeT node = new nodeT();
                if (!stream.ReadT(out key)) { return false; }
                if (!node.UnserializeFrom(stream)) { return false; }
                map.Add(key, node);
            }
            return true;
        }
        #endregion

        #region CMapT<keyT, nodeT>[]
        public static bool SerializeObjGroupTo<keyT, nodeT>(ggc.Foundation.CMapT<keyT, nodeT>[] maps, ggc.Foundation.CStream stream)
            where keyT : struct, System.IConvertible
            where nodeT : ISerializable, new()
        {
            int cnt = maps.Length;
            if (!stream.WriteT(cnt)) { return false; }
            foreach (var entry in maps)
            {
                if (!SerializeObjGroupTo(entry, stream))
                    return false;
            }
            return true;
        }
        public static bool UnserializeObjGroupFrom<keyT, nodeT>(out ggc.Foundation.CMapT<keyT, nodeT>[] maps, ggc.Foundation.CStream stream)
            where keyT : struct, System.IConvertible
            where nodeT : ISerializable, new()
        {
            maps = null;
            int cnt = 0;
            if (!stream.ReadT(out cnt)) { return false; }
            maps = new ggc.Foundation.CMapT<keyT, nodeT>[cnt];
            for (int i = 0; i < cnt; ++i)
            {
                if (!UnserializeObjGroupFrom(out maps[i], stream))
                    return false;
            }
            return true;
        }
        #endregion

        #region CMapT<Tkey, CListT<TValue>>
        public static bool SerializeObjGroupTo<keyT, valueT>(ggc.Foundation.CMapT<keyT, ggc.Foundation.CListT<valueT>> map, ggc.Foundation.CStream stream)
            where keyT : struct, System.IConvertible
            where valueT : ISerializable, new()
        {
            int cnt = map.Count;
            if (!stream.WriteT(cnt)) { return false; }
            var ie = map.GetEnumerator();
            while (ie.MoveNext())
            {
                if (ie.Current.Value == null) { throw new System.NullReferenceException("SerializeObjGroupTo, null obj in container"); }
                if (!stream.WriteT(ie.Current.Key)) { return false; }
                if (!SerializeObjGroupTo(ie.Current.Value, stream)) { return false; }
            }
            return true;
        }
        public static bool UnserializeObjGroupFrom<keyT, valueT>(out ggc.Foundation.CMapT<keyT, ggc.Foundation.CListT<valueT>> map, ggc.Foundation.CStream stream)
            where keyT : struct, System.IConvertible
            where valueT : ISerializable, new()
        {
            map = new ggc.Foundation.CMapT<keyT, ggc.Foundation.CListT<valueT>>();
            int cnt = 0;
            if (!stream.ReadT(out cnt)) { return false; }
            for (int i = 0; i < cnt; ++i)
            {
                keyT key = new keyT();
                ggc.Foundation.CListT<valueT> list = null;
                if (!stream.ReadT(out key)) { return false; }
                if (!UnserializeObjGroupFrom(out list, stream)) { return false; }
                map.Add(key, list);
            }
            return true;
        }
        #endregion

        #region ListT<nodeT>
        public static bool SerializeObjGroupTo<nodeT>(List<nodeT> list, ggc.Foundation.CStream stream)
            where nodeT : ISerializable, new()
        {
            int cnt = list.Count;
            if (!stream.WriteT(cnt))
                return false;
            foreach (nodeT node in list)
            {
                if (node == null) { throw new System.NullReferenceException("SerializeObjGroupTo, null obj in container"); }
                if (!node.SerializeTo(stream))
                    return false;
            }
            return true;
        }
        public static bool UnserializeObjGroupFrom<nodeT>(out List<nodeT> list, ggc.Foundation.CStream stream)
            where nodeT : ISerializable, new()
        {
            list = new ggc.Foundation.CListT<nodeT>();
            int cnt = 0;
            if (!stream.ReadT(out cnt)) { return false; }
            for (int i = 0; i < cnt; ++i)
            {
                nodeT node = new nodeT();
                if (!node.UnserializeFrom(stream)) { return false; }
                list.Add(node);
            }
            return true;
        }
        #endregion

        #region ListT<ListT<nodeT>>
        public static bool SerializeObjGroupTo<nodeT>(List<List<nodeT>> list, ggc.Foundation.CStream stream)
            where nodeT : ISerializable, new()
        {
            int cnt = list.Count;
            if (!stream.WriteT(cnt)) { return false; }
            foreach (List<nodeT> node in list)
            {
                if (node == null) { throw new System.NullReferenceException("SerializeObjGroupTo, null obj in container"); }
                if (!SerializeObjGroupTo(node, stream)) { return false; }
            }
            return true;
        }
        public static bool UnserializeObjGroupFrom<nodeT>(out List<List<nodeT>> list, ggc.Foundation.CStream stream)
            where nodeT : ISerializable, new()
        {
            list = new List<List<nodeT>>();
            int cnt = 0;
            if (!stream.ReadT(out cnt)) { return false; }
            for (int i = 0; i < cnt; ++i)
            {
                List<nodeT> node = new List<nodeT>();
                if (!UnserializeObjGroupFrom(out node, stream)) { return false; }
                list.Add(node);
            }
            return true;
        }
        #endregion

        #region CListT<nodeT>
        public static bool SerializeObjGroupTo<nodeT>(ggc.Foundation.CListT<nodeT> list, ggc.Foundation.CStream stream)
            where nodeT : ISerializable, new()
        {
            int cnt = list.Count;
            if (!stream.WriteT(cnt)) { return false; }
            foreach (nodeT node in list)
            {
                if (node == null) { throw new System.NullReferenceException("SerializeObjGroupTo, null obj in container"); }
                if (!node.SerializeTo(stream)) { return false; }
            }
            return true;
        }
        public static bool UnserializeObjGroupFrom<nodeT>(out ggc.Foundation.CListT<nodeT> list, ggc.Foundation.CStream stream)
            where nodeT : ISerializable, new()
        {
            list = new ggc.Foundation.CListT<nodeT>();
            int cnt = 0;
            if (!stream.ReadT(out cnt)) { return false; }
            for (int i = 0; i < cnt; ++i)
            {
                nodeT node = new nodeT();
                if (!node.UnserializeFrom(stream)) { return false; }
                list.Add(node);
            }
            return true;
        }
        #endregion

        #region CListT<CListT<nodeT>>
        public static bool SerializeObjGroupTo<nodeT>(ggc.Foundation.CListT<ggc.Foundation.CListT<nodeT>> list, ggc.Foundation.CStream stream)
            where nodeT : ISerializable, new()
        {
            int cnt = list.Count;
            if (!stream.WriteT(cnt)) { return false; }
            foreach (ggc.Foundation.CListT<nodeT> node in list)
            {
                if (node == null) { throw new System.NullReferenceException("SerializeObjGroupTo, null obj in container"); }
                if (!SerializeObjGroupTo(node, stream)) { return false; }
            }
            return true;
        }
        public static bool UnserializeObjGroupFrom<nodeT>(out ggc.Foundation.CListT<ggc.Foundation.CListT<nodeT>> list, ggc.Foundation.CStream stream)
            where nodeT : ISerializable, new()
        {
            list = new ggc.Foundation.CListT<ggc.Foundation.CListT<nodeT>>();
            int cnt = 0;
            if (!stream.ReadT(out cnt)) { return false; }
            for (int i = 0; i < cnt; ++i)
            {
                ggc.Foundation.CListT<nodeT> node = new ggc.Foundation.CListT<nodeT>();
                if (!UnserializeObjGroupFrom(out node, stream)) { return false; }
                list.Add(node);
            }
            return true;
        }
        #endregion

        #region CListT<KeyValuePair<string,string>>
        public static bool SerializeObjGroupTo(ggc.Foundation.CListT<KeyValuePair<string, string>> list, ggc.Foundation.CStream stream)
        {
            int cnt = list.Count;
            if (!stream.WriteT(cnt)) { return false; }
            foreach (var node in list)
            {
                if (!stream.WriteT(node.Key)) { return false; }
                if (!stream.WriteT(node.Value)) { return false; }
            }
            return true;
        }
        public static bool UnserializeObjGroupFrom(out ggc.Foundation.CListT<KeyValuePair<string, string>> list, ggc.Foundation.CStream stream)
        {
            list = new ggc.Foundation.CListT<KeyValuePair<string, string>>();
            int cnt = 0;
            if (!stream.ReadT(out cnt)) { return false; }
            for (int i = 0; i < cnt; ++i)
            {
                string key = "";
                string value = "";
                if (!stream.ReadT(out key)) { return false; }
                if (!stream.ReadT(out value)) { return false; }
                list.Add(new KeyValuePair<string, string>(key, value));
            }
            return true;
        }
        #endregion
        #endregion

        //! 对象集合(对象可为空)
        #region 对象集合(对象可为空)
        #region CListT<nodeT>
        public static bool SerializeNullableObjGroupTo<nodeT>(ggc.Foundation.CListT<nodeT> list, ggc.Foundation.CStream stream)
            where nodeT : ISerializable, new()
        {
            bool bHasVal = true;
            if (list == null)
            {
                bHasVal = false;
                if (!stream.WriteT(bHasVal)) { return false; }
                return true;
            }
            if (!stream.WriteT(bHasVal)) { return false; }

            int cnt = list.Count;
            if (!stream.WriteT(cnt)) { return false; }
            foreach (nodeT node in list)
            {
                if (node != null)
                {
                    bHasVal = true;
                    if (!stream.WriteT(bHasVal)) { return false; }
                    if (!node.SerializeTo(stream)) { return false; }
                }
                else
                {
                    bHasVal = false;
                    if (!stream.WriteT(bHasVal)) { return false; }
                }
            }
            return true;
        }
        public static bool UnserializeNullableObjGroupFrom<nodeT>(out ggc.Foundation.CListT<nodeT> list, ggc.Foundation.CStream stream)
            where nodeT : ISerializable, new()
        {
            list = null;

            bool bHasVal = true;
            if (!stream.ReadT(out bHasVal)) { return false; }
            if (!bHasVal)
                return true;

            list = new ggc.Foundation.CListT<nodeT>();
            int cnt = 0;
            if (!stream.ReadT(out cnt)) { return false; }
            for (int i = 0; i < cnt; ++i)
            {
                nodeT node = default(nodeT);
                if (!stream.ReadT(out bHasVal)) { return false; }
                if (bHasVal)
                {
                    node = new nodeT();
                    if (!node.UnserializeFrom(stream)) { return false; }
                }
                list.Add(node);
            }
            return true;
        }
        #endregion

        #region nodeT[]
        public static bool SerializeNullableObjGroupTo<nodeT>(nodeT[] array, ggc.Foundation.CStream stream)
            where nodeT : ISerializable, new()
        {
            bool bHasVal = true;
            if (array == null)
            {
                bHasVal = false;
                if (!stream.WriteT(bHasVal)) { return false; }
                return true;
            }
            if (!stream.WriteT(bHasVal)) { return false; }

            int cnt = array.Length;
            if (!stream.WriteT(cnt)) { return false; }
            foreach (nodeT node in array)
            {
                if (node != null)
                {
                    bHasVal = true;
                    if (!stream.WriteT(bHasVal)) { return false; }
                    if (!node.SerializeTo(stream)) { return false; }
                }
                else
                {
                    bHasVal = false;
                    if (!stream.WriteT(bHasVal)) { return false; }
                }
            }
            return true;
        }
        public static bool UnserializeNullableObjGroupFrom<nodeT>(out nodeT[] array, ggc.Foundation.CStream stream)
            where nodeT : ISerializable, new()
        {
            array = null;

            bool bHasVal = true;
            if (!stream.ReadT(out bHasVal)) { return false; }
            if (!bHasVal)
                return true;

            int cnt = 0;
            if (!stream.ReadT(out cnt)) { return false; }
            array = new nodeT[cnt];
            for (int i = 0; i < cnt; ++i)
            {
                nodeT node = default(nodeT);
                if (!stream.ReadT(out bHasVal)) { return false; }
                if (bHasVal)
                {
                    node = new nodeT();
                    if (!node.UnserializeFrom(stream)) { return false; }
                }
                array[i] = node;
            }
            return true;
        }
        #endregion

        #region CMapT<keyT, nodeT>
        public static bool SerializeNullableObjGroupTo<keyT, nodeT>(ggc.Foundation.CMapT<keyT, nodeT> map, ggc.Foundation.CStream stream)
            where keyT : struct, System.IConvertible
            where nodeT : ISerializable, new()
        {
            bool bHasVal = true;
            if (map == null)
            {
                bHasVal = false;
                if (!stream.WriteT(bHasVal)) { return false; }
                return true;
            }
            if (!stream.WriteT(bHasVal)) { return false; }

            int cnt = map.Count;
            if (!stream.WriteT(cnt)) { return false; }
            var ie = map.GetEnumerator();
            while (ie.MoveNext())
            {
                if (!stream.WriteT(ie.Current.Key)) { return false; }
                if (ie.Current.Value != null)
                {
                    bHasVal = true;
                    if (!stream.WriteT(bHasVal)) { return false; }
                    if (!ie.Current.Value.SerializeTo(stream)) { return false; }
                }
                else
                {
                    bHasVal = false;
                    if (!stream.WriteT(bHasVal)) { return false; }
                }
            }
            return true;
        }
        public static bool UnserializeNullableObjGroupFrom<keyT, nodeT>(out ggc.Foundation.CMapT<keyT, nodeT> map, ggc.Foundation.CStream stream)
            where keyT : struct, System.IConvertible
            where nodeT : ISerializable, new()
        {
            map = null;

            bool bHasVal = true;
            if (!stream.ReadT(out bHasVal)) { return false; }
            if (!bHasVal)
                return true;

            map = new ggc.Foundation.CMapT<keyT, nodeT>();
            int cnt = 0;
            if (!stream.ReadT(out cnt)) { return false; }
            for (int i = 0; i < cnt; ++i)
            {
                keyT key = new keyT();
                if (!stream.ReadT(out key)) { return false; }
                nodeT node = default(nodeT);
                if (!stream.ReadT(out bHasVal)) { return false; }
                if (bHasVal)
                {
                    node = new nodeT();
                    if (!node.UnserializeFrom(stream)) { return false; }
                }
                map.Add(key, node);
            }
            return true;
        }
        #endregion
        #endregion

        //! string
        #region string
        #region string[]
        public static bool SerializeStringArrayTo(string[] values, ggc.Foundation.CStream stream)
        {
            int cnt = values.Length;
            if (!stream.WriteT(cnt)) { return false; }
            for (int i = 0; i < cnt; i++)
                if (!stream.WriteT(values[i])) { return false; }
            return true;
        }
        public static bool UnserializeStringArrayFrom(out string[] values, ggc.Foundation.CStream stream)
        {
            values = null;
            int cnt = 0;
            if (!stream.ReadT(out cnt)) { return false; }
            values = new string[cnt];
            for (int i = 0; i < cnt; ++i)
            {
                string value = "";
                if (!stream.ReadT(out value)) { return false; }
                values[i] = CUniqueString.Instance.GetUniqueString(value);
            }
            return true;
        }
        #endregion

        #region string[,]
        public static bool SerializeStringArrayTo(string[,] values, ggc.Foundation.CStream stream)
        {
            int row = values.GetLength(0);
            int column = values.GetLength(1);
            if (!stream.WriteT(row)) { return false; }
            if (!stream.WriteT(column)) { return false; }
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    if (!stream.WriteT(values[i, j])) { return false; }
                }
            }
            return true;
        }
        public static bool UnserializeStringArrayFrom(out string[,] values, ggc.Foundation.CStream stream)
        {
            values = null;
            int row = 0;
            int column = 1;
            if (!stream.ReadT(out row)) { return false; }
            if (!stream.ReadT(out column)) { return false; }
            values = new string[row, column];
            for (int i = 0; i < row; ++i)
            {
                for (int j = 0; j < column; j++)
                {
                    string value = "";
                    if (!stream.ReadT(out value)) { return false; }
                    values[i,j] = CUniqueString.Instance.GetUniqueString(value);
                }
            }
            return true;
        }
        #endregion

        #region string[][]
        public static bool SerializeStringArrayTo(string[][] values, ggc.Foundation.CStream stream)
        {
            int iCnt = values.Length;
            if (!stream.WriteT(iCnt)) { return false; }

            int jCnt = values[0].Length;
            if (!stream.WriteT(jCnt)) { return false; }

            for (int i = 0; i < iCnt; i++)
            {
                for (int j = 0; j < jCnt; j++)
                {
                    if (!stream.WriteT(values[i][j])) { return false; }
                }
            }
            return true;
        }
        public static bool UnserializeStringArrayFrom(out string[][] values, ggc.Foundation.CStream stream)
        {
            values = null;

            int iCnt = 0;
            if (!stream.ReadT(out iCnt)) { return false; }
            int jCnt = 0;
            if (!stream.ReadT(out jCnt)) { return false; }

            values = new string[iCnt][];
            for (int i = 0; i < iCnt; i++)
            {
                values[i] = new string[jCnt];

                for (int j = 0; j < jCnt; j++)
                {
                    string value = "";
                    if (!stream.ReadT(out value)) { return false; }
                    values[i][j] = CUniqueString.Instance.GetUniqueString(value);
                }
            }
            return true;
        }
        #endregion

        #region string[][][]
        public static bool SerializeStringArrayTo(string[][][] values, ggc.Foundation.CStream stream)
        {
            int iCnt = values.Length;
            if (!stream.WriteT(iCnt)) { return false; }

            int jCnt = values[0].Length;
            if (!stream.WriteT(jCnt)) { return false; }

            int kCnt = values[0][0].Length;
            if (!stream.WriteT(kCnt)) { return false; }

            for (int i = 0; i < iCnt; i++)
            {
                for (int j = 0; j < jCnt; j++)
                {
                    for (int k = 0; k < kCnt; k++)
                    {
                        if (!stream.WriteT(values[i][j][k])) { return false; }
                    }
                }
            }
            return true;
        }
        public static bool UnserializeStringArrayFrom(out string[][][] values, ggc.Foundation.CStream stream)
        {
            values = null;

            int iCnt = 0;
            if (!stream.ReadT(out iCnt)) { return false; }
            int jCnt = 0;
            if (!stream.ReadT(out jCnt)) { return false; }
            int kCnt = 0;
            if (!stream.ReadT(out kCnt)) { return false; }

            values = new string[iCnt][][];
            for (int i = 0; i < iCnt; i++)
            {
                values[i] = new string[jCnt][];
                for (int j = 0; j < jCnt; j++)
                {
                    values[i][j] = new string[kCnt];
                    for (int k = 0; k < kCnt; k++)
                    {
                        string value = "";
                        if (!stream.ReadT(out value)) { return false; }
                        values[i][j][k] = CUniqueString.Instance.GetUniqueString(value);
                    }
                }
            }
            return true;
        }
        #endregion

        #region string[][][][]
        public static bool SerializeStringArrayTo(string[][][][] values, ggc.Foundation.CStream stream)
        {
            int iCnt = values.Length;
            if (!stream.WriteT(iCnt)) { return false; }

            int jCnt = values[0].Length;
            if (!stream.WriteT(jCnt)) { return false; }

            int kCnt = values[0][0].Length;
            if (!stream.WriteT(kCnt)) { return false; }

            int lCnt = values[0][0][0].Length;
            if (!stream.WriteT(lCnt)) { return false; }

            for (int i = 0; i < iCnt; i++)
            {
                for (int j = 0; j < jCnt; j++)
                {
                    for (int k = 0; k < kCnt; k++)
                    {
                        for (int l = 0; l < lCnt; l++)
                        {
                            if (!stream.WriteT(values[i][j][k][l])) { return false; }
                        }
                    }
                }
            }
            return true;
        }
        public static bool UnserializeStringArrayFrom(out string[][][][] values, ggc.Foundation.CStream stream)
        {
            values = null;

            int iCnt = 0;
            if (!stream.ReadT(out iCnt)) { return false; }
            int jCnt = 0;
            if (!stream.ReadT(out jCnt)) { return false; }
            int kCnt = 0;
            if (!stream.ReadT(out kCnt)) { return false; }
            int lCnt = 0;
            if (!stream.ReadT(out lCnt)) { return false; }

            values = new string[iCnt][][][];
            for (int i = 0; i < iCnt; i++)
            {
                values[i] = new string[jCnt][][];
                for (int j = 0; j < jCnt; j++)
                {
                    values[i][j] = new string[kCnt][];
                    for (int k = 0; k < kCnt; k++)
                    {
                        values[i][j][k] = new string[lCnt];
                        for (int l = 0; l < lCnt; l++)
                        {
                            string value = "";
                            if (!stream.ReadT(out value)) { return false; }
                            values[i][j][k][l] = CUniqueString.Instance.GetUniqueString(value);
                        }
                    }
                }
            }
            return true;
        }
        #endregion

        #region CMapT<keyT, string>
        public static bool SerializeStringMapTo<keyT>(ggc.Foundation.CMapT<keyT, string> map, ggc.Foundation.CStream stream)
            where keyT : struct, System.IConvertible
        {
            int cnt = map.Count;
            if (!stream.WriteT(cnt)) { return false; }
            var ie = map.GetEnumerator();
            while (ie.MoveNext())
            {
                if (!stream.WriteT(ie.Current.Key)) { return false; }
                if (!stream.WriteT(ie.Current.Value)) { return false; }
            }
            return true;
        }
        public static bool UnserializeStringMapFrom<keyT>(out ggc.Foundation.CMapT<keyT, string> map, ggc.Foundation.CStream stream)
            where keyT : struct, System.IConvertible
        {
            map = new ggc.Foundation.CMapT<keyT, string>();
            int cnt = 0;
            if (!stream.ReadT(out cnt)) { return false; }
            for (int i = 0; i < cnt; ++i)
            {
                keyT key = new keyT();
                string value = "";
                if (!stream.ReadT(out key)) { return false; }
                if (!stream.ReadT(out value)) { return false; }
                map.Add(key, value);
            }
            return true;
        }
        #endregion

        #region CMapT<string, valueT>
        public static bool SerializeStringMapTo<valueT>(ggc.Foundation.CMapT<string, valueT> map, ggc.Foundation.CStream stream)
            where valueT : struct, System.IConvertible
        {
            int cnt = map.Count;
            if (!stream.WriteT(cnt)) { return false; }
            var ie = map.GetEnumerator();
            while (ie.MoveNext())
            {
                if (!stream.WriteT(ie.Current.Key)) { return false; }
                if (!stream.WriteT(ie.Current.Value)) { return false; }
            }
            return true;
        }
        public static bool UnserializeStringMapFrom<valueT>(out ggc.Foundation.CMapT<string, valueT> map, ggc.Foundation.CStream stream)
            where valueT : struct, System.IConvertible
        {
            map = new ggc.Foundation.CMapT<string, valueT>();
            int cnt = 0;
            if (!stream.ReadT(out cnt)) { return false; }
            for (int i = 0; i < cnt; ++i)
            {
                string key = "";
                valueT value = new valueT();
                if (!stream.ReadT(out key)) { return false; }
                if (!stream.ReadT(out value)) { return false; }
                map.Add(key, value);
            }
            return true;
        }
        #endregion

        #region CMapT<string, string>
        public static bool SerializeStringMapTo(ggc.Foundation.CMapT<string, string> map, ggc.Foundation.CStream stream)
        {
            int cnt = map.Count;
            if (!stream.WriteT(cnt)) { return false; }
            var ie = map.GetEnumerator();
            while (ie.MoveNext())
            {
                if (!stream.WriteT(ie.Current.Key)) { return false; }
                if (!stream.WriteT(ie.Current.Value)) { return false; }
            }
            return true;
        }
        public static bool UnserializeStringMapFrom(out ggc.Foundation.CMapT<string, string> map, ggc.Foundation.CStream stream)
        {
            map = new ggc.Foundation.CMapT<string, string>();
            int cnt = 0;
            if (!stream.ReadT(out cnt)) { return false; }
            for (int i = 0; i < cnt; ++i)
            {
                string key = "";
                string value = "";
                if (!stream.ReadT(out key)) { return false; }
                if (!stream.ReadT(out value)) { return false; }
                map.Add(key, value);
            }
            return true;
        }
        #endregion

        #region CMapT<string, valueT>
        public static bool SerializeStringObjMapTo<valueT>(ggc.Foundation.CMapT<string, valueT> map, ggc.Foundation.CStream stream)
            where valueT : ISerializable, new()
        {
            int cnt = map.Count;
            if (!stream.WriteT(cnt)) { return false; }
            var ie = map.GetEnumerator();
            while (ie.MoveNext())
            {
                if (!stream.WriteT(ie.Current.Key)) { return false; }
                if (!stream.WriteObjT(ie.Current.Value)) { return false; }
            }
            return true;
        }
        public static bool UnserializeStringObjMapFrom<valueT>(out ggc.Foundation.CMapT<string, valueT> map, ggc.Foundation.CStream stream)
            where valueT : ISerializable, new()
        {
            map = new ggc.Foundation.CMapT<string, valueT>();
            int cnt = 0;
            if (!stream.ReadT(out cnt)) { return false; }
            for (int i = 0; i < cnt; ++i)
            {
                string key = "";
                valueT value = default(valueT);
                if (!stream.ReadT(out key)) { return false; }
                if (!stream.ReadObjT(out value)) { return false; }
                map.Add(key, value);
            }
            return true;
        }
        #endregion

        #region CMapT<string, List<valueT>>
        public static bool SerializeStringMapTo(ggc.Foundation.CMapT<string, List<string>> map, ggc.Foundation.CStream stream)
        {
            int cnt = map.Count;
            if (!stream.WriteT(cnt)) { return false; }
            var ie = map.GetEnumerator();
            while (ie.MoveNext())
            {
                if (!stream.WriteT(ie.Current.Key)) { return false; }
                if (!stream.WriteT(ie.Current.Value)) { return false; }
            }
            return true;
        }
        public static bool UnserializeStringMapFrom(out ggc.Foundation.CMapT<string, List<string>> map, ggc.Foundation.CStream stream)
        {
            map = new ggc.Foundation.CMapT<string, List<string>>();
            int cnt = 0;
            if (!stream.ReadT(out cnt)) { return false; }
            for (int i = 0; i < cnt; ++i)
            {
                string key = "";
                List<string> value = null;
                if (!stream.ReadT(out key)) { return false; }
                if (!stream.ReadT(out value)) { return false; }
                map.Add(key, value);
            }
            return true;
        }
        #endregion
        #endregion
        #endregion
    }
    #endregion
}
