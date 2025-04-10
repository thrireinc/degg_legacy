using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(bGerenciadorDeNivel))]
public class eGerenciadorDeNivelInspector : Editor 
{

    SerializedProperty _numeroMaximoDeInimigosNaTela, _delaySpawnInimigo, _offsetHorizontal, _offsetVertical, _delaySpawnPowerUp, _delaySpawnMisc, _cO_controller;

    private void OnEnable()
    {
        _numeroMaximoDeInimigosNaTela = serializedObject.FindProperty("numeroMaximoDeInimigosNaTela");
        _delaySpawnInimigo = serializedObject.FindProperty("delaySpawnInimigo");
        _delaySpawnPowerUp = serializedObject.FindProperty("delaySpawnPowerUp");
        _delaySpawnMisc = serializedObject.FindProperty("delaySpawnMisc");
        _offsetHorizontal = serializedObject.FindProperty("offsetHorizontal");
        _offsetVertical = serializedObject.FindProperty("offsetVertical");
        _cO_controller = serializedObject.FindProperty("cO_controller");
    }
    
    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField("Características Gerais", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(_numeroMaximoDeInimigosNaTela, new GUIContent("Número máximo de inimigos na tela"));
            EditorGUILayout.PropertyField(_delaySpawnInimigo, new GUIContent("Delay de spawn dos inimigos"));
            EditorGUILayout.PropertyField(_delaySpawnPowerUp, new GUIContent("Delay de spawn dos powerups"));
            EditorGUILayout.PropertyField(_delaySpawnMisc, new GUIContent("Delay de spawn dos miscs"));
            EditorGUILayout.PropertyField(_offsetHorizontal, new GUIContent("Offset horizontal da tela"));
            EditorGUILayout.PropertyField(_offsetVertical, new GUIContent("Offset vertical da tela"));
            EditorGUILayout.Space();
            
            EditorGUILayout.LabelField("Referências para Objetos", EditorStyles.boldLabel);
            EditorGUILayout.Space();
            
            EditorGUILayout.PropertyField(_cO_controller, new GUIContent("Controlador de objetos"));
        EditorGUILayout.EndVertical();
        
        if(EditorGUI.EndChangeCheck())
            serializedObject.ApplyModifiedProperties();
    }
}
