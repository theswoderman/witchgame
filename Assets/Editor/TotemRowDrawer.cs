using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(Row))]
public class TotemRowDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty rowProp = property.FindPropertyRelative("cell");

        if (rowProp == null || !rowProp.isArray) return;

        float cellSize = 20f;

        for (int i = 0; i < rowProp.arraySize; i++)
        {
            Rect cellRect = new Rect(position.x + i * cellSize, position.y, cellSize, cellSize);
            SerializedProperty cell = rowProp.GetArrayElementAtIndex(i);
            cell.boolValue = EditorGUI.Toggle(cellRect, cell.boolValue);
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return 20f;
    }
}