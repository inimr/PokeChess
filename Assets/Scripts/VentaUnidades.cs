using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VentaUnidades : MonoBehaviour,  IPointerEnterHandler, IPointerExitHandler
{
    private bool estaEncima;
    public Personaje pokeSeleccionado;
    public Player player;
    public void OnPointerEnter(PointerEventData eventData)
    {
        
        estaEncima = true;
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        estaEncima = false;
    }

    private void Update()
    {
        if(Input.GetMouseButtonUp(0) && estaEncima)
        {
            estaEncima = false;
            pokeSeleccionado = TableroJugador.instance.objetoSeleccionado.GetComponent<Personaje>();
            
            Database.instance.AñadirAlPool(pokeSeleccionado.data, pokeSeleccionado.nivelEstrellas);
            pokeSeleccionado.Deseleccionar();
            
            
            if (player.listaPiezasEnTablero.Contains(pokeSeleccionado)) 
            {
                player.listaPiezasEnTablero.Remove(pokeSeleccionado);
            }
            Destroy(pokeSeleccionado.gameObject);

            StartCoroutine(EsperarUnFrame());



        }
    }
    // AHORA MISMO EL GAMEOBJECT pokeSeleccionado SE ESTA DESTRUYENDO ANTES DE QUE TERMINE DE LEER EL CODIGO DESELECCIONAR()
    IEnumerator EsperarUnFrame()
    {
        yield return null;
        player.CheckEnTablero();
        gameObject.SetActive(false);
    }
}
