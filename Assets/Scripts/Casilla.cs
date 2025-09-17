using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Casilla : MonoBehaviour
{
    [HideInInspector] public bool isHovering;
    private void OnMouseEnter()
    {
        if(!TableroJugador.instance.isClicked) { return; }
        TableroJugador.instance.indicador.SetActive(true);
        isHovering = true;
    }

    private void OnMouseOver()
    {
        if (!TableroJugador.instance.isClicked) { return; }
        TableroJugador.instance.casillaSeleccionada = this.transform;
        TableroJugador.instance.indicador.transform.position = transform.position + new Vector3(0,0.5f,0); //ESTE ES EL TRANSFORM DONDE SE INSTANCIAN LOS POKES
      

    }

    private void OnMouseExit()
    {
        if (!TableroJugador.instance.isClicked) { return; }
        TableroJugador.instance.indicador.SetActive(false);
        TableroJugador.instance.casillaSeleccionada = null;
        isHovering=false;
    }





}
