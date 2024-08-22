using UnityEngine;

namespace Blahaj
{
    internal static class LogHelper
    {
        public static void Log(string message)
        {
            Debug.Log("[Blahaj] " + message);
        }

        public static void Warn(string message)
        {
            Debug.LogWarning("[Blahaj Warning] " + message);
        }

        public static void Error(string message)
        {
            Debug.LogError("[Blahaj Error] " + message);
        }
    }
}