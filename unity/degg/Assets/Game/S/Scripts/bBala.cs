using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class bBala : MonoBehaviour
{
    #region Variáveis
    
    #region Variáveis Públicas

    public int GsDirecao {get => direcao; set => direcao = value;}
    public string GsAtiradoPor {get => atiradoPor; set => atiradoPor = value;}
    public bool GsIndestrutivel {get => indestrutivel; set => indestrutivel = value;}
    public SpriteRenderer GsSpriteRenderer {get => _spriteRenderer; set => _spriteRenderer = value;}

    #endregion
    
    #region Variáveis Privadas
    [SerializeField] private float forcaMovimentoVertical;
    
    /* Essa(s) variável(is) não aparecerá(am) no inspector */ private int direcao = -1;
    /* Essa(s) variável(is) não aparecerá(am) no inspector */ private string atiradoPor;
    /* Essa(s) variável(is) não aparecerá(am) no inspector */ private bool indestrutivel;
    /* Essa(s) variável(is) não aparecerá(am) no inspector */ private SpriteRenderer _spriteRenderer;
    
    #endregion
    
    #endregion

    #region Métodos
    
    #region Métodos da Unity

    #region Métodos Privados

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        Mover();
    }
    private void OnBecameInvisible()
    {
        Destruir();
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if ((!indestrutivel) && ((collider.CompareTag("Player") && (atiradoPor == "Enemy" || atiradoPor == "Boss")) || ((collider.CompareTag("Enemy") || collider.CompareTag("Boss")) && atiradoPor == "Player")))
            Destruir();
    }
    
    #endregion
    
    #endregion

    #region Métodos Personalizados

    #region Métodos Privados

    private void Mover()
    {
        transform.Translate(new Vector3(0, direcao, 0) * (forcaMovimentoVertical * Time.deltaTime));
    }

    private void Destruir()
    {
        Destroy(gameObject);
    }

    #endregion

    #endregion
    
    #endregion
}

