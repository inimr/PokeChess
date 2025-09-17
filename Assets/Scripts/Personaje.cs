using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personaje : MonoBehaviour
{    
    public CharactersDataSO data; // Variable para acceder a los datos del personaje
    public Player jugador; // AHORA MISMO LO ASIGNAMOS AL COMPRAR EN LA TIENDA
    [HideInInspector] public Vector3 posIni;
    [HideInInspector] public Vector3 escalaInicial;
    public int nivelEstrellas = 0; // AL SUBIR LEVEL LO PRIMERO DE TODO ES AUMENTAR ESTE VALOR
    public int numPokeIguales = 0;
    private bool enTablero;       
    public Animator animator;
    private string animFloat = "atSpeed";


    // HEMOS CREADO LAS PROPIEDADES PARA CALCULAR AUTOMATICAMENTE EL CAMBIO DE ESTADISTICAS AQUI
    // TENDREMOS QUE HACER LO MISMO PARA TODOS LOS STATS QUE PUEDAN AUMENTAR MEDIANTE SINERGIAS

    // >>>>>> AHORA MISMO NO TENEMOS UN STAT QUE PODAMOS MULTIPLICAR *1.8 CUANDO SUBA NIVEL ESTRELLAS!!!!!!!!!!! <<<<<<<
    //----------------------------------------------------------------------------//
    //-------------chequear que esto no este cumplido ya por favoh  --------------//
    //----------------------------------------------------------------------------//
    #region CalculosAumentoEstadisticasMedianteSinergias

    /// <summary>
    /// VALORES MAXIMOS QUE TIENE ACTUALMENTE EL PERSONAJE
    /// </summary>

    [HideInInspector] public int vidaMax;
    [HideInInspector] public int atkMax;
    [HideInInspector] public int defMax;
    [HideInInspector] public int spAMax;
    [HideInInspector] public int spDMax;
    [HideInInspector] public float atkSpeedMax;
    [HideInInspector] public int manaMax;

    [HideInInspector] public int manaActual;
    [HideInInspector] public int vidaActual;
    [HideInInspector] public int reduccionDaño = 1; // SERA UNO SI NO HAY REDUCCION, 0.8 SI HAY UN 20% ETC.
    [HideInInspector] public int vidaEscudo;
    [HideInInspector] public int mitigacionTanque;





    private int vidaBaseMax;
    private int atkBaseMax;
    private int defBaseMax;
    private int spDBaseMax;
    private int spABaseMax;
    private float atkSpeedBaseMax;
    private float multiplicadorAnimSpeed; //DE BASE ES 1, AÑADIMOS SINERGIAS Y AT SPEED TAMBIEN A ESTO



    private int _vidaExtraSinergias;
    public int vidaExtraSinergias
    {
        get { return _vidaExtraSinergias; }
        set
        {
            _vidaExtraSinergias = value;
            
            vidaMax = CalcularStatMax(vidaBaseMax, vidaExtraSinergias); // PREGUNTAR SI ESTO FUNCIONA
            vidaActual = vidaMax;
        }
    }

    

    private int CalcularStatMax(int statBase, int statSinergia)
    {
        return statBase + statSinergia;
    }
   
    
    private int _atkExtraSinergias;
    public int atkExtraSinergias
    {
        get {  return _atkExtraSinergias; }
        set
        {
            _atkExtraSinergias= value;
            atkMax = CalcularStatMax(atkBaseMax, atkExtraSinergias);
        }
    }
    
    private int _defExtraSinergias;
    public int defExtraSinergias
    {
        get { return _defExtraSinergias; }
        set
        {
            _defExtraSinergias= value;
            defMax = CalcularStatMax(defBaseMax, defExtraSinergias);
        }
    }
   
    private int _spAExtraSinergias;
    public int spAExtraSinergias
    {
        get { return _spAExtraSinergias; }
        set
        {
            _spAExtraSinergias = value;
            spAMax = CalcularStatMax(spABaseMax, spAExtraSinergias);
        }
    }
  
    private int _spDExtraSinergias;
    public int spDExtraSinergias
    {
        get { return _spDExtraSinergias; }
        set
        {
            _spDExtraSinergias = value;
            spDMax = CalcularStatMax(spDBaseMax, spDExtraSinergias);
        }
    }

  
    private float _atkSpeedSinergias;
    public float atkSpeedSinergias
    {
        get { return  _atkSpeedSinergias; }

        set
        {
            _atkSpeedSinergias= value;
            CalcularAtkSpeed();
            
        }
    }
    public void CalcularAtkSpeed()
    {
        atkSpeedMax = atkSpeedBaseMax * (1f + atkSpeedSinergias);
        multiplicadorAnimSpeed = 1f + atkSpeedSinergias; // SUMAREMOS A SINERGIAS LOS BUFOS EXTRA DE LAS HABILIDADES???        
        animator.SetFloat(animFloat, multiplicadorAnimSpeed);
    }
    #endregion

    private void Awake()
    {
        
        animator = GetComponent<Animator>();
        CogerInfoSO();
        AñadirStatsMax();
        escalaInicial = transform.localScale;
       
    }

    private void CogerInfoSO()
    {
        //ESTO HABRA QUE HACERLO TAMBIEN CUANDO SUBAMOS DE ESTRELLAS EL PERSONAJE, DESPUES DE SUMAR AL nivelEstrellas++
        vidaBaseMax = (int)(data.HP * (1 + (0.8f * nivelEstrellas)));
        atkBaseMax = (int)(data.Ataque * (1 + (0.8f * nivelEstrellas)));
        defBaseMax = (int)(data.Defensa * (1 + (0.8f * nivelEstrellas)));       
        spDBaseMax = (int)(data.DefensaEspecial * (1 + (0.8f * nivelEstrellas)));
        atkSpeedBaseMax = data.VelocidadAtq * (1 + (0.18f * nivelEstrellas)); 
        if (data.usaMana)
        {
            manaMax = data.ManaMax;
            manaActual = data.startingMana;
        }
    }
    private void AñadirStatsMax()
    {
        vidaMax = CalcularStatMax(vidaBaseMax, vidaExtraSinergias);
        atkMax = CalcularStatMax(atkBaseMax, atkExtraSinergias);
        defMax = CalcularStatMax(defBaseMax, defExtraSinergias);
        spAMax = CalcularStatMax(spABaseMax, spAExtraSinergias);
        spDMax = CalcularStatMax(spDBaseMax, spDExtraSinergias);
        CalcularAtkSpeed();
        vidaActual = vidaMax; //// ESTO LO PONEMOS AQUI POR AHORA PERO NO SABEMOS DONDE ESTARA MAS ADELANTE
    } 
   
    public void DetectarEvolucion()
    {
        //TENEMOS QUE GUARDAR LA POSICION DE LAS PIEZAS DONDE ESTAN
        Personaje primerPersonaje = null;
        Personaje segundoPersonaje = null;

        // MIRAMOS SI HAY MAS PIEZAS DEL MISMO TIPO Y DEL MISMO NIVEL TANTO EN EL BANQUILLO COMO EN EL TABLERO
        foreach(Personaje poke in jugador.listaPiezasEnTablero)
        {
           
            if(poke.data.ID == data.ID && poke.nivelEstrellas == nivelEstrellas)
            {
                numPokeIguales++;
                
                if(primerPersonaje == null && poke != this)
                {
                    primerPersonaje = poke;
                    enTablero = true;
                }
                else if(poke!= this) 
                { 
                    segundoPersonaje = poke; enTablero = true; 
                
                }
                
            }
        }
        for(int i = 0; i < jugador.banquillo.Length; i++)
        {
            if (jugador.banquillo[i].childCount > 0)
            {
                Personaje pokeEnBanquillo = jugador.banquillo[i].GetComponentInChildren<Personaje>();                
                if(pokeEnBanquillo.data.ID == data.ID && pokeEnBanquillo.nivelEstrellas == nivelEstrellas)
                {
                    
                    numPokeIguales++;
                  
                    //print("Numeros Iguales sin el this son " + pokeEnBanquillo.numPokeIguales);
                    //print("Numeros Iguales con el this son " + this.numPokeIguales);
                    //print("Numeros iguales a secas son  " + numPokeIguales);
                    // NOS ASEGURAMOS QUE SI ESTAN TODOS EN EL BANQUILLO NO SE COJE A SI MISMO DE NUEVO
                    if (primerPersonaje == null && pokeEnBanquillo != this)
                    {
                        primerPersonaje = pokeEnBanquillo;
                    }
                    else if (pokeEnBanquillo != this)
                    { 
                        segundoPersonaje = pokeEnBanquillo;
                    }
                }
            }
        }
        
        if (numPokeIguales > 2)
        {       //COMPROBAMOS SI ALGUNO DE LOS POKES ESTABA EN EL TABLERO
            if(jugador.listaPiezasEnTablero.Contains(primerPersonaje))
            {
                //AQUI YA TENEMOS QUE INSTANCIAR TODO LO QUE QUERAMOS
                if (nivelEstrellas == 0)
                {
                    LogicaEvolucion(primerPersonaje, primerPersonaje.data.Prefab2Estrellas, primerPersonaje, segundoPersonaje);
                    
                }
                else if(nivelEstrellas == 1)
                {
                    LogicaEvolucion(primerPersonaje, primerPersonaje.data.Prefab3Estrellas, primerPersonaje, segundoPersonaje);                   

                }                
            }
            //SI NO SE CUMPLE LA CONDICION DE ARRIBA, IMPLICA QUE LOS 3 ESTARAN EN EL BANQUILLO
            else
            {
                // PONIENDO primerPersonaje EN VEZ DE this, SIEMPRE SE INSTANCIARA EN EL SPOT MAS A LA IZQUIERDA EN EL BANQUILLO
                if(nivelEstrellas == 0)
                {
                    
                    LogicaEvolucion(primerPersonaje, data.Prefab2Estrellas, primerPersonaje, segundoPersonaje);
                   

                }
                else if(nivelEstrellas == 1)
                {
                    
                    LogicaEvolucion(primerPersonaje, data.Prefab3Estrellas, primerPersonaje, segundoPersonaje);                    

                }
            }
        }
    }
    private void LogicaEvolucion(Personaje personaje, GameObject prefab, Personaje primerPersonaje, Personaje segundoPersonaje)
    {
        Vector3 spawnPos = personaje.transform.position;
        GameObject evolucion = Instantiate(prefab, spawnPos, Quaternion.identity, personaje.transform.parent);
        
        
        Personaje pokeEvolucion = evolucion.GetComponent<Personaje>();
        if (enTablero) { jugador.listaPiezasEnTablero.Add(pokeEvolucion); }
       
        pokeEvolucion.nivelEstrellas = personaje.nivelEstrellas + 1;
        StartCoroutine(EsperarInstanciaEvolucion(pokeEvolucion, primerPersonaje, segundoPersonaje));
    }

    private IEnumerator EsperarInstanciaEvolucion(Personaje pokeEvolucion, Personaje primerPersonaje, Personaje segundoPersonaje)
    {
        if (jugador.listaPiezasEnTablero.Contains(primerPersonaje)) { jugador.listaPiezasEnTablero.Remove(primerPersonaje); }
        if (jugador.listaPiezasEnTablero.Contains(segundoPersonaje)) { jugador.listaPiezasEnTablero.Remove(segundoPersonaje); }


        Destroy(primerPersonaje.gameObject);
        Destroy(segundoPersonaje.gameObject);
        yield return null;
        Personaje pokePersonaje = pokeEvolucion.GetComponent<Personaje>();
        pokePersonaje.jugador= this.jugador;
        pokePersonaje.gameObject.GetComponent<EnseñarInfoPoke>().canvasInfo = pokePersonaje.jugador.panelInfoPoke;
        pokeEvolucion.DetectarEvolucion();
        Destroy(gameObject);
        

    }
  
   
    private void OnMouseDown()
    {
        if(jugador == null)
        {
            return;
        }
        if (TableroJugador.instance.objetoSeleccionado == null)
        {
            TableroJugador.instance.isClicked = true;
            TableroJugador.instance.objetoSeleccionado = this.transform;
            posIni = transform.position;
            TableroJugador.instance.gridShader.SetActive(true);
            TableroJugador.instance.shaderBanquillo.SetActive(true);
            TableroJugador.instance.indicador.SetActive(true);
            int LayerIgnoreRaycast = LayerMask.NameToLayer("Ignore Raycast");
            gameObject.layer = LayerIgnoreRaycast;
            jugador.panelVenta.SetActive(true);
        }
    }
  
    private void OnMouseUp()
    {
        // <<<<<<<HAY QUE CHEQUEAR SI ESTA DENTRO DE UNA DE LAS CASILLAS PORQUE AHORA MISMO GUARDA LA ULTIMA CASILLA EN LA QUE HA PASADO>>>>>>>>
        if (TableroJugador.instance.objetoSeleccionado != null)
        {
            if (TableroJugador.instance.casillaSeleccionada != null)
            {             

                if(TableroJugador.instance.casillaSeleccionada.childCount == 0) //CASILLA VACIA
                {
                    if(jugador.listaPiezasEnTablero.Count >= jugador.nivel) //TABLERO LLENO
                    {

                        //MIRA SI EL TRANSFORM ESTA EN EL ARRAY DEL BANQUILLO, SI NO ESTA, DEVUELVE -1 &&
                        // MIRA SI EL JUGADOR ESTA EN EL TABLERO O NO,
                        // SI SE CUMPLEN AMBAS CONDICIONES
                        // ASUMIMOS QUE LA PIEZA QUEREMOS PONERLA EN EL TABLERO Y EMPEZO EN EL BANQUILLO                       
                       /* if (Array.IndexOf(jugador.banquillo, ControlJuego.instance.casillaSeleccionada) == -1 && Array.IndexOf(jugador.tablero, transform.parent) == -1)                         
                        {                                
                           
                        }  */


                        //AHORA MISMO NO DISTINGUE SI SE MUEVE ENTRE TABLERO, BANQUILLO-TABLERO/TABLERO-BANQUILLO O ENTRE BANQUILLO. 
                        //HABRIA QUE ESPECIFICARLO EN ALGUN MOMENTO SI SE QUIERE OPTIMIZAR EL CODIGO



                        if(TableroJugador.instance.casillaSeleccionada.CompareTag("Tablero") && transform.parent.CompareTag("Banquillo"))
                        {
                            
                            TableroJugador.instance.objetoSeleccionado.position = posIni;
                            Deseleccionar();
                            return;
                        }
                        
                    }
                    
                    MoverPieza();                  
                
                }
                else // LA CASILLA TIENE OTRA PIEZA
                {
                    
                    Transform hijoCasilla = TableroJugador.instance.casillaSeleccionada.GetChild(0);
                    hijoCasilla.parent = transform.parent;
                    hijoCasilla.position = posIni;
                    MoverPieza();                   

                }
            }
                
               // DESELECCIONAR PORQUE NO ES UN OBJETIVO VALIDO PARA PONER UNA PIEZA
            else
            {
                TableroJugador.instance.objetoSeleccionado.position= posIni;
                Deseleccionar();
            }
        }
    }

    private void MoverPieza()
    {
      
        
        Vector3 posCasilla = Vector3.zero;
        posCasilla.y = 0.5f;
        TableroJugador.instance.objetoSeleccionado.parent = TableroJugador.instance.casillaSeleccionada;
        TableroJugador.instance.objetoSeleccionado.localPosition = posCasilla;       
        
        //Checkear que estan dentro del tablero y activar las sinergias, mas adelante tendra
        // que llamar al script de jugadores
        jugador.CheckEnTablero();
        

        

        Deseleccionar();
    }

    public void Deseleccionar()
    {
        int LayerDefault = LayerMask.NameToLayer("Default");
        gameObject.layer = LayerDefault;
        TableroJugador.instance.stop = true;     
        StartCoroutine(EsperarADesactivar());
    }

    
    IEnumerator EsperarADesactivar()
    {
        yield return null;

        TableroJugador.instance.objetoSeleccionado = null;
        TableroJugador.instance.stop = false;

        TableroJugador.instance.casillaSeleccionada = null;
        TableroJugador.instance.gridShader.SetActive(false);
        TableroJugador.instance.shaderBanquillo.SetActive(false);
        TableroJugador.instance.indicador.SetActive(false);
        TableroJugador.instance.isClicked = false;

        jugador.panelVenta.SetActive(false);
    }

   
}
