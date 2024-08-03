using UnityEngine;

namespace Blahaj
{
    internal static class LogHelper
    {
        public static void Log(string message)
        {
            Debug.Log("[Blahaj] " + message);
        }
    }
}