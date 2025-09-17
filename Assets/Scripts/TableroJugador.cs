using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableroJugador : MonoBehaviour
{
    public Transform objetoSeleccionado;
    public Transform casillaSeleccionada;
    public static TableroJugador instance; // VAMOS A TENER QUE TENER UNO POR JUGADOR
    public LayerMask mask;
    public GameObject gridShader;
    public GameObject shaderBanquillo;
    public GameObject indicador;
    public bool isClicked;
    public bool stop;
    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

   
    void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (stop) return;
        if (Physics.Raycast(ray, out hit, 100, mask))
        {
            Debug.DrawLine(ray.origin, hit.point);
            if (objetoSeleccionado != null)
            {
                Vector3 pos = hit.point;
                pos.y = 0.5f;
                objetoSeleccionado.position = pos;
                

            }
        }

    }
}
