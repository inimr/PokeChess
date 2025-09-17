using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : Jugadores
{
    // MAS ADELANTE USAREMOS ESTO
    private int vida = 100;
    //private int experiencia;
    private int oroTotal;
    public int nivel;


    [Space]
    [Header ("Cosas del tablero")]
    public Transform[] banquillo;
    public Transform[] tablero;
    [Space]
    public List<CharactersDataSO> personajesTienda = new();
    [Space]
    [Header ("Cosas de la UI")]
    public List<Button> listaButtons;
    public GameObject[] listaUITipoActivadas;
    public GameObject[] listaUITiposDesactivadas;
    public GameObject[] listaUIRolesActivadas;
    public GameObject[] listaUIRolesDesactivadas;
    public GameObject panelInfoPoke;
    public GameObject panelTienda;
    public GameObject panelVenta;
    public TextMeshProUGUI tiempoRestante;

    [Space]
    public List<Personaje> listaPokesEnemigo;
    public EnemigoSupervivencia enemigo;
    
    private void Start()
    {
        oroTotal = 50;
        CrearTienda(); // Esto habra que moverlo a otro sitio mas adelante
        arrayClases = new int[System.Enum.GetValues(typeof(CharactersDataSO._Rol)).Length];
        arrayTipos = new int[System.Enum.GetValues(typeof(CharactersDataSO._Tipo)).Length];
        Debug.LogError("Quitar todas las Ñ de las variables");
    }


    /// <summary>
    /// CREAR TIENDA, SWITCHPOOL, SELECCIONARPERSONAJE POOL E INSTANCIAR FICHA EN BANQUILLO DEBERIAN ESTAR EN OTRO SCRIPT Y LLAMARLAS DESDE AQUI SI ESO
    /// </summary>

    public void CrearTienda()
    {

        for(int i = 0; i < listaButtons.Count; i++)
        {
            SwitchPool(i);
        }
       
    }
    private void SwitchPool(int i) 
    {
        switch (nivel)
        {
            case 1:

                SeleccionarPersonajePool(Database.instance.poolPersonajesCoste1.Count, Database.instance.poolPersonajesCoste1, i);

                break;

            case 2:
                SeleccionarPersonajePool(Database.instance.poolPersonajesCoste1.Count, Database.instance.poolPersonajesCoste1, i);

                break;

            case 3:
                int selectorCosteTres = Random.Range(0, 100);
                if (selectorCosteTres < 65)
                {
                    SeleccionarPersonajePool(Database.instance.poolPersonajesCoste1.Count, Database.instance.poolPersonajesCoste1, i);

                }
                else
                {
                    SeleccionarPersonajePool(Database.instance.poolPersonajesCoste2.Count, Database.instance.poolPersonajesCoste2, i);
                }
                break;

            case 4:
                int selectorCosteCuatro = Random.Range(0, 100);

                if (selectorCosteCuatro < 55)
                {
                    SeleccionarPersonajePool(Database.instance.poolPersonajesCoste1.Count, Database.instance.poolPersonajesCoste1, i);
                }
                else if (selectorCosteCuatro > 54 && selectorCosteCuatro < 85)
                {
                    SeleccionarPersonajePool(Database.instance.poolPersonajesCoste2.Count, Database.instance.poolPersonajesCoste2, i);
                }
                else
                {
                    SeleccionarPersonajePool(Database.instance.poolPersonajesCoste3.Count, Database.instance.poolPersonajesCoste3, i);
                }

                break;
            case 5:
                int selectorCosteCinco = Random.Range(0, 100);
                if (selectorCosteCinco < 45)
                {
                    SeleccionarPersonajePool(Database.instance.poolPersonajesCoste2.Count, Database.instance.poolPersonajesCoste2, i);
                }
                else if (selectorCosteCinco > 44 && selectorCosteCinco < 77)
                {
                    SeleccionarPersonajePool(Database.instance.poolPersonajesCoste2.Count, Database.instance.poolPersonajesCoste2, i);
                }
                else if (selectorCosteCinco > 76 && selectorCosteCinco < 98)
                {
                    SeleccionarPersonajePool(Database.instance.poolPersonajesCoste3.Count, Database.instance.poolPersonajesCoste3, i);
                }
                else
                {
                    SeleccionarPersonajePool(Database.instance.poolPersonajesCoste4.Count, Database.instance.poolPersonajesCoste4, i);
                }
                break;
            case 6:
                int selectorCosteSeis = Random.Range(0, 100);
                if (selectorCosteSeis < 31)
                {
                    SeleccionarPersonajePool(Database.instance.poolPersonajesCoste1.Count, Database.instance.poolPersonajesCoste1, i);
                }
                else if (selectorCosteSeis > 30 && selectorCosteSeis < 71)
                {
                    SeleccionarPersonajePool(Database.instance.poolPersonajesCoste2.Count, Database.instance.poolPersonajesCoste2, i);
                }
                else if (selectorCosteSeis > 70 && selectorCosteSeis < 96)
                {
                    SeleccionarPersonajePool(Database.instance.poolPersonajesCoste3.Count, Database.instance.poolPersonajesCoste3, i);
                }
                else
                {
                    SeleccionarPersonajePool(Database.instance.poolPersonajesCoste4.Count, Database.instance.poolPersonajesCoste4, i);

                }
                break;
                // FALTAN LOS NIVELES 7-9 O 10 VAMOS, QUE ME DABA PEREZA


        }

    }

    // AQUI O EN ALGUN OTRO LADO TENEMOS QUE QUITAR EL OBJETO DE LA LISTA QUE LE HA TOCADO, Y VOLVER A AÑADIRLO CUANDO PULSEMOS
    // BOTON REROLL O ACABE LA RONDA DE COMPRAR Y EMPIECE UNA NUEVA
    private void SeleccionarPersonajePool(int numLimite, List<CharactersDataSO> data, int numFor)
    {
        int numPieza = Random.Range(0, numLimite);
        if (numLimite == 0)
        {
            SwitchPool(numFor);
            return;
        }
        
        personajesTienda.Add(data[numPieza]);
        data.Remove(data[numPieza]); 
        listaButtons[numFor].interactable = true;
        listaButtons[numFor].GetComponent<Image>().sprite = personajesTienda[numFor].spriteTienda;
    }
    public void InstanciarFichaEnBanquillo(int ID)
    {
        for(int i = 0; i < banquillo.Length; i++) 
        {
            if (banquillo[i].childCount == 0)
            {
                
                GameObject instanciaPersonaje = Instantiate(personajesTienda[ID].Prefab1Estrella, banquillo[i]);                   
                instanciaPersonaje.transform.SetParent(banquillo[i]);                
                StartCoroutine(EsperarInstanciaPoke(instanciaPersonaje));
                listaButtons[ID].GetComponent<Image>().sprite = null;
                listaButtons[ID].interactable = false;
            
                return;
            }
        }          
    }

    IEnumerator EsperarInstanciaPoke(GameObject instancia)
    {       
        yield return null;
        instancia.GetComponent<EnseñarInfoPoke>().canvasInfo = panelInfoPoke;
        Personaje scriptPoke = instancia.GetComponent<Personaje>();
        scriptPoke.jugador = this;
        
        scriptPoke.DetectarEvolucion();
        
    }
    public void RerolTienda()
    {
        //QUITAR EL ORO
        if (oroTotal < 2) return;
        oroTotal -= 2;
        CambiarTienda();
    }

    public void CambiarTienda() //LO HEMOS SEPARADO PARA QUE AL CAMBIAR DE FASE PODAMOS HACERLO SIN PERDER DINERO
    {
        //AÑADIR A LA POOL LOS POKES DE LOS BOTONES QUE NO SE HAN CLICKADO
        for (int i = 0; i < listaButtons.Count; i++)
        {
            if (listaButtons[i].interactable)
            {
                Database.instance.AñadirAlPool(personajesTienda[i], 0);
            }
        }

        personajesTienda.Clear();
        CrearTienda();
    }
    
    public void PrepararPokesCombate()
    {
        listaPokesEnemigo.AddRange(enemigo.listaPiezasEnTablero);

        foreach (Personaje poke in listaPiezasEnTablero)
        {
            ControladorPoke controlPoke = poke.gameObject.GetComponent<ControladorPoke>();
            controlPoke.agent.enabled = true;
            controlPoke.enCombate = true;
            controlPoke.animator.SetBool("enCombate", true);
            controlPoke.EncontrarTargetMasCercano();
        }
    }

    public void DetectarPokesActivados()
    {
        for(int i = 0; i < listaPokesEnemigo.Count; i++)
        {
            if (listaPokesEnemigo[i].gameObject.activeSelf) { break;}
            if(i == listaPokesEnemigo.Count - 1)
            {
                //ACTIVAR LA LOGICA DE CAMBIAR DE FASE
                // PASAR TODOS LOS POKES A MODO NO COMBATE
                // PASAR A FALSE SU BOOLEANA EN COMBATE
                //HEMOS GANADO NOSOTROS, NO HACEMOS DAÑO A NADIE, DIFERENTE EN EL MULTI
                StartCoroutine(ControladorFases.instance.PreparacionFaseTienda());
                
            }
        }
    }

    public Transform DetectarEnemigoMasCercano(Transform miPokeTransform, Transform target)
    {
        
        foreach(Personaje pokeEnemigo in listaPokesEnemigo) 
        {
            print(pokeEnemigo.tag + "tag del target");
            print("Si esta activo en si mismo el target" + pokeEnemigo.gameObject.activeSelf);
            if (!pokeEnemigo.gameObject.activeSelf || pokeEnemigo.CompareTag("Muerto"))
            {
                
                continue;
            }
           
            // CALCULAMOS AL ENEMIGO MAS CERCANO
           float  distanciaEnemigo = Vector3.Distance(miPokeTransform.position, pokeEnemigo.transform.position);

            // MIRAMOS SI HAY UN TARGET O NO, SI NO LO HAY ASIGNAMOS, SINO, COMPARAMOS
            if (target != null)
            {
                if (distanciaEnemigo < Vector3.Distance(miPokeTransform.position, target.position)) { target = pokeEnemigo.transform; }

            }
            else
            {
                target = pokeEnemigo.transform;
            }
        }
        return target;
    }

    public void PrepararPokesFaseTienda()
    {
        if (listaPiezasEnTablero.Count == 0) return;
        foreach(Personaje poke in listaPiezasEnTablero)
        {
            poke.transform.localScale = poke.escalaInicial;
            poke.transform.position = new Vector3(poke.transform.parent.position.x, poke.transform.parent.position.y + 0.5f, poke.transform.parent.position.z);
            ControladorPoke controlador = poke.gameObject.GetComponent<ControladorPoke>();
            controlador.enCombate = false;
            controlador.agent.enabled = false;
            poke.gameObject.SetActive(true);
            
        }
    }

    public void ActualizarOroRonda()
    {
        int intereses = oroTotal / 10;
        if (intereses > 5) intereses = 5;
       
        oroTotal += 5 + intereses;
    }

    public void QuitarVida(int enemigos)
    {
        vida -= ControladorFases.instance.ronda + (ControladorFases.instance.ronda + enemigos);
        
        if(vida < 0)
        {
            ControladorFases.instance.state = ControladorFases.FaseJuego.Final;

            Debug.LogWarning("HasPerdido");

            //---------------LOGICA DERROTA-----------------------//
        }
    }
    public void CheckEnTablero()
    {
        
        //PARA QUE SOLO ME COJA EL ULTIMO Y NO VAYAN SUBIENDO EXPONENCIALMENTE, 
         for(int i = 0; i < arrayClases.Length; i++) 
         {
             arrayClases[i] = 0;
         }

         for (int y = 0; y < arrayTipos.Length; y++)
         {
             arrayTipos[y] = 0;
         }

        
        IDPiezasEnTablero.Clear();
        listaPiezasEnTablero.Clear();   
        
        foreach(Transform box in tablero)
        {          
            if(box.childCount > 0)
            {
                CharactersDataSO characterData = box.GetComponentInChildren<Personaje>().data;
                listaPiezasEnTablero.Add(box.GetChild(0).GetComponent<Personaje>()); 
                if (!IDPiezasEnTablero.Contains(characterData.ID))
                {
                    IDPiezasEnTablero.Add(characterData.ID);
                    CharactersDataSO._Tipo _tipo = characterData.Tipo;
                    arrayTipos[(int)_tipo]++;

                    CharactersDataSO._Rol _rol = characterData.Rol;
                    arrayClases[(int)_rol]++;
                }  
            }
        }
        ///// AHORA MISMO SI DOS SINERGIAS SUMAN STATS, NO LO HARIAN REALMENTE, HAY QUE PENSAR UNA MANERA
        ///QUIZA ESTO FUNCIONE
        foreach (Personaje poke in listaPiezasEnTablero)
        {
            poke.vidaExtraSinergias = 0;
            poke.atkExtraSinergias = 0;
            poke.defExtraSinergias = 0;
            poke.spAExtraSinergias = 0;
            poke.spDExtraSinergias = 0;
            poke.atkSpeedSinergias = 0;
        }

        // PARA QUE LA PIEZA QUE ESTABA EN EL TABLERO Y HA IDO AL BANQUILLO PIERDA LOS STATS
        foreach (Transform box in banquillo)
        {
            if (box.childCount == 1)
            {
                Personaje pokeEnBanquillo = box.GetComponentInChildren<Personaje>();
                pokeEnBanquillo.vidaExtraSinergias = 0;
                pokeEnBanquillo.atkExtraSinergias = 0;
                pokeEnBanquillo.defExtraSinergias = 0;
                pokeEnBanquillo.spAExtraSinergias = 0;
                pokeEnBanquillo.spDExtraSinergias = 0;
                pokeEnBanquillo.atkExtraSinergias = 0;
            }
        }

        //Esto habra que quitarlo si lo conseguimos hacer con la clase padre, LO LOGRAMOS (CREO) POGGERS
        ActualizarSinergias();
    }


    private void ActualizarSinergias()
    {
    

        // HEMOS HECHO ASI LAS COSAS DE LA UI PARA PODER AÑADIR MAS SINERGIAS SIN TENER QUE MODIFICAR CODIGO MANUALMENTE
        for (int i = 0; i < arrayTipos.Length; i++)
        {
            GameObject UIDesactivada = listaUITiposDesactivadas[i];
            TextMeshProUGUI numPiezasDesactivada = listaUITiposDesactivadas[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>(); ///NUM SINERGIA ACTUAL

            GameObject UIActivada = listaUITipoActivadas[i];
            TextMeshProUGUI numPiezasActivada = listaUITipoActivadas[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>(); /// NUM SINERGIA ACTUAL
            switch (i)
            {
                case 0: ///////////// LOGICA DEL TIPO ACERO //////////                     
                    switch (arrayTipos[i])
                    {
                        case < 2:
                            foreach (Personaje poke in listaPiezasEnTablero) 
                            {                                                                
                                 poke.defExtraSinergias += 0;                                 
                            
                            }
                            if (arrayTipos[i] > 0)
                            {
                                UIDesactivada.SetActive(true);
                                numPiezasDesactivada.text = arrayTipos[i].ToString();                              
                                
                            }
                            else
                            {
                                UIDesactivada.SetActive(false);

                            }
                            UIActivada.SetActive(false);
                            break;
                        case >= 2:

                            UIActivada.SetActive(true);
                            numPiezasActivada.text = arrayTipos[i].ToString();
                            UIDesactivada .SetActive(false);
                            if(arrayTipos[i] >= 4) 
                            {
                                foreach (Personaje poke in listaPiezasEnTablero)
                                {
                                    poke.defExtraSinergias += 100;
                                }

                            }
                            else
                            {                                
                                foreach (Personaje poke in listaPiezasEnTablero)
                                {                                                                     
                                     poke.defExtraSinergias += 40;
                                    
                                }
                            }
                           

                      break;
                    }
                    break;//// LOGICA TIPO ACERO
                case 1: ///////////// LOGICA DEL TIPO HIELO //////////    
                    switch (arrayTipos[i])
                    {
                        case < 2:
                            //Desactivar las booleanas que hagan que se pueda freezear
                            UIActivada.SetActive(false);
                            if (arrayTipos[i] > 0)
                            {
                                UIDesactivada.SetActive(true);
                                numPiezasDesactivada.text = arrayTipos[i].ToString();
                            }
                            else { UIDesactivada.SetActive(false); }


                            break;
                        case >=2:
                            if (arrayTipos[i] >= 4)
                            {
                                //Activar solamente la sinergia 4
                                
                            }
                            else
                            {
                                //Activar solamente el de sinergia 2
                            }
                            UIActivada.SetActive(true);
                            numPiezasActivada.text = arrayTipos[i].ToString();
                            UIDesactivada.SetActive(false);

                        break;
                    }
                    break;//// LOGICA TIPO HIELO
                case 2: ///////////// LOGICA DEL TIPO FANTASMA ////////// 
                    switch(arrayTipos[i]) 
                    {
                        case < 2:
                            //Desactivar la booleana de la sinergia fantasma
                            if (arrayTipos[i] > 0)
                            {
                                UIDesactivada.SetActive(true);
                                numPiezasDesactivada.text = arrayTipos[i].ToString();
                            }
                            else
                            {
                                UIDesactivada .SetActive(false);
                            }
                            UIActivada.SetActive(false);
                            break;
                        case >=2:
                            //Activar la booleana de la sinergia fantasma
                            UIActivada.SetActive(true);
                            numPiezasActivada.text = arrayTipos[i].ToString();
                            UIDesactivada.SetActive (false);
                            break;
                    }
                    break;//// LOGICA TIPO FANTASMA
                case 3: ///////////// LOGICA DEL TIPO ELECTRICO ////////// 
                    switch (arrayTipos[i])
                    {
                        case < 2:
                            //Desactivar las booleanas de las sinergias de electrico
                            if (arrayTipos[i] > 0)
                            {
                                UIDesactivada.SetActive(true);
                                numPiezasDesactivada.text = arrayTipos[i].ToString();
                            }
                            else
                            {
                                UIDesactivada.SetActive(false);
                            }
                            UIActivada.SetActive(false);
                            break;
                        case >=2:
                            if (arrayTipos[i] >= 4)
                            {
                                //Activar la booleana de 4, desactivar el resto
                                
                            }
                            else
                            {
                                //Activar la de 2
                            }
                            UIActivada.SetActive(true);
                            numPiezasActivada.text = arrayTipos[i].ToString();
                            UIDesactivada.SetActive(false);

                            break;
                    }
                    break;//// LOGICA TIPO ELECTRICO
                case 4: ///////////// LOGICA DEL VOLADOR ////////// 
                    switch (arrayTipos[i])
                    {
                        case < 2:
                            foreach (Personaje poke in listaPiezasEnTablero)
                            {
                                poke.atkSpeedSinergias += 0;

                            }
                            if (arrayTipos[i] > 0)
                            {
                                UIDesactivada.SetActive(true);
                                numPiezasDesactivada.text = arrayTipos[i].ToString();
                            }
                            else
                            {
                                UIDesactivada.SetActive(false);
                            }
                            UIActivada.SetActive(false);
                            break;
                        case >= 2:
                            if (arrayTipos[i] >= 6)
                            {
                                foreach(Personaje poke in listaPiezasEnTablero)
                                {
                                    if(poke.data.Tipo == CharactersDataSO._Tipo.Volador)
                                    {
                                        poke.atkSpeedSinergias += 0.6f;
                                    }
                                    else
                                    {
                                        poke.atkSpeedSinergias += 0.3f;
                                    }
                                }                                
                            }
                            else if (arrayTipos[i] >=4 && arrayTipos[i] < 6)
                            {
                                foreach (Personaje poke in listaPiezasEnTablero)
                                {
                                    if(poke.data.Tipo == CharactersDataSO._Tipo.Volador)
                                    {
                                        poke.atkSpeedSinergias += 0.4f;
                                    }
                                    else
                                    {
                                        poke.atkSpeedSinergias += 0.2f;
                                    }
                                }                                
                            }
                            else
                            {
                                foreach(Personaje poke in listaPiezasEnTablero)
                                {
                                    if (poke.data.Tipo == CharactersDataSO._Tipo.Volador)
                                    {
                                        poke.atkSpeedSinergias += 0.2f;
                                    }
                                    else
                                    {
                                        poke.atkSpeedSinergias += 0f;
                                    }

                                }
                            }

                            UIActivada.SetActive(true);
                            numPiezasActivada.text = arrayTipos[i].ToString();
                            UIDesactivada.SetActive(false);
                            break;                       

                    }
                    break;//// LOGICA TIPO VOLADOR
                case 5: ///////// LOGICA DEL TIPO FUEGO /////////////////
                    switch(arrayTipos[i]) 
                    {
                        case < 2:
                            //Desactivar todas las booleanas de las sinergias
                            if (arrayTipos[i] > 0)
                            {
                                UIDesactivada.SetActive(true);
                                numPiezasDesactivada.text = arrayTipos[i].ToString();
                            }
                            else
                            {
                                UIDesactivada.SetActive(false);
                            }
                            UIActivada.SetActive(false);
                            break;
                        case >= 2:
                            if (arrayTipos[i] >= 6)
                            {
                                //Activar la sinergia de fuego 3
                                //Desactivar el resto
                                
                            }
                            else if (arrayTipos[i] >= 4 && arrayTipos[i]< 6)
                            {
                                //Activar la segunda sinergia, desactivar el resto
                                
                            }
                            else
                            {
                                //Activar la primera sinergia
                            }
                            UIActivada.SetActive(true);
                            numPiezasActivada.text = arrayTipos[i].ToString();
                            UIDesactivada.SetActive(false);

                            break;
                    }
                    break;//// LOGICA TIPO FUEGO
                case 6: ////////////////LOGICA DEL TIPO DRAGON ///////////////
                    switch(arrayTipos[i])
                    {
                        case 0:
                            //Desactivar los bufos que ha podido dar la sinergia
                            foreach (Personaje poke in listaPiezasEnTablero)
                            {
                                if (poke.data.Tipo == CharactersDataSO._Tipo.Dragon)
                                {
                                    poke.atkExtraSinergias += 0;
                                    poke.spAExtraSinergias += 0;
                                    poke.vidaExtraSinergias += 0;
                                }
                            }
                            UIDesactivada.SetActive(false);
                            UIActivada.SetActive(false);
                            break;
                        case 1:
                            foreach(Personaje poke in listaPiezasEnTablero)
                            {
                                if(poke.data.Tipo == CharactersDataSO._Tipo.Dragon)
                                {
                                    poke.atkExtraSinergias += 50;
                                    poke.spAExtraSinergias += 50;
                                    poke.vidaExtraSinergias += 200;                                    
                                }
                            }
                            UIActivada .SetActive(true);
                            numPiezasActivada.text = arrayTipos[i].ToString();

                            UIDesactivada .SetActive(false);
                            break;
                        case > 1:
                            if (IDPiezasEnTablero.Contains(30)) // EL ID DE PALKIA
                            {
                                foreach (Personaje poke in listaPiezasEnTablero)
                                {
                                    if(poke.data.Tipo == CharactersDataSO._Tipo.Dragon)
                                    {
                                        poke.atkExtraSinergias += 50;
                                        poke.spAExtraSinergias += 50;
                                        poke.vidaExtraSinergias += 200;
                                    }
                                }

                                UIActivada .SetActive(true);
                                numPiezasActivada.text = arrayTipos[i] .ToString();
                                UIDesactivada.SetActive(false);

                            }
                            else
                            {
                                UIActivada.SetActive(false);
                                UIDesactivada.SetActive (true);
                                numPiezasDesactivada.text = arrayTipos[i].ToString();

                                
                                foreach (Personaje poke in listaPiezasEnTablero)
                                {
                                    if (poke.data.Tipo == CharactersDataSO._Tipo.Dragon)
                                    {
                                        poke.atkExtraSinergias += 0;
                                        poke.spAExtraSinergias += 0;
                                        poke.vidaExtraSinergias += 0;
                                    }
                                }
                            }
                            break;
                    }
                    break;//// LOGICA TIPO DRAGON
            }
        }

        for(int i = 0; i < arrayClases.Length; i++) 
        {
            GameObject UIDesactivada = listaUIRolesDesactivadas[i];
            TextMeshProUGUI numPiezasDesactivada = listaUIRolesDesactivadas[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>();

            GameObject UIActivada = listaUIRolesActivadas[i];
            TextMeshProUGUI numPiezasActivada = listaUIRolesActivadas[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            switch (i)
            {
                case 0: //////// LOGICA DE LA SINERGIA TANQUE/////////
                    switch(arrayClases[i]) 
                    {
                        case < 2:
                            //Poner a 0 la reduccion de daño plana al calcular daño
                            foreach(Personaje poke in listaPiezasEnTablero)
                            {
                                poke.mitigacionTanque = 0;
                            }
                            if (arrayClases[i] > 0)
                            {
                                UIDesactivada.SetActive(true);
                                numPiezasDesactivada.text = arrayClases[i].ToString();
                            }
                            else
                            {
                                UIDesactivada.SetActive(false);
                            }
                            UIActivada.SetActive(false);
                            break;
                        case >= 2:                            
                            if (arrayClases[i] >= 4)
                            {
                                //Poner a 80 la reduccion de daño plana
                                foreach (Personaje poke in listaPiezasEnTablero)
                                {
                                    poke.mitigacionTanque = 80;
                                }
                            }
                            else
                            {
                                //Ponerla a 40
                                foreach (Personaje poke in listaPiezasEnTablero)
                                {
                                    poke.mitigacionTanque = 40;
                                }
                            }
                            UIActivada.SetActive(true);
                            numPiezasActivada.text = arrayClases[i].ToString();
                            UIDesactivada.SetActive(false);
                            break;
                    }
                    break;//// LOGICA SINERGIA TANQUE
                case 1: ///////// LOGICA DE LA SINERGIA LUCHADOR
                    switch (arrayClases[i])
                    {
                        case < 2:
                            //Quitar las posibles addiciones de esta sinergia
                            if (arrayClases[i] > 0)
                            {
                                UIDesactivada.SetActive(true);
                                numPiezasDesactivada.text = arrayClases[i].ToString();
                            }
                            else
                            {
                                UIDesactivada.SetActive(false);
                            }
                            UIActivada.SetActive(false);
                            foreach (Personaje poke in listaPiezasEnTablero)
                            {
                                poke.vidaExtraSinergias += 0;
                            }
                            break;
                        case >= 2:
                            if (arrayClases[i] >= 6)
                            {
                                foreach(Personaje poke in listaPiezasEnTablero)
                                {
                                    if(poke.data.Rol == CharactersDataSO._Rol.Luchador)
                                    {
                                        poke.vidaExtraSinergias += 600;
                                    }
                                    else
                                    {
                                        poke.vidaExtraSinergias += 100;
                                    }
                                }
                            }
                            else if(arrayClases[i] >= 4 && arrayClases[i] < 6)
                            {
                                foreach (Personaje poke in listaPiezasEnTablero)
                                {
                                    if (poke.data.Rol == CharactersDataSO._Rol.Luchador)
                                    {
                                        poke.vidaExtraSinergias += 400;
                                    }
                                    else
                                    {
                                        poke.vidaExtraSinergias += 100;
                                    }
                                }
                            }
                            else
                            {
                                foreach (Personaje poke in listaPiezasEnTablero)
                                {
                                    if (poke.data.Rol == CharactersDataSO._Rol.Luchador)
                                    {
                                        poke.vidaExtraSinergias += 200;
                                    }
                                    else
                                    {
                                        poke.vidaExtraSinergias += 100;
                                    }
                                }
                            }
                            UIActivada.SetActive(true);
                            numPiezasActivada.text = arrayClases[i].ToString();

                            UIDesactivada.SetActive(false);
                            break;
                    }
                    break;//// LOGICA SINERGIA LUCHADOR
                case 2: /////// LOGICA DE LA SINERGIA CAZADOR //////////////////
                    switch (arrayClases[i])
                    {
                        case < 3:
                            //Quitar las posibles adiciones de esta sinergia
                            if (arrayClases[i] > 0)
                            {
                                UIDesactivada.SetActive(true);
                                numPiezasDesactivada.text = arrayClases[i].ToString();
                            }
                            else
                            {
                                UIDesactivada.SetActive(false);
                            }
                            UIActivada.SetActive(false);
                            break;
                        case >=3:
                            if (arrayClases[i] >= 6)
                            {
                                foreach (Personaje poke in listaPiezasEnTablero)
                                {
                                    if (poke.data.Rol == CharactersDataSO._Rol.Cazador)
                                    {
                                        poke.atkExtraSinergias += 150;
                                        poke.atkSpeedSinergias += 0.6f;
                                    }
                                  
                                }
                                
                            }                      
                            else
                            {
                                foreach (Personaje poke in listaPiezasEnTablero)
                                {
                                    if (poke.data.Rol == CharactersDataSO._Rol.Cazador)
                                    {
                                        poke.atkExtraSinergias += 50;
                                        poke.atkSpeedSinergias += 0.2f;
                                    }

                                }
                            }
                            UIActivada.SetActive(true);
                            numPiezasActivada.text = arrayClases[i].ToString();
                            UIDesactivada.SetActive(false);
                            break;
                        
                    }
                    break;//// LOGICA SINERGIA CAZADOR
                case 3: ////////////// LOGICA DE LA SINERGIA LANZAHABILIDADES////////////////////
                    switch(arrayClases[i]) 
                    {
                        case < 3:
                            //Quitar las posibles adiciones de la sinergia
                            if (arrayClases[i] > 0)
                            {
                                UIDesactivada.SetActive(true);
                                numPiezasDesactivada.text = arrayClases[i].ToString();
                            }
                            else
                            {
                                UIDesactivada.SetActive(false);
                            }
                            UIActivada.SetActive(false);
                            break;
                        case >=3:
                            if (arrayClases[i] >= 6)
                            {
                                foreach (Personaje poke in listaPiezasEnTablero)
                                {
                                    if (poke.data.Rol == CharactersDataSO._Rol.Lanzahabilidades)
                                    {
                                        poke.spAExtraSinergias += 60;
                                        //Activar booleana 2 de reducir SpD de los oponentes
                                    }

                                }
                                
                            }
                            else
                            {
                                foreach (Personaje poke in listaPiezasEnTablero)
                                {
                                    if (poke.data.Rol == CharactersDataSO._Rol.Lanzahabilidades)
                                    {
                                        poke.spAExtraSinergias += 20;
                                        //Activar boooleana 1 de reducir SpD de los oponentes                                        
                                    }
                                }
                            }
                            UIActivada.SetActive(true);
                            numPiezasActivada.text = arrayClases[i].ToString();
                            UIDesactivada.SetActive(false);
                            break;

                    }
                    break;//// LOGICA SINERGIA LANZAHABILIDADES
                case 4: /////////////////// LOGICA DE LA SINERGIA GUARDIAN
                    switch (arrayClases[i])
                    {
                        case < 2:
                            //Quitar las posibles booleanas
                            if (arrayClases[i] > 0)
                            {
                                UIDesactivada.SetActive(true);
                                numPiezasDesactivada.text = arrayClases[i].ToString();
                            }
                            else
                            {
                                UIDesactivada.SetActive(false);
                            }
                            UIActivada.SetActive(false);
                            break;
                        case >=2:
                            if (arrayClases[i] >= 4)
                            {
                                //Activar la booleana de 4 guardianes
                            }
                            else
                            {
                                //Activar booleana de 2 guardianes
                            }
                            UIActivada.SetActive(true);
                            numPiezasActivada.text = arrayClases[i].ToString();
                            UIDesactivada.SetActive(false);
                            break;
                    }
                    break;//// LOGICA SINERGIA GUARDIAN
                case 5: //////////////////////LOGICA DE LA CLASE MISTICO
                    switch (arrayClases[i])
                    {
                        case < 2:
                            //Quitar las posibles mejoras
                            if (arrayClases[i] > 0)
                            {
                                UIDesactivada.SetActive(true);
                                numPiezasDesactivada.text = arrayClases[i].ToString();
                            }
                            else
                            {
                                UIDesactivada.SetActive(false);
                            }
                            UIActivada.SetActive(false);
                            break;
                        case >=2:
                            if (arrayClases[i] >= 4)
                            {
                                foreach (Personaje poke in listaPiezasEnTablero)
                                {
                                    if(poke.data.Rol == CharactersDataSO._Rol.Mistico)
                                    {
                                        poke.spDExtraSinergias += 80;
                                    }
                                    else
                                    {
                                        poke.spDExtraSinergias += 40;
                                    }
                                }
                            }
                            else
                            {
                                foreach (Personaje poke in listaPiezasEnTablero)
                                {
                                    if (poke.data.Rol == CharactersDataSO._Rol.Mistico)
                                    {
                                        poke.spDExtraSinergias += 40;
                                    }
                                    else
                                    {
                                        poke.spDExtraSinergias += 20;
                                    }
                                }
                            }
                            UIActivada.SetActive(true);
                            numPiezasActivada.text = arrayClases[i].ToString();
                            UIDesactivada.SetActive(false);
                            break;
                    }
                    break;//// LOGICA SINERGIA MISTICO

            }
        }
    }

    public override void SinergiaUIDesactivadaActualizar(int posArray, GameObject UIDesactivada, TextMeshProUGUI numPiezasDesactivada)
    {
        UIDesactivada.SetActive(true);
        numPiezasDesactivada.text = arrayTipos[posArray].ToString();
      
    }

    public override void SinergiaUIDesactivadaFalse(int posArray,GameObject UIDesactivada)
    {
        UIDesactivada.SetActive(false);
    }

    public override void SinergiaUIActivadaFalse(int posArray, GameObject UIActivada)
    {
        UIActivada.SetActive(false);
    }

    public override void SinergiaUIActivadaActualizar(int posArray, GameObject UIDesactivada, GameObject UIActivada, TextMeshProUGUI numPiezasActivada)
    {      

        UIActivada.SetActive(true);
        numPiezasActivada.text = arrayTipos[posArray].ToString();
        UIDesactivada.SetActive(false);
    }

    
}
