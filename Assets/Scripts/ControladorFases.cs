using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorFases : MonoBehaviour
{
    // Start is called before the first frame update
    public static ControladorFases instance;
    public FaseJuego state;
    public Player[] listaJugadores;
    private float cuentaAtras = 8f;
    public int ronda;
    [SerializeField] private EnemigoSupervivencia enemigo;
    public GameObject panelFinal;
    void Start()
    {   
        if(instance == null) { instance = this; }
        else { Destroy(this); }

        state = FaseJuego.Prefase;
        listaJugadores = FindObjectsOfType<Player>();
        ////Comillado para linkedin
        //enemigo = GameObject.Find("Enemigo").GetComponent<EnemigoSupervivencia>();
        //StartCoroutine(CambiarPrefaseATienda()); 
    }

    // Update is called once per frame

    //Esto lo hemos comillado para el video de Linkedin
    /*void Update()
    {
        if(state == FaseJuego.Tienda)
        {
            cuentaAtras -= Time.deltaTime;
            for(int i = 0; i < listaJugadores.Length; i++)
            {
                listaJugadores[i].tiempoRestante.text = cuentaAtras.ToString("F0");
            }
        }
        if(cuentaAtras <= 0 && state == FaseJuego.Tienda)
        {
            StartCoroutine(PreparacionFaseCombate());
        }
        
    }*/

    public enum FaseJuego
    {
        Prefase,
        Tienda,
        Combate,
        Final
    }

    private IEnumerator PreparacionFaseCombate()
    {
        state = FaseJuego.Combate;
        ActivarTiempoRestante(false);
        enemigo.MostrarEquipoCombate();
        yield return new WaitForSeconds(3f);

        // ACTIVAMOS LOS POKES DE TODOS LOS JUGADORES, PREVIO TENEMOS QUE HACER QUE UNOS SE INSTANCIEN EN OTRO LADO Y TAL EN EL MULTIPLAYER
        // AHORA MISMO, SIMPLEMENTE INSTANCIAR LOS DEL ENEMIGO
        foreach(Player player in listaJugadores)
        {
            player.PrepararPokesCombate();
        }

        enemigo.ActivarLosEnemigos();
        
       
        cuentaAtras = 10f;
        
        // LANZAR EVENTO DE PREPARACION DE COMBATE
        // PARA ELLO TENEMOS QUE SUSCRIBIR A LOS JUGADORES
        // Y LUEGO ACCEDER A LOS QUE ESTAN EN EN TABLERO MEDIANTE LA LISTA
      
    }

    private IEnumerator CambiarPrefaseATienda()
    {      

        yield return new WaitForSeconds(0.5f);

        StartCoroutine(PreparacionFaseTienda());
    }

    public IEnumerator PreparacionFaseTienda()
    {
        state = FaseJuego.Tienda;
        yield return new WaitForSeconds(2);
        foreach(Player player in listaJugadores)
        {
            player.PrepararPokesFaseTienda();
            player.ActualizarOroRonda();
            player.CambiarTienda();
        }
        enemigo.DesactivarLosEnemigos();
        yield return new WaitForSeconds(2);
        ronda++;
        if(ronda > 1)
        {
            state = FaseJuego.Final;
            panelFinal.SetActive(true);

        }
        ActivarTiempoRestante(true);       
        

        //LLAMAR AL EVENTO PARA HACER LA LOGICA DE LA FASE DE TIENDAS
        // DESACTIVAR NAV MESH, ACTIVAR LOS POKES MUERTOS, RECUPERAR VIDA ETC.
    }

    private void ActivarTiempoRestante(bool estado)
    {      
        foreach(Player player in listaJugadores)
        {
            player.tiempoRestante.gameObject.SetActive(estado);
        }
    }


    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }
}
