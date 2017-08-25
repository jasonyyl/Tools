// ============================================
// Created: 2014/11/03
// Author: Jeff
// Brief: 
// ============================================

using UnityEngine;
using System.Collections;

namespace Mga.SHLib
{
    public interface ITable
    {
        bool LoadTable(string path);
        bool CheckTable();

        bool LoadFromBinary(byte[] content);
        bool SaveToBinary(string path);

        string TableName { get; }
    }
}
