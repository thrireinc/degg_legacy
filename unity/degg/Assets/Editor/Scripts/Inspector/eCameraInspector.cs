using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(bCamera))]
public class eCameraInspector : Editor
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
