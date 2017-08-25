// ============================================
// Created: 2014/12/03
// Author: Jeff
// Brief: 
// ============================================

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Mga.SHLib
{

    public abstract class CBaseTableInfo : ISerializable
    {

        #region Properties
        public abstract int Version { get; }
        protected int StreamVersion { get { return m_streamVersion; } }
        #endregion

        #region Serialize
        public virtual bool SerializeTo(ggc.Foundation.CStream stream)
        {
            stream.WriteT(Version);
            return true;
        }

        public virtual bool UnserializeFrom(ggc.Foundation.CStream stream)
        {
            stream.ReadT(out m_streamVersion);
            if (m_streamVersion != Version)
                return false;
            return true;
        }
        #endregion

        #region Field
        int m_streamVersion;
        #endregion
    }

    public class CBaseTableMoveInfo : ISerializable
    {
        public float StartTime;
        public float EndTime;
        public Vector3 Movement;        // program movement
        public float BlendTime;         // blend time

        #region ISerializable
        public virtual bool SerializeTo(ggc.Foundation.CStream stream)
        {
            stream.WriteT(StartTime);
            stream.WriteT(EndTime);
            stream.WriteT(Movement);
            stream.WriteT(BlendTime);
            return true;
        }

        public virtual bool UnserializeFrom(ggc.Foundation.CStream stream)
        {
            stream.ReadT(out StartTime);
            stream.ReadT(out EndTime);
            stream.ReadT(out Movement);
            stream.ReadT(out BlendTime);
            return true;
        }
        #endregion
    }

    public static class CTableUtility
    {
        #region CombineID
        public static int CombineID(ushort id1, byte id2)
        {
            return ((int)id1 << 16) | id2;
        }
        public static int CombineID(ushort id1, byte id2, byte id3)
        {
            return ((int)id1 << 16) | ((int)id2 << 8) | id3;
        }
        public static int CombineID(int id1, byte id2)
        {
            return (id1 << 16 | id2);
        }
        public static byte CombineByte(byte id1, byte id2)
        {
            return (byte)((id1 << 4) | (id2 & 0xf));
        }
        #endregion

        #region Check
        public static bool IsValidStringValue(string value)
        {
            return !string.IsNullOrEmpty(FilterStringValue(value));
        }

        public static string FilterStringValue(string value)
        {
            if (!string.IsNullOrEmpty(value) && value != "0")
                return value;
            return "";
        }
        #endregion

        #region String To Byte[], KeyFrame, etc, and vice verse
        public static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static string GetString(byte[] bytes)
        {
            int iCharLen = bytes.Length / sizeof(char);
            if (bytes.Length % 2 == 1)
                iCharLen += 1;

            char[] chars = new char[iCharLen];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        public static string KeyFrame2String(Keyframe keyFrame, char sp = ',')
        {
            return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}", System.Math.Round(keyFrame.time, 3), sp,
                                                                System.Math.Round(keyFrame.value, 3), sp,
                                                                System.Math.Round(keyFrame.inTangent, 3), sp,
                                                                System.Math.Round(keyFrame.outTangent, 3), sp,
                                                                keyFrame.tangentMode);
        }

        public static bool String2KeyFrame(string s, out Keyframe keyFrame, char sp = ',')
        {
            keyFrame = new Keyframe();
            try
            {
                var values = s.Split(new char[] { sp });
                keyFrame.time = System.Convert.ToSingle(values[0]);
                keyFrame.value = System.Convert.ToSingle(values[1]);
                keyFrame.inTangent = System.Convert.ToSingle(values[2]);
                keyFrame.outTangent = System.Convert.ToSingle(values[3]);
                keyFrame.tangentMode = System.Convert.ToInt32(values[4]);
                return true;
            }
            catch
            {
                ggc.Foundation.Log.LogErrorMsg("Wrong KeyFrame Value: " + s);
                return false;
            }
        }

        public static string AnimationCurve2String(AnimationCurve curve, char sp = ';')
        {
            string ret = "";
            foreach (var keyFrame in curve.keys)
            {
                if (!string.IsNullOrEmpty(ret))
                    ret += sp;
                ret += KeyFrame2String(keyFrame);
            }
            return ret;
        }

        public static AnimationCurve String2AnimationCurve(string curveString, char sp = ';')
        {
            UnityEngine.AnimationCurve curve = new UnityEngine.AnimationCurve();

            if (string.IsNullOrEmpty(curveString))
                return curve;

            try
            {
                var curveValues = curveString.Split(new char[] { sp });
                foreach (var keyFrameValue in curveValues)
                {
                    Keyframe keyFrame = new Keyframe();
                    if (!String2KeyFrame(keyFrameValue, out keyFrame))
                    {
                        ggc.Foundation.Log.LogErrorMsg("Wrong KeyFrame Value:" + keyFrameValue + " in AnimationCurve Config: " + curveString);
                        continue;
                    }
                    curve.AddKey(keyFrame);
                }
                return curve;
            }
            catch
            {
                ggc.Foundation.Log.LogErrorMsg("Wrong AnimationCurve Value: " + curveString);
                return curve;
            }
        }

        public static string Vector22String(Vector2 v, char sp = ',')
        {
            return string.Format("{0}{1}{2}", v.x, sp, v.y);
        }

        public static Vector2 String2Vector2(string valueStr, char sp = ',')
        {
            if (valueStr == string.Empty || valueStr == null)
                return Vector2.zero;
            Vector2 value = new Vector2();
            var valueArr = valueStr.Split(new char[] { sp });
            if (valueArr.Length >= 1)
                value.x = System.Convert.ToSingle(valueArr[0]);
            if (valueArr.Length >= 2)
                value.y = System.Convert.ToSingle(valueArr[1]);
            return value;
        }

        public static string Vector32String(Vector3 v, char sp = ',')
        {
            return string.Format("{0}{1}{2}{3}{4}", v.x, sp, v.y, sp, v.z);
        }

        public static Vector3 String2Vector3(string valueStr, char sp = ',')
        {
            if (valueStr == string.Empty || valueStr == null)
                return Vector3.zero;
            Vector3 value = new Vector3();
            var valueArr = valueStr.Split(new char[] { sp });
            if (valueArr.Length >= 1)
                value.x = System.Convert.ToSingle(valueArr[0]);
            if (valueArr.Length >= 2)
                value.y = System.Convert.ToSingle(valueArr[1]);
            if (valueArr.Length >= 3)
                value.z = System.Convert.ToSingle(valueArr[2]);
            return value;
        }

        public static void String2TimeVector3(string valueStr, out float t0, out float t1, out Vector3 value, char sp = ',')
        {
            t0 = 0;
            t1 = 0;
            value = Vector3.zero;

            try
            {
                if (string.IsNullOrEmpty(valueStr))
                    return;

                // valueStr: "t0, t1, x, y, z"
                //
                var valueArr = valueStr.Split(new char[] { sp });

                if (valueArr.Length >= 1)
                    t0 = System.Convert.ToSingle(valueArr[0]) * CConstant.TIME_VALUE_MS_2_SEC;
                if (valueArr.Length >= 2)
                    t1 = System.Convert.ToSingle(valueArr[1]) * CConstant.TIME_VALUE_MS_2_SEC;

                if (valueArr.Length >= 3)
                    value.x = System.Convert.ToSingle(valueArr[2]);
                if (valueArr.Length >= 4)
                    value.y = System.Convert.ToSingle(valueArr[3]);
                if (valueArr.Length >= 5)
                    value.z = System.Convert.ToSingle(valueArr[4]);
            }
            catch (System.Exception e)
            {
                ggc.Foundation.Log.LogErrorMsg("String2TimeVector3: wrong value string:" + valueStr);
                ggc.Foundation.Log.LogErrorMsg(e.ToString());
            }
        }

        public static Color32 String2Color32(string valueStr, char sp = ',')
        {
            Color32 value = new Color32();
            if (valueStr == string.Empty || valueStr == null)
                return value;

            var valueArr = valueStr.Split(new char[] { sp });
            if (valueArr.Length >= 1)
                value.r = System.Convert.ToByte(valueArr[0]);
            if (valueArr.Length >= 2)
                value.g = System.Convert.ToByte(valueArr[1]);
            if (valueArr.Length >= 3)
                value.b = System.Convert.ToByte(valueArr[2]);
            if (valueArr.Length >= 4)
                value.a = System.Convert.ToByte(valueArr[3]);
            return value;
        }

        public static string Vector3List2String(List<Vector3> vecList, char sp = ';')
        {
            string ret = "";
            foreach (var vec in vecList)
            {
                if (!string.IsNullOrEmpty(ret))
                    ret += sp;
                ret += Vector32String(vec);
            }
            return ret;
        }

        public static bool String2Vector3List(string str, List<Vector3> vecList, char separatorChar = ';')
        {
            if (string.IsNullOrEmpty(str) || str == null)
                return false;

            char[] separator = new char[] { separatorChar };
            string[] vecValues = str.Split(separator);
            foreach (string vecValue in vecValues)
            {
                try
                {
                    vecList.Add(String2Vector3(vecValue));
                }
                catch
                {
                    ggc.Foundation.Log.LogErrorMsg("Wrong Vector3 Value: " + vecValue + " in table value: " + str);
                    return false;
                }
            }
            vecList.Capacity = vecList.Count;

            return true;
        }

        public static bool String2IntList(string str, List<int> intList, char separatorChar = ';')
        {
            if (string.IsNullOrEmpty(str) || str == null)
                return false;

            char[] separator = new char[] { separatorChar };
            string[] intValues = str.Split(separator);
            foreach (string intValue in intValues)
            {
                try
                {
                    int value = System.Convert.ToInt32(intValue);
                    intList.Add(value);
                }
                catch
                {
                    ggc.Foundation.Log.LogErrorMsg("Wrong Int Id: " + intValue + " in table value: " + str);
                    return false;
                }
            }
            intList.Capacity = intList.Count;

            return true;
        }
        public static bool String2FloatList(string str, List<float> intList, char separatorChar = ';')
        {
            if (string.IsNullOrEmpty(str) || str == null)
                return false;

            char[] separator = new char[] { separatorChar };
            string[] intValues = str.Split(separator);
            foreach (string intValue in intValues)
            {
                try
                {
                    float value = float.Parse(intValue);
                    intList.Add(value);
                }
                catch
                {
                    ggc.Foundation.Log.LogErrorMsg("Wrong Int Id: " + intValue + " in table value: " + str);
                    return false;
                }
            }
            intList.Capacity = intList.Count;

            return true;
        }
        public static bool String2IdLvlList(string str, List<int> idLvlList, char separatorChar = ';')
        {
            if (string.IsNullOrEmpty(str))
                return true;

            char[] separator = new char[] { separatorChar };
            string[] values = str.Split(separator);

            try
            {
                char[] idLvlSeparator = new char[] { ',' };
                foreach (var idLvlValue in values)
                {
                    string[] idLvlArr = idLvlValue.Split(idLvlSeparator);
                    int id = System.Convert.ToInt32(idLvlArr[0]);
                    byte level = (byte)System.Convert.ToInt32(idLvlArr[1]);
                    idLvlList.Add(CBattleCommonDef.MakeIDLevel(id, level));
                }

            }
            catch
            {
                ggc.Foundation.Log.LogErrorMsg("Wrong Skill Id Lvl in table value: " + str);
                return false;
            }
            return true;
        }

        public static List<string> String2StringList(string str, char separatorChar = ';')
        {
            List<string> strs = new List<string>();
            if (string.IsNullOrEmpty(str))
                return strs;

            char[] separator = new char[] { separatorChar };
            string[] values = str.Split(separator);
            foreach (var value in values)
            {
                if (FilterStringValue(value) == "")
                    continue;
                strs.Add(value);
            }
            return strs;
        }
        public static List<string> String2StringList2(string str, char separatorChar = ';')
        {
            List<string> strs = new List<string>();
            if (string.IsNullOrEmpty(str))
                return strs;

            char[] separator = new char[] { separatorChar };
            string[] values = str.Split(separator);
            foreach (var value in values)
            {
                if (string.IsNullOrEmpty(value)) continue;
                strs.Add(value);
            }
            return strs;
        }
        public static bool String2StringList(string str, List<string> strList, char separatorChar = ';')
        {
            if (string.IsNullOrEmpty(str))
                return true;

            char[] separator = new char[] { separatorChar };
            string[] values = str.Split(separator);
            foreach (var value in values)
            {
                if (FilterStringValue(value) == "")
                    continue;
                strList.Add(value);
            }

            return true;
        }

        public static bool String2StringList2(string str, List<string> strList, char separatorChar = ';')
        {
            if (string.IsNullOrEmpty(str))
                return true;

            char[] separator = new char[] { separatorChar };
            string[] values = str.Split(separator);
            foreach (var value in values)
            {
                if (string.IsNullOrEmpty(value)) continue;
                strList.Add(value);
            }

            return true;
        }

        public static int String2IdLvl(string str, char separatorChar = ',')
        {
            int id = CBattleCommonDef.INVALID_SKILL_ID;
            byte lvl = CBattleCommonDef.INVALID_SKILL_LEVEL;

            char[] separator = new char[] { separatorChar };
            string[] values = str.Split(separator);

            try
            {
                id = System.Convert.ToInt32(values[0]);
                lvl = (byte)System.Convert.ToInt32(values[1]);
                return CBattleCommonDef.MakeIDLevel(id, lvl);
            }
            catch
            {
                ggc.Foundation.Log.LogErrorMsg("Wrong Id Lvl in table value: " + str);
                return 0;
            }
        }

        public static bool String2IdLvl(string str, out int skillId, out byte skillLvl, char separatorChar = ',')
        {
            skillId = CBattleCommonDef.INVALID_SKILL_ID;
            skillLvl = CBattleCommonDef.INVALID_SKILL_LEVEL;

            char[] separator = new char[] { separatorChar };
            string[] values = str.Split(separator);

            try
            {
                skillId = System.Convert.ToInt32(values[0]);
                skillLvl = (byte)System.Convert.ToInt32(values[1]);
            }
            catch
            {
                ggc.Foundation.Log.LogErrorMsg("Wrong Id Lvl in table value: " + str);
                return false;
            }
            return true;
        }
        #endregion
    }
}
