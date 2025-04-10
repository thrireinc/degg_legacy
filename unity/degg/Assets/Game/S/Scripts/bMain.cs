using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class bMain : MonoBehaviour
{
    #region Variáveis
    
    #region Variáveis Públicas

    public int GsNumeroDeVidas {get => numeroDeVidas; set => numeroDeVidas = value;}
    public float GsDelayFrigideira {get => delayFrigideira; set => delayFrigideira = value;}
    public float GsDelayEspantalho {get => delayEspantalho; set => delayEspantalho = value;}
    public bool GsTempoFrigideira {get => _tempoFrigideira; set => _tempoFrigideira = value;}
    public bool GsTempoEspantalho {get => _tempoEspantalho; set => _tempoEspantalho = value;}
    
    #endregion
    
    #region Variáveis Privadas
    [SerializeField] private int numeroDeVidas;
    [SerializeField] private float forcaMovimento, delayTiro, velocidadeMorte, duracaoFrigideira, delayFrigideira, delayEspantalho;
    
    [SerializeField] private GameObject escudo;
    [SerializeField] private GameObject[] armas;
    
    [SerializeField] private bBala bala;
    [SerializeField] private AudioSource somTiro, somDano;

    /*This variable(s) will not appear in the inspector*/ private float _h, _v;

    private char tipoPowerUp;
    private bool _tempoEspantalho, _tempoFrigideira;
    /*This variable(s) will not appear in the inspector*/ private bool  _estaComFrigideira, _estaComTiroMelhorado;
    /*This variable(s) will not appear in the inspector*/ private bool _podeAtirar, _podeTomarDano, _podeUsarFrigideira, _podeUsarEspantalho;
    /*This variable(s) will not appear in the inspector*/ private bool _encontrouBala, _encontrouArma, _encontrouCamera, _encontrouSomTiro, _encontrouSpriteRenderer, _encontrouSomDano, _encontrouBoss;
    /*This variable(s) will not appear in the inspector*/ private bool _acabouDelayTiro;
    
    /*This variable(s) will not appear in the inspector*/ private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    /*This variable(s) will not appear in the inspector*/ private Camera _cam;

    /*This variable(s) will not appear in the inspector*/ private bGerenciadorDeNivel _level;
    /*This variable(s) will not appear in the inspector*/ private bGerenciadorDeUI _UI;
    private bBoss _boss;
    #endregion
    
    #endregion

    #region Métodos
    
    #region Métodos da Unity
    
    #region Métodos Privados
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
            
        _cam = Camera.main;

        _level = FindObjectOfType<bGerenciadorDeNivel>();
        _UI = FindObjectOfType<bGerenciadorDeUI>();
        _boss = FindObjectOfType<bBoss>();
    }
    private void Start()
    {
        tipoPowerUp = 'n';
        _tempoEspantalho = _tempoFrigideira = _podeTomarDano = _podeAtirar = _acabouDelayTiro = true;
        _estaComFrigideira = _estaComTiroMelhorado =  false;
        _encontrouSpriteRenderer = _spriteRenderer != null;
        _encontrouBala = bala != null;
        _encontrouArma = armas[0] != null;
        _encontrouCamera = _cam != null;
        _encontrouSomTiro = somTiro != null;
        _encontrouSomDano = somDano != null;
        _encontrouBoss = _boss != null;
    }
    private void Update()
    {
        if (numeroDeVidas <= 0) return;

        Inputing();
        
        if (_podeAtirar && _acabouDelayTiro) 
            Atirar();

        if ((_podeUsarFrigideira && !_estaComFrigideira && _tempoFrigideira) || (_podeUsarEspantalho && _tempoEspantalho))
            StartCoroutine(EstaSendoAfetadoPeloPowerUp(tipoPowerUp));

    }
    private void FixedUpdate()
    {
        if (numeroDeVidas <= 0) return;
        
        Mover();
    }

    private void OnTriggerEnter2D(Collider2D collider2d)
    {
        if (((collider2d.CompareTag("Bullet") && collider2d.gameObject.GetComponent<bBala>().GsAtiradoPor == "Enemy")  || collider2d.CompareTag("Bullet") && collider2d.gameObject.GetComponent<bBala>().GsAtiradoPor == "Boss") || collider2d.CompareTag("Enemy"))
            if (_podeTomarDano)
                StartCoroutine(PodeTomarDano());

        if (collider2d.CompareTag("PowerUp"))
            StartCoroutine(TiroDuplo());

        if (collider2d.CompareTag("Boss") && _cam.GetComponent<bCamera>() != null)
        {
            Destroy(collider2d.gameObject);
            
            if (_encontrouBoss)
                StartCoroutine(AtivarGalinha());
        }

        if (!collider2d.CompareTag("Die")) return;
        numeroDeVidas = 0;
        
        if (_encontrouSomDano)
            somDano.Play();
        
        Destroy(gameObject);

    }
    
    #endregion
    
    #endregion

    #region Métodos Personalizados

    #region Métodos Privados
    
    private void Inputing()
    {
        _h = Input.GetAxis("Horizontal");
        _podeAtirar = Input.GetButton("Shoot");

        if (Input.GetKeyDown(KeyCode.O))
        {
            _podeUsarEspantalho = true;
            tipoPowerUp = 'e';
        }
        else
        {
            _podeUsarEspantalho = false;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            _podeUsarFrigideira = true;
            tipoPowerUp = 'f';
        }
        else
        {
            _podeUsarFrigideira = false;
        }
    }
    private void Mover()
    {
        transform.Translate(new Vector3 (_h, 0, 0) * (forcaMovimento * Time.deltaTime * 1));

        if (!_encontrouCamera) return;
        
        if (_cam.WorldToViewportPoint(transform.position).x < 0.15f)
            transform.position = new Vector3 (_cam.ViewportToWorldPoint(new Vector3(0.15f, 0, 0)).x, transform.position.y, transform.position.z);
        if (_cam.WorldToViewportPoint(transform.position).x > 0.85f)
            transform.position = new Vector3 (_cam.ViewportToWorldPoint(new Vector3(0.85f, 0, 0)).x, transform.position.y, transform.position.z);
    }
    private void Atirar()
    {
        if (!_encontrouBala || !_encontrouArma) return;
        bBala laserBulletInstance = null;
        
        _acabouDelayTiro = false;
        
        if (_estaComTiroMelhorado)
        {
            foreach (var arma in armas)
            {
                var position = arma.transform.position;

                laserBulletInstance = Instantiate(bala, position, Quaternion.identity);
                laserBulletInstance.GsSpriteRenderer.color = Color.blue;
                laserBulletInstance.GsDirecao = 1;
                laserBulletInstance.GsIndestrutivel = true;
            }
        }
        else
        {
            laserBulletInstance = Instantiate(bala, armas[0].transform.position, Quaternion.identity);
            laserBulletInstance.transform.position = new Vector3(armas[0].transform.position.x, armas[0].transform.position.y, armas[0].transform.position.z);
            laserBulletInstance.GsDirecao = 1;  
        }
            
        laserBulletInstance.GsAtiradoPor = "Player";

        if (_encontrouSomTiro)
            somTiro.Play();
            
        StartCoroutine(PodeAtirar());
    }

    private IEnumerator PodeAtirar()
    {
        yield return new WaitForSeconds(delayTiro);
        _acabouDelayTiro = true;
        StopCoroutine(PodeAtirar());
    }
    private IEnumerator EstaSendoAfetadoPeloPowerUp(char t)
    {
        switch (t)
        {
            case 'e':
                
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                foreach (var enemie in enemies)
                    Destroy(enemie);

                break;
            
            case 'f':
                _estaComFrigideira = true;
                yield return new WaitForSeconds(duracaoFrigideira);
                _estaComFrigideira = false;
                break;
        }
        
        StartCoroutine(AtivarUsoPowerUp(t));
        StopCoroutine(EstaSendoAfetadoPeloPowerUp(tipoPowerUp));
    }
    private IEnumerator AtivarUsoPowerUp(char t)
    {
        switch (t)
        {
            case 'e':
                _tempoEspantalho = false;
                yield return new WaitForSeconds(delayEspantalho);
                _tempoEspantalho = true;
                break;
            
            case 'f':
                _tempoFrigideira = false;
                yield return new WaitForSeconds(delayFrigideira);
                _tempoFrigideira = true;
                break;
        }
    }
    private IEnumerator PodeTomarDano()
    {
        if (_estaComFrigideira)
        {
            _estaComFrigideira = false;
            StartCoroutine(AtivarUsoPowerUp('f'));
            StopCoroutine(EstaSendoAfetadoPeloPowerUp(tipoPowerUp));
        }
        else
        {
            _podeTomarDano = false;
            numeroDeVidas--;
        
            if (_encontrouSomDano)
                somDano.Play();

            if (numeroDeVidas <= 0)
                Destroy(gameObject);
        
            if (_encontrouSpriteRenderer)
            {
                for (var x = 0; x < 4; x++)
                {
                    _spriteRenderer.color = Color.red;
                    yield return new WaitForSeconds(0.15f);
                    _spriteRenderer.color = Color.white;
                    yield return new WaitForSeconds(0.15f);
                }
            }

            _podeTomarDano = true;
        }
        
        StopCoroutine(PodeTomarDano());
    }
    private IEnumerator AtivarGalinha()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemie in enemies)
            Destroy(enemie);
        
        yield return new WaitForSeconds(2f);
        _cam.GetComponent<bCamera>().GsForcaMovimentoVertical = 0;
        _boss.GsPodeAtirar = true;
        _boss.GsComecar = true;
        StopCoroutine(AtivarGalinha());
    }
    private IEnumerator TiroDuplo()
    {
        _estaComTiroMelhorado = true;
        yield return new WaitForSeconds(10f);
        _estaComTiroMelhorado = false;
        StopCoroutine(TiroDuplo());
    }

    #endregion
    
    #endregion
    
    #endregion
}
