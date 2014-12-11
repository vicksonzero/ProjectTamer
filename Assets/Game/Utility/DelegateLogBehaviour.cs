using UnityEngine;
using System.Collections;

public class DelegateLogBehaviour : MonoBehaviour
{
    public string output = "";
    public string stack = "";
    void OnEnable()
    {
        Application.RegisterLogCallback(HandleLog);
    }
    void OnDisable()
    {
        Application.RegisterLogCallback(null);
    }
    void HandleLog(string logString, string stackTrace, LogType type)
    {
        output = logString;
        stack = stackTrace;
    }
}