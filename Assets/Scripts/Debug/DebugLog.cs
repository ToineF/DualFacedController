using UnityEngine;

namespace Common.DebugLog
{
    public class DebugLog : MonoBehaviour
    {
        public void Log(string message)
        {
            UnityEngine.Debug.Log(message);
        }
        
        public void LogWarning(string message)
        {
            UnityEngine.Debug.LogWarning(message);
        }
        
        public void LogError(string message)
        {
            UnityEngine.Debug.LogError(message);
        }
    }
}