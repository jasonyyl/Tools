// ============================================
// Created: 2014/09/09
// Author: Jeff
// Brief: 
// ============================================

using UnityEngine;
using System.Collections;

namespace Mga.SHLib
{
    public class CUniqueString : Singleton<CUniqueString>
    {
        public const string EmptyString = "";

        public CUniqueString()
        {
            m_uniqueStrings = new ggc.Foundation.CMapT<string, string>();
        }

        public string GetUniqueString(string s)
        {
            if (!m_uniqueStrings.ContainsKey(s))
                m_uniqueStrings[s] = s;
            return m_uniqueStrings[s];
        }

        #region Fields
        ggc.Foundation.CMapT<string, string> m_uniqueStrings;    // since cant use HastSet, use this instead of
        #endregion
    }
}
