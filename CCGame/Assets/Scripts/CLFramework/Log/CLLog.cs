using System;
using System.Collections.Generic;

namespace CLFramework.Log
{
    public class CLLog
    {
        private static Dictionary<Type, CLLog> _log_dict = new Dictionary<Type, CLLog>();

        public static CLLog CreateLog(Type type)
        {
            CLLog log = new CLLog(type);
            _log_dict[type] = log;
            return log;
        }


        private string type_str;

        protected CLLog(Type type)
        {
            type_str = type.ToString();
        }

        public void Log(string str)
        {
            UnityEngine.Debug.Log(String.Format("[{1}] [{2}] {3}", type_str, new DateTime().ToString(), str));
        }

        public void Warn(string str)
        {
            UnityEngine.Debug.LogWarning(String.Format("[{1}] [{2}] {3}", type_str, new DateTime().ToString(), str));
        }

        public void Error(string str)
        {
            UnityEngine.Debug.LogError(String.Format("[{1}] [{2}] {3}", type_str, new DateTime().ToString(), str));
        }
    }
}