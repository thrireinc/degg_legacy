using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class bAcoesUI : MonoBehaviour
{
    #region Métodos Personalizados
    
    #region Métodos Públicos
    
    public void ReiniciarFase()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void CarregarFasePeloIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
    }
    public void Sair()
    {
        Application.Quit();
    }

    #endregion
    
    #endregion
}
