using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(bMain))]
public class eMainInspector : Editor
{
    private SerializedProperty _numeroDeVidas, _forcaMovimento, _delayTiro, _velocidadeMorte, _escudo, _armas, _bala, _somTiro, _somDano, _delayFrigideira, _delayEspantalho, _duracaoFrigideira;

    private void OnEnable()
    {
        _numeroDeVidas = serializedObject.FindProperty("numeroDeVidas");
        _forcaMovimento = serializedObject.FindProperty("forcaMovimento");
        _delayTiro = serializedObject.FindProperty("delayTiro");
        _velocidadeMorte = serializedObject.FindProperty("velocidadeMorte");
        _escudo = serializedObject.FindProperty("escudo");
        _armas = serializedObject.FindProperty("armas");
        _bala = serializedObject.FindProperty("bala");
        _somTiro = serializedObject.FindProperty("somTiro");
        _somDano = serializedObject.FindProperty("somDano");
        _delayFrigideira = serializedObject.FindProperty("delayFrigideira");
        _delayEspantalho = serializedObject.FindProperty("delayEspantalho");
        _duracaoFrigideira = serializedObject.FindProperty("duracaoFrigideira");
    }
    
    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField("Características Gerais", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(_numeroDeVidas, new GUIContent("Número de vidas"));
            EditorGUILayout.PropertyField(_forcaMovimento, new GUIContent("Velocidade"));
            EditorGUILayout.PropertyField(_delayTiro, new GUIContent("Delay entre tiros"));
            EditorGUILayout.PropertyField(_duracaoFrigideira, new GUIContent("Duração da frigideira"));
            EditorGUILayout.PropertyField(_delayFrigideira, new GUIContent("Delay para usar a frigideira"));
            EditorGUILayout.PropertyField(_delayEspantalho, new GUIContent("Delay para usar o espantalho"));
            EditorGUILayout.PropertyField(_velocidadeMorte, new GUIContent("Velocidade da morte"));
            EditorGUILayout.Space();
            
            EditorGUILayout.LabelField("Referências para Objetos", EditorStyles.boldLabel);
            EditorGUILayout.Space();
            
            EditorGUILayout.PropertyField(_escudo, new GUIContent("Escudo"));
            EditorGUILayout.PropertyField(_somTiro, new GUIContent("Som do Tiro"));
            EditorGUILayout.PropertyField(_somDano, new GUIContent("Som do Dano"));
            EditorGUILayout.PropertyField(_bala, new GUIContent("Bala"));
            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(_armas, new GUIContent("Armas"));
        EditorGUILayout.EndVertical();
        
        if(EditorGUI.EndChangeCheck())
            serializedObject.ApplyModifiedProperties();
    }

}
