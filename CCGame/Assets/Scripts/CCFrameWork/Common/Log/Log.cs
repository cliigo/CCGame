using System;

namespace CCFrameWork.Common.LOG
{
    public class Log
    {
        public static Log CreateLog(Type class_type)
        {
            return   new Log(class_type);
        }

        public string _class_type;

        public Log(Type class_type)
        {
            this._class_type = class_type.ToString();
        }

        public void Debug(string info)
        {
            UnityEngine.Debug.Log(string.Format("[{0}][{1}] {2}", _get_time_str(), _class_type, info));
        }

        public void Warn(string info)
        {
            UnityEngine.Debug.LogWarning(string.Format("[{0}][{1}] {2}", _get_time_str(), _class_type, info));
        }

        public void Error(string info)
        {
            UnityEngine.Debug.LogError(string.Format("[{0}][{1}] {2}", _get_time_str(), _class_type, info));
        }

        public string _get_time_str()
        {
            return DateTime.Now.ToString();
        }
    }
}