using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(bInimigo))]
public class eInimigoInspector : Editor
{
    private SerializedProperty _campoDeVisao, _forcaMovimento, _delayTiro, _velocidadeMorte, _armas, _bala, _somTiro, _olho, _numeroDeVidas;

    private void OnEnable()
    {
        _campoDeVisao = serializedObject.FindProperty("campoDeVisao");
        _forcaMovimento = serializedObject.FindProperty("forcaMovimento");
        _delayTiro = serializedObject.FindProperty("delayTiro");
        _velocidadeMorte = serializedObject.FindProperty("velocidadeMorte");
        _armas = serializedObject.FindProperty("armas");
        _bala = serializedObject.FindProperty("bala");
        _somTiro = serializedObject.FindProperty("somTiro");
        _olho = serializedObject.FindProperty("olho");
        _numeroDeVidas = serializedObject.FindProperty("numeroDeVidas");
    }
    
    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField("Características Gerais", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(_numeroDeVidas, new GUIContent("Número de Vidas"));
            EditorGUILayout.PropertyField(_campoDeVisao, new GUIContent("Campo de Visão"));
            EditorGUILayout.PropertyField(_forcaMovimento, new GUIContent("Velocidade"));
            EditorGUILayout.PropertyField(_delayTiro, new GUIContent("Delay entre tiros"));
            EditorGUILayout.PropertyField(_velocidadeMorte, new GUIContent("Velocidade da morte"));
            EditorGUILayout.Space();
            
            EditorGUILayout.LabelField("Referências para Objetos", EditorStyles.boldLabel);
            EditorGUILayout.Space();
            
            EditorGUILayout.PropertyField(_somTiro, new GUIContent("Som do Tiro"));
            EditorGUILayout.PropertyField(_bala, new GUIContent("Bala"));
            EditorGUILayout.PropertyField(_olho, new GUIContent("Olho"));
            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(_armas, new GUIContent("Armas"));
        EditorGUILayout.EndVertical();
        
        if(EditorGUI.EndChangeCheck())
            serializedObject.ApplyModifiedProperties();
    }

}