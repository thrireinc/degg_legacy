using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class bGerenciadorDeNivel : MonoBehaviour
{
    #region Variáveis
    
    #region Variáveis Públicas

    public int GsNumeroDeInimigosNaTela {get => _numeroDeInimigosNaTela; set => _numeroDeInimigosNaTela = value;}
    public float GsOffsetHorizontal {get => offsetHorizontal; set => offsetHorizontal = value;}
    public float GsOffsetVertical {get => offsetVertical; set => offsetVertical = value;}

    #endregion
    
    #region Variáveis Privadas
    
    [SerializeField] private int numeroMaximoDeInimigosNaTela;
    [SerializeField] private float delaySpawnInimigo, offsetHorizontal, offsetVertical, delaySpawnPowerUp, delaySpawnMisc;

    [SerializeField] private bControladorDeObjetos cO_controller;
    
    /*This variable(s) does not appear in the inspector*/ private int _numeroDeInimigosNaTela;
    private bool _encontrouJogador;
    /*This variable(s) does not appear in the inspector*/ private bool _podeSpawnarInimigo, _podeSpawnarPowerUp, _podeSpanwarMisc, _podeComecarPodeSpawnarPowerUp, _podeComecarPodeSpawnarInimigo, _podeComecarPodeSpawnarMisc;
    /*This variable(s) does not appear in the inspector*/ private Camera _cam;
    private bMain _instanciaPlayer;

    #endregion
    
    #endregion

    #region Métodos
    
    #region Métodos da Unity
    
    #region Métodos Privados
    private void Start()
    {
        _encontrouJogador = _instanciaPlayer != null;
        _cam = Camera.main;
        _podeComecarPodeSpawnarMisc = _podeComecarPodeSpawnarInimigo = _podeComecarPodeSpawnarPowerUp = true;
    }
    private void Update()
    {
        _instanciaPlayer = FindObjectOfType<bMain>();
        _encontrouJogador = _instanciaPlayer != null;
        
        if (!_encontrouJogador) return;

        SpawnMisc();
        SpawnEnemy();
        //SpawnPowerUp();
        AumentarMaximoDeInimigosNaTela();
    }
    
    #endregion
    
    #endregion

    #region Métodos Personalizados

    private void SpawnMisc()
    {
        if (_podeSpanwarMisc && cO_controller.outros.Length != 0)
        {
            _podeSpanwarMisc = false;
            Instantiate(cO_controller.outros[Random.Range(0, cO_controller.outros.Length)], new Vector2(_cam.ViewportToWorldPoint(new Vector2(Random.Range(0.15f, 0.85f), 1)).x, _cam.ViewportToWorldPoint(new Vector2(Random.Range(0.15f, 0.85f), 1)).y + offsetVertical) ,quaternion.identity);
        }
        else if (_podeComecarPodeSpawnarMisc)
        {
            _podeComecarPodeSpawnarMisc = false;
            StartCoroutine(PodeSpawnarMisc());
        }

    }
    private void SpawnEnemy()
    {
        if (_numeroDeInimigosNaTela >= numeroMaximoDeInimigosNaTela) return;

        if (_podeSpawnarInimigo && cO_controller.inimigos.Length != 0)
        {
            _podeSpawnarInimigo = false;
            Instantiate(cO_controller.inimigos[Random.Range(0, cO_controller.inimigos.Length)], new Vector2(_cam.ViewportToWorldPoint(new Vector2(Random.Range(0.15f, 0.85f), 1)).x, _cam.ViewportToWorldPoint(new Vector2(Random.Range(0.15f, 0.85f), 1)).y + offsetVertical), quaternion.identity);
        }
        else if (_podeComecarPodeSpawnarInimigo)
        {
            _podeComecarPodeSpawnarInimigo = false;
            StartCoroutine(PodeSpawnarInimigo());
        }
    }
    
    /*
    private void SpawnPowerUp()
    {
        if (_podeSpawnarPowerUp && !_instanciaPlayer.GsEstaComPowerUp && cO_controller.powerUps.Length != 0)
        {
            _podeSpawnarPowerUp = false;
            Instantiate(cO_controller.powerUps[Random.Range(0, cO_controller.powerUps.Length)], new Vector2(_cam.ViewportToWorldPoint(new Vector2(Random.Range(0.0f, 1.0f), 1)).x, _cam.ViewportToWorldPoint(new Vector2(Random.Range(0.0f, 1.0f), 1)).y), quaternion.identity);
        }
        else if (!_instanciaPlayer.GsEstaComPowerUp && _podeComecarPodeSpawnarPowerUp)
        {
            _podeComecarPodeSpawnarPowerUp = false;
            StartCoroutine(PodeSpawnarPowerUp());
        }
    }
    */

    private IEnumerator PodeSpawnarInimigo()
    {
        yield return new WaitForSeconds(delaySpawnInimigo);
        _numeroDeInimigosNaTela += 1;
        _podeComecarPodeSpawnarInimigo = _podeSpawnarInimigo = true;
        StopCoroutine(PodeSpawnarInimigo());
    }
    private IEnumerator PodeSpawnarPowerUp()
    {
        yield return new WaitForSeconds(delaySpawnPowerUp);
        _podeComecarPodeSpawnarPowerUp = _podeSpawnarPowerUp = true;
        StopCoroutine(PodeSpawnarPowerUp());
    }
    private IEnumerator PodeSpawnarMisc()
    {
        yield return new WaitForSeconds(delaySpawnMisc);
        _podeComecarPodeSpawnarMisc = _podeSpanwarMisc = true;
        StopCoroutine(PodeSpawnarMisc());
    }
    
    private void AumentarMaximoDeInimigosNaTela()
    {
        switch (Time.time / 60)
        {
            case 5:
                numeroMaximoDeInimigosNaTela += 2;
                break;
            case 10:
                numeroMaximoDeInimigosNaTela += 4;
                break;
            case 15:
                numeroMaximoDeInimigosNaTela += 6;
                break;
            case 20:
                numeroMaximoDeInimigosNaTela += 8;
                break;
            case 40:
                numeroMaximoDeInimigosNaTela += 12;
                break;
            case 60:
                numeroMaximoDeInimigosNaTela += 16;
                break;
        }
    }
    
    #endregion
    
    #endregion
}

