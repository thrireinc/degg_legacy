using System;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(bBala))]
public class eBalaInspector : Editor
{
    private SerializedProperty _forcaMovimentoVertical;

    private void OnEnable()
    {
        _forcaMovimentoVertical = serializedObject.FindProperty("forcaMovimentoVertical");
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField("Características Gerais", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(_forcaMovimentoVertical, new GUIContent("Velocidade"));
        EditorGUILayout.EndVertical();
        
        if(EditorGUI.EndChangeCheck())
            serializedObject.ApplyModifiedProperties();
    }
}
