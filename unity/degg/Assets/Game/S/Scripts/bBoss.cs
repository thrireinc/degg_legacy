using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class bBoss : MonoBehaviour
{
    public bool GsComecar {get => _comecar; set => _comecar = value;}
    public bool GsPodeAtirar {get => _podeAtirar; set => _podeAtirar = value;}
    public int GsNumeroDeVidasAtual {get => _numeroAtualDeVidas; set => _numeroAtualDeVidas = value;}
    
    [SerializeField] private int numeroDeVidas;
    [SerializeField] private float forcaMovimentoHorizontal, delayTiro;
    [SerializeField] private bBala bala;
    [SerializeField] private GameObject arma, barraDeVida;
    [SerializeField] private Slider sldrVida;

    private int _numeroAtualDeVidas;
    private SpriteRenderer _spriteRenderer;
    private Camera _cam;
    private bool _acabouDelayTiro, _comecar;
    private bool _podeRondar, _podeSeguir, _podeAtirar, _podeTomarDano;
    private bool _encontrouPlayer, _encontrouBala, _encontrouArma, _encontrouUI, _encontrouSpriteRenderer, _encontrouSldrVida;
    private Vector3 dir;
    private bMain _instanciaPlayer;
    private bGerenciadorDeUI _UI;

    private void Awake()
    {
        _cam = Camera.main;
        _instanciaPlayer = FindObjectOfType<bMain>();
        _UI = FindObjectOfType<bGerenciadorDeUI>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        _podeAtirar = _podeSeguir = _podeRondar = _podeSeguir = _podeTomarDano = false;
        _acabouDelayTiro = true;
        
        _encontrouBala = bala != null;
        _encontrouArma = arma != null;
        _encontrouSpriteRenderer = _spriteRenderer != null;
        _encontrouSldrVida = sldrVida != null;

        _numeroAtualDeVidas = numeroDeVidas;
        
        dir = Vector3.right;
    }
    private void Update()
    {
        _encontrouPlayer = _instanciaPlayer != null;
        if (!_encontrouPlayer || !_comecar) return;
        
        barraDeVida.SetActive(true);

        DetectarMovimento();
        
        if (_podeRondar)
            Rondar();
        else if (_podeSeguir)
            Seguir();
        else
            dir = Vector3.zero;

        Mover();
        _podeTomarDano = true;
        
        if (_podeAtirar && _acabouDelayTiro)
            Atirar();
    }

    private void OnTriggerEnter2D(Collider2D collider2d)
    {
        if ((collider2d.CompareTag("Bullet") && collider2d.gameObject.GetComponent<bBala>().GsAtiradoPor == "Player") && _podeTomarDano)
            StartCoroutine(PodeTomarDano());
    }

    private void DetectarMovimento()
    {
        if (_numeroAtualDeVidas > numeroDeVidas / 2)
        {
            _podeRondar = true;
            _podeSeguir = false;
        }
        else
        {
            _podeRondar = false;
            _podeSeguir = true;
        }
    }
    private void Rondar()
    {
        if (_cam.WorldToViewportPoint(transform.position).x <= 0.15f)
            dir = Vector3.right;
        if (_cam.WorldToViewportPoint(transform.position).x >= 0.85f)
            dir = Vector3.left;
    }
    private void Seguir()
    {
        dir = _instanciaPlayer.transform.position.x > transform.position.x ? Vector3.right : _instanciaPlayer.transform.position.x < transform.position.x ? Vector3.left : Vector3.zero;
    }
    private void Mover()
    {
        transform.Translate(dir * (forcaMovimentoHorizontal * Time.deltaTime * 1));
    }
    private void Atirar()
    {
        if (!_encontrouBala || !_encontrouArma) return;
        bBala laserBulletInstance = null;
        
        _acabouDelayTiro = false;
        
        laserBulletInstance = Instantiate(bala, arma.transform.position, Quaternion.identity);
        laserBulletInstance.transform.position = new Vector3(arma.transform.position.x, arma.transform.position.y, arma.transform.position.z);
            
        laserBulletInstance.GsAtiradoPor = "Boss";
        laserBulletInstance.GsDirecao = -1;

        StartCoroutine(PodeAtirar());
    }

    private IEnumerator PodeAtirar()
    {
        yield return new WaitForSeconds(delayTiro);
        _acabouDelayTiro = true;
        StopCoroutine(PodeAtirar());
    }

    private IEnumerator PodeTomarDano()
    {
        StopCoroutine(PodeTomarDano());
        _numeroAtualDeVidas--;
        
        if (_encontrouSldrVida)
            sldrVida.value = _numeroAtualDeVidas;
        
        _UI.GsPontuacaoAtual += 50;

        if (_numeroAtualDeVidas <= 0)
        {
            if (_encontrouUI)
                _UI.GsPontuacaoAtual += 10000;

            Destroy(gameObject);
        }

        if (!_encontrouSpriteRenderer) yield break;
        for (var x = 0; x < 4; x++)
        {
            _spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.15f);
            _spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.15f);
        }
    }
}
