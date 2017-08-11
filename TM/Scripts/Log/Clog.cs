using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TM
{
    public enum EMsgType
    {
        Invaild,
        Msg_Normal,
        Msg_Warning,
        Msg_Error,

    }
    public class Clog : CSingleton<Clog>
    {
        public event Action<string, EMsgType, bool> LogMsgEvent;
        public void Log(string Msg, bool isClearBefore = false)
        {
            if (LogMsgEvent != null)
            {
                LogMsgEvent(Msg, EMsgType.Msg_Normal, isClearBefore);
            }
        }
        public void LogWarning(string Msg, bool isClearBefore = false)
        {
            if (LogMsgEvent != null)
            {
                LogMsgEvent(Msg, EMsgType.Msg_Warning, isClearBefore);
            }
        }

        public void LogError(string Msg, bool isClearBefore = false)
        {
            if (LogMsgEvent != null)
            {
                LogMsgEvent(Msg, EMsgType.Msg_Error, isClearBefore);
            }
        }
    }
}
