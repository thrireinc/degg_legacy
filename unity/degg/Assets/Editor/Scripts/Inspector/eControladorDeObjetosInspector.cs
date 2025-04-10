using System;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(bControladorDeObjetos))]
public class eControladorDeObjetosInspector : Editor
{
    private SerializedProperty _inimigos, _powerUps, _outros;

    private void OnEnable()
    {
        _inimigos = serializedObject.FindProperty("inimigos");
        _powerUps = serializedObject.FindProperty("powerUps");
        _outros = serializedObject.FindProperty("outros");
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical();
        
            EditorGUILayout.LabelField("Referências para Objetos", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(_inimigos, new GUIContent("Inimigos"));
            EditorGUILayout.PropertyField(_powerUps, new GUIContent("Power Ups"));
            EditorGUILayout.PropertyField(_outros, new GUIContent("Misc"));
            
        EditorGUILayout.EndVertical();
        
        if(EditorGUI.EndChangeCheck())
            serializedObject.ApplyModifiedProperties();
    }
}
