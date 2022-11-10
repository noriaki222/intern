using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UI;

[CustomEditor(typeof(DynamickMask))]
public class DynamicMaskEditor : GraphicEditor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var mask = (DynamickMask)target;
        mask.vert1 = EditorGUILayout.Vector2Field("Vert 1", mask.vert1);
        mask.vert2 = EditorGUILayout.Vector2Field("Vert 2", mask.vert2);
        mask.vert3 = EditorGUILayout.Vector2Field("Vert 3", mask.vert3);
        mask.vert4 = EditorGUILayout.Vector2Field("Vert 4", mask.vert4);
    }
}
