using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TM
{
    public static class CDataType
    {
        public const string DataType_INT = "int";
        public const string DataType_FLOAT = "float";
        public const string DataType_BOOL = "bool";
        public const string DataType_STRING = "string";
        public const string DataType_VECTOR3 = "vector3";
        public const string DataType_VECTOR2 = "vector2";

        public static string ChangeToDataType(string input)
        {
            if (input.Substring(0, 3).ToLower() == "int")
                return "int";
            if (input.Substring(0, 4).ToLower() == "bool" || input.ToLower() == "boolean")
                return "bool";
            if (input.Substring(0, 5).ToLower() == "float")
                return "float";
            if (input.Substring(0, 6).ToLower() == "string")
                return "string";
            if (input.Substring(0, 7).ToLower() == "vector2")
                return "Vector2";
            if (input.Substring(0, 7).ToLower() == "vector3")
                return "Vector3";
            return string.Empty;
        }
    }
}
