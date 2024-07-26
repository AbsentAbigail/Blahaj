using Deadpan.Enums.Engine.Components.Modding;
using UnityEngine;

namespace Blahaj
{
    static class LogHelper
    {
        public static void Log(string message)
        {
            Debug.Log("[Blahaj] " + message);
        }
    }
}