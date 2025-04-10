using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class bInimigo : MonoBehaviour
{
    #region Variáveis
    
    #region Variáveis Privadas

    [SerializeField] private int numeroDeVidas;
    [SerializeField] private float velocidadeMorte, forcaMovimento, delayTiro, campoDeVisao;
    [SerializeField] private AudioSource somTiro;
    [SerializeField] private bBala bala;
    [SerializeField] private GameObject olho;
    [SerializeField] private GameObject[] armas;
    
    /*This variable(s) does not appear in the inspector*/ private bool _podeAtirar;
    /*This variable(s) does not appear in the inspector*/ private bool _encontrouAnimator, _encontrouSomTiro, _encontrouUI, _encontrouArma;
    private bool _estaVivo;
    
    /*This variable(s) does not appear in the inspector*/ private Animator _animator;
    /*This variable(s) does not appear in the inspector*/ private bGerenciadorDeNivel _level;
    /*This variable(s) will not appear in the inspector*/ private bGerenciadorDeUI _UI;
    /*This variable(s) does not appear in the inspector*/ private bMain _instanciaPlayer;
    
    #endregion
    
    #endregion

    #region Métodos
    
    #region Métodos da Unity
    
    #region Métodos Privados
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _instanciaPlayer = FindObjectOfType<bMain>();
        _level = FindObjectOfType<bGerenciadorDeNivel>();
        _UI = FindObjectOfType<bGerenciadorDeUI>();
    }
    private void Start()
    {
        _estaVivo = _podeAtirar = true;
        
        _encontrouAnimator = _animator != null;
        _encontrouArma = armas.Length != 0 && armas[0] != null;
        _encontrouSomTiro = somTiro != null;
        _encontrouUI = _UI != null;
    }
    private void Update()
    {
        if (_encontrouArma)
            Atirar();
    }
    private void FixedUpdate()
    {
        Mover();
    }

    private void OnBecameInvisible()
    {
        if (PlayerPrefs.GetInt("Alive") == 1)
            transform.position = new Vector3((Random.Range(-Screen.width + _level.GsOffsetHorizontal, Screen.width - _level.GsOffsetHorizontal)) / 120, (Screen.height + _level.GsOffsetVertical) / 120, transform.position.z);
        else
        {
            _level.GsNumeroDeInimigosNaTela -= 1;
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collider2d)
    {
        if ((_estaVivo) && (collider2d.CompareTag("Bullet") && collider2d.gameObject.GetComponent<bBala>().GsAtiradoPor == "Player") || (collider2d.CompareTag("Player")))
            TomarDano();
    }

    #endregion
    
    #endregion

    #region Métodos Personalizados
    
    #region Métodos Privados
    
    private void Mover()
    {
        transform.Translate(Vector3.down * (forcaMovimento * Time.deltaTime));
    }
    private void Atirar()
    {
        var hit = Physics2D.Linecast(olho.transform.position, olho.transform.position + Vector3.down * campoDeVisao, 1 << LayerMask.NameToLayer("Characters"));

        if (hit.collider == null || !_podeAtirar) return;
        if (!hit.collider.gameObject.CompareTag("Player")) return;

        _podeAtirar = false;

        foreach (var gun in armas)
        {
            var _laserBulletInstance = Instantiate(bala, gun.transform.position, Quaternion.identity);
            _laserBulletInstance.GetComponent<bBala>().GsAtiradoPor = "Enemy";
            _laserBulletInstance.GsDirecao = -1;
        }

        if (_encontrouSomTiro)
            somTiro.Play();

        StartCoroutine(PodeAtirar());
    }

    private IEnumerator PodeAtirar()
    {
        yield return new WaitForSeconds(delayTiro);
        _podeAtirar = true;
        StopCoroutine(PodeAtirar());
    }
    private void TomarDano()
    {
        numeroDeVidas--;
        
        if (numeroDeVidas != 0 && _encontrouAnimator)
            _animator.SetTrigger(numeroDeVidas.ToString());
        else
        {
            _level.GsNumeroDeInimigosNaTela -= 1;
            _estaVivo = false;

            if (_encontrouUI)
                _UI.GsPontuacaoAtual += 150;

            Destroy(gameObject);
        }
    }
   
    #endregion
    
    #endregion
    
    #endregion
}
