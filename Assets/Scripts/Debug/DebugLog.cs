using UnityEngine;

namespace Common.DebugLog
{
    public class DebugLog : MonoBehaviour
    {
        public void Log(string message)
        {
            UnityEngine.Debug.Log(message);
        }
    }
}