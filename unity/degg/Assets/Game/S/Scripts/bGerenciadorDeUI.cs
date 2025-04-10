using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class bGerenciadorDeUI : MonoBehaviour
{
    #region Variáveis
    
    #region Variáveis Públicas

    public int GsPontuacaoAtual {get => _pontuacaoAtual; set => _pontuacaoAtual = value;}
    
    public bool GsPodeDesenharVidas {get => _podeDesenharVidas; set => _podeDesenharVidas = value;}
    public bool GsCarregarGameOver {get => _podeCarregarGameOver; set => _podeCarregarGameOver = value;}

    #endregion
    
    #region Variáveis Privadas

    [SerializeField] private bMain instanciaPlayer;
    [SerializeField] private bBoss instanciaBoss;
    [SerializeField] private Vector2 tamanhoDaTelaDeReferencia, offsetTela, lifeIcoPosOffset;
    [SerializeField] private GameObject referenciaCanvas, posicaoVidas;
    [SerializeField] private Text txtPontuacao, txtFrigideira, txtEspantalho;
    [SerializeField] private Slider sldrVida;
    [SerializeField] private Gradient gradienteSldrVida;
    [SerializeField] private Image preenchimentoVida, imgFrigideira, imgEspantalho;
    
    /* Essa(s) variável(is) não aparecerá(am) no inspector */ private int _pontuacaoEscrita, _pontuacaoAtual, _numeroDeZeros;
    /* Essa(s) variável(is) não aparecerá(am) no inspector */ private bool _estaEscrevendoPontuacao;
    /* Essa(s) variável(is) não aparecerá(am) no inspector */ private bool _encontrouTxtPontuacao, _encontrouTxtFrigideira, _encontrouInstanciaBoss, _encotrouTxtEspantalho, _encontrouPlayer, _encontrouSldrVida, _encontrouPreenchimentoVida, _encontrouFrigideira, _encontrouEspantalho;
    /* Essa(s) variável(is) não aparecerá(am) no inspector */ private bool _podeDesenharVidas, _podeCarregarGameOver, _podeAtivarFrigideira, _podeAtivarEspantalho;

    #endregion

    #endregion

    #region Métodos da Unity

    #region Métodos Privados
    
    private void Start()
    {
        _podeAtivarFrigideira = _podeAtivarEspantalho = _podeDesenharVidas = _podeCarregarGameOver = true;
        _estaEscrevendoPontuacao = false;
        _pontuacaoAtual = _pontuacaoEscrita = 0;
        _numeroDeZeros = 9;

        _encontrouInstanciaBoss = instanciaBoss != null;
        _encontrouSldrVida = sldrVida != null;
        _encontrouTxtPontuacao = txtPontuacao != null;
        _encotrouTxtEspantalho = txtEspantalho != null;
        _encontrouTxtFrigideira = txtFrigideira != null;
        _encontrouEspantalho = imgEspantalho != null;
        _encontrouFrigideira = imgFrigideira != null;
        _encontrouPlayer = instanciaPlayer != null;
        _encontrouPreenchimentoVida = preenchimentoVida != null;

    }
    private void Update() 
    {
        if(_encontrouSldrVida && _podeDesenharVidas)
            DesenharVidas();

        if(_encontrouTxtPontuacao && _pontuacaoAtual - _pontuacaoEscrita != 0 && !_estaEscrevendoPontuacao)
            StartCoroutine(DesenharPontuacao());
        
        if (_encontrouFrigideira && !instanciaPlayer.GsTempoFrigideira && _podeAtivarFrigideira)
            StartCoroutine(AtivarFrigideira());
        
        if (_encontrouEspantalho && !instanciaPlayer.GsTempoEspantalho && _podeAtivarEspantalho)
            StartCoroutine(AtivarEspantalho());
        
        if (_encontrouPlayer && instanciaPlayer.GsNumeroDeVidas == 0 && SceneManager.GetActiveScene().buildIndex != 0 && _podeCarregarGameOver) 
            GameOver();
        
        if (_encontrouInstanciaBoss && instanciaBoss.GsNumeroDeVidasAtual == 0)
            Venceu();
    }
    
    #endregion
    
    #endregion

    #region Métodos Personalizados

    #region Métodos Privados
    
    private void DesenharVidas()
    {
        sldrVida.value = instanciaPlayer.GsNumeroDeVidas;
        
        if (_encontrouPreenchimentoVida)
            preenchimentoVida.color = gradienteSldrVida.Evaluate(sldrVida.value / sldrVida.maxValue);
    }
    private IEnumerator DesenharPontuacao()
    {
        _estaEscrevendoPontuacao = true;
        _pontuacaoEscrita++;

        txtPontuacao.text = " ";
        
        for (var x = 0; x < _numeroDeZeros - _pontuacaoEscrita.ToString().Length; x++)
            txtPontuacao.text += "0";
        
        txtPontuacao.text += _pontuacaoEscrita.ToString();
        yield return new WaitForSeconds(0.005f);
        _estaEscrevendoPontuacao = false;
        StopCoroutine(DesenharPontuacao());
    }
    private IEnumerator AtivarFrigideira()
    {
        _podeAtivarFrigideira = false;
        imgFrigideira.color = Color.black;

        if (_encontrouTxtFrigideira)
        {
            txtFrigideira.enabled = true;
            
            for (var x = 0; x <= instanciaPlayer.GsDelayFrigideira; x++)
            {
                txtFrigideira.text = (instanciaPlayer.GsDelayFrigideira - x).ToString();
                yield return new WaitForSeconds(1f);
            }

            txtFrigideira.enabled = false;
        }

        imgFrigideira.color = Color.white;
        _podeAtivarFrigideira = true;
        StopCoroutine(AtivarFrigideira());
    }
    private IEnumerator AtivarEspantalho()
    {
        _podeAtivarEspantalho = false;
        imgEspantalho.color = Color.black;

        if (_encotrouTxtEspantalho)
        {
            txtEspantalho.enabled = true;

            for (var x = 0; x <= instanciaPlayer.GsDelayEspantalho; x++)
            {
                txtEspantalho.text = (instanciaPlayer.GsDelayEspantalho - x).ToString();
                yield return new WaitForSeconds(1f);
            }
            
            txtEspantalho.enabled = false;
        }

        imgEspantalho.color = Color.white;
        _podeAtivarEspantalho = true;
        StopCoroutine(AtivarEspantalho());
    }
    
    private void GameOver()
    {
        SceneManager.LoadScene("GameOver", LoadSceneMode.Additive);
        _podeCarregarGameOver = false;
    }

    private void Venceu()
    {
        SceneManager.LoadScene("Venceu", LoadSceneMode.Single);
    }

    #endregion
    
    #endregion
}
