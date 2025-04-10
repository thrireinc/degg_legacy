using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class bLegumes : MonoBehaviour
{
    private bGerenciadorDeUI _UI;
    private bool _encontrouUI;

    private void Awake()
    {
        _UI = FindObjectOfType<bGerenciadorDeUI>();
    }

    private void Start()
    {
        _encontrouUI = _UI != null;
    }

    private void OnTriggerEnter2D(Collider2D collider2d)
    {
        if (!collider2d.CompareTag("Player")) return;
        
        if (_encontrouUI)
            _UI.GsPontuacaoAtual += 50;
            
        Destroy(gameObject);
    }
}
