using System;
using System.Globalization;
using UnityEngine;
using UnityEditor;

public class CunstomAttributes
{
}

#if UNITY_EDITOR
public class ReadOnlyFieldAttribute : PropertyAttribute
{
    public float value;
}
#endif


// public class ReadOnlyFieldAttribute : PropertyAttribute
// {
//     public float value;
//
//     // [AttributeUsage("This is readonly SerializeField.")]
//     // public void ToReadOnly()
//     // {
//     // }
// }

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(ReadOnlyFieldAttribute))]
public class ReadOnlyFieldDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Debug.Log(property);
        ReadOnlyFieldAttribute range2Attribute = (ReadOnlyFieldAttribute) attribute;
        EditorGUILayout.PropertyField(property);
    }
}
#endif

// public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
// {
//     ReadOnlyFieldAttribute readOnlyField = (ReadOnlyFieldAttribute) attribute;
//
//     if (property.propertyType == SerializedPropertyType.Float) {
//         readOnlyField.value = float.Parse(EditorGUILayout.TextField(label,
//             readOnlyField.value.ToString(CultureInfo.InvariantCulture)));
//     }
// }
