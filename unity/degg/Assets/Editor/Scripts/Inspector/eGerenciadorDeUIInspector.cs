using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(bGerenciadorDeUI))]
public class eGerenciadorDeUIInspector : Editor
{
    private SerializedProperty _instanciaPlayer, _instanciaBoss, _tamanhoTelaDeReferencia, _offsetTela, _lifeIcoPosOffset, _referenciaCanvas, _posicaoVidas, _txtPontuacao, _sldrVida, _preenchimentoVida, _gradienteVida, _txtFrigideira, _txtEspantalho, _imgFrigideira, _imgEspantalho;

    private void OnEnable()
    {
        _instanciaPlayer = serializedObject.FindProperty("instanciaPlayer");
        _instanciaBoss = serializedObject.FindProperty("instanciaBoss");
        _tamanhoTelaDeReferencia = serializedObject.FindProperty("tamanhoDaTelaDeReferencia");
        _offsetTela = serializedObject.FindProperty("offsetTela");
        _lifeIcoPosOffset = serializedObject.FindProperty("lifeIcoPosOffset");
        _referenciaCanvas = serializedObject.FindProperty("referenciaCanvas");
        _posicaoVidas = serializedObject.FindProperty("posicaoVidas");
        _txtPontuacao = serializedObject.FindProperty("txtPontuacao");
        _sldrVida = serializedObject.FindProperty("sldrVida");
        _preenchimentoVida = serializedObject.FindProperty("preenchimentoVida");
        _gradienteVida = serializedObject.FindProperty("gradienteSldrVida");
        _txtFrigideira = serializedObject.FindProperty("txtFrigideira");
        _txtEspantalho = serializedObject.FindProperty("txtEspantalho");
        _imgEspantalho = serializedObject.FindProperty("imgEspantalho");
        _imgFrigideira = serializedObject.FindProperty("imgFrigideira");
    }
    
    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField("Características Gerais", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(_tamanhoTelaDeReferencia, new GUIContent("Tamanho da tela (referência)"));
            EditorGUILayout.PropertyField(_offsetTela, new GUIContent("Offset da tela"));
            EditorGUILayout.PropertyField(_posicaoVidas, new GUIContent("Posição das vidas"));
            EditorGUILayout.PropertyField(_lifeIcoPosOffset, new GUIContent("Offset da vidas"));
            EditorGUILayout.Space();
            
            EditorGUILayout.LabelField("Referências para Objetos", EditorStyles.boldLabel);
            EditorGUILayout.Space();
            
            EditorGUILayout.PropertyField(_instanciaPlayer, new GUIContent("Player"));
            EditorGUILayout.PropertyField(_instanciaBoss, new GUIContent("Boss"));
            EditorGUILayout.PropertyField(_referenciaCanvas, new GUIContent("Canvas"));
            EditorGUILayout.PropertyField(_imgFrigideira, new GUIContent("Frigideira"));
            EditorGUILayout.PropertyField(_txtFrigideira, new GUIContent("Texto da Frigideira"));
            EditorGUILayout.PropertyField(_imgEspantalho, new GUIContent("Espantalho"));
            EditorGUILayout.PropertyField(_txtEspantalho, new GUIContent("Texto do Espantalho"));
            EditorGUILayout.PropertyField(_txtPontuacao, new GUIContent("Texto da Pontuação"));
            EditorGUILayout.PropertyField(_sldrVida, new GUIContent("Slider de vida"));
            EditorGUILayout.PropertyField(_preenchimentoVida, new GUIContent("Preenchimento de vida"));
            EditorGUILayout.PropertyField(_gradienteVida, new GUIContent("Gradiente de vida"));
        EditorGUILayout.EndVertical();
        
        if(EditorGUI.EndChangeCheck())
            serializedObject.ApplyModifiedProperties();
    }
}
