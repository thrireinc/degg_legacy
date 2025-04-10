using UnityEngine;

public class bCamera : MonoBehaviour
{
    public float GsForcaMovimentoVertical {get => forcaMovimentoVertical; set => forcaMovimentoVertical = value;}
    [SerializeField] private float forcaMovimentoVertical;
    
    private void Update()
    {
        Mover();
    }

    private void Mover()
    {
        transform.Translate(Vector3.up * (forcaMovimentoVertical * Time.deltaTime));
    }
}
