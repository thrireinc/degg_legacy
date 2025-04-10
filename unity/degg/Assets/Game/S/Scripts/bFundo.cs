using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bFundo : MonoBehaviour
{
    [SerializeField] private float tamanhoFundo;
    private Camera _cam;
    private bool _encontrouCam;
    
    private void Awake()
    {
        _cam = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
        _encontrouCam = _cam != null;
    }

    // Update is called once per frame
    void Update()
    {
        if (_encontrouCam)
            Mover();
    }

    private void Mover()
    {
        if (_cam.transform.position.y >= transform.position.y + tamanhoFundo * 2) transform.position = new Vector3(transform.position.x, transform.position.y + tamanhoFundo * 3, transform.position.z);
    }
}
