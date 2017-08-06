using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TM
{
    public enum EFileType
    {
        Invaild,
        Text,
        Excel,

    }

    public enum EFileFlag
    {
        Invaild,
        File_Exist,
        Path_NotExist,
        Directory_NotExist,
        Success,

    }
    public class CFileManager
    {

        public EFileFlag CreateFile(string dir, string name, EFileType ft)
        {
            if (Directory.Exists(dir))
            {
                string ext = string.Empty;
                string path = Path.Combine(dir, name);
                switch (ft)
                {
                    case EFileType.Text: ext = ".txt"; break;
                    case EFileType.Excel: ext = ".xlsm"; break;
                    default:
                        break;
                }
                path += ext;
                if (File.Exists(path))
                {
                    return EFileFlag.File_Exist;
                }
                else
                {
                    File.Create(path);
                }
                return EFileFlag.Success; ;
            }
            else
            {
                return EFileFlag.Directory_NotExist;
            }
        }
        public EFileFlag CreateDirectory(string dir, string name)
        {
            if (Directory.Exists(dir))
            {
                string p = Path.Combine(dir, name);
                Directory.CreateDirectory(p);
                return EFileFlag.Success;
            }
            else
            {
                return EFileFlag.Directory_NotExist;
            }
        }
        public EFileFlag Delete(string path)
        {
            bool isExist = false;
            if (Directory.Exists(path))
            {
                isExist = true;
                Directory.Delete(path, true);
            }
            if (File.Exists(path))
            {
                isExist = true;
                File.Delete(path);
            }
            if (isExist)
            {
                return EFileFlag.Success;
            }
            else
            {
                return EFileFlag.Path_NotExist;
            }

        }
    }
}
