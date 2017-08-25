// ============================================
// Created: 2014/12/08
// Author: Jeff
// Brief: 
// ============================================

namespace Mga.SHLib
{
    //! 流化/反流化接口
    public interface ISerializable
    {
        bool SerializeTo(ggc.Foundation.CStream stream);
        bool UnserializeFrom(ggc.Foundation.CStream stream);
    }
}
