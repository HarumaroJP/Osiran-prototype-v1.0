using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public static class ExtentionMethods
{
    public static bool InRange(this float t, float start, float end) => start <= t && t <= end;

#if UNITY_EDITOR
    public static void ClearLog()
    {
        Assembly assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
        Type type = assembly.GetType("UnityEditor.LogEntries");
        MethodInfo method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
    }
#endif
}
