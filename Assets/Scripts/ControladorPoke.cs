using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator), typeof(Rigidbody), typeof(BoxCollider))]
[RequireComponent(typeof(NavMeshAgent), typeof(EnseñarInfoPoke), typeof(Personaje))]
public class ControladorPoke : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;
    public Transform target;
    private Personaje myStats;
    [HideInInspector] public bool isCCed;
    public bool enCombate; //ESTE SE ACTIVARA CUANDO PASEMOS A LA FASE DE COMBATE, LO PONEMOS PUBLICO POR SI QUEREMOS TESTEAR COSAS
    public Player player;
    public EnemigoSupervivencia enemigoSupervivencia;
    private bool animMuerteEnded; 


    // MAS ADELANTE LO HAREMOS DE OTRA MANERA, PERO POR AHORA SERAN PROYECTILES TODOS LOS RANGED
    [Header("Variables para los Pokemon que tengan rango")]
    public GameObject proyectil;
    public Transform puntoInstanciaProyectil;
    
   
   
    void Start()
    {        
        myStats = GetComponent<Personaje>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        enemigoSupervivencia = GameObject.FindObjectOfType<EnemigoSupervivencia>();
        /*enCombate = true;
        animator.SetBool("enCombate", true);
        EncontrarTargetMasCercano();*/
    }  
    void Update()
    {
        if (!enCombate)
        {
            animator.SetBool("enCombate", false);
            return;
        }
        if (isCCed)
        {
            animator.SetBool("isCCed", true); // ESTO QUIZA PODAMOS HACERLO EN OTRO SITIO, YA VEREMOS            
            return;
        }
        transform.LookAt(target);
        if (!target.gameObject.activeSelf)
        {
            
            EncontrarTargetMasCercano();
            
        }
        if (!HaLlegadoASuDestino())
        {
            animator.SetBool("HaLlegado", false);               
            agent.SetDestination(target.position);

        }
        else
        {                  
            animator.SetBool("HaLlegado", true);
        }


    }

    public void EncontrarTargetMasCercano()
    {
        //LOGICA SI ES UN POKE DEL JUGADOR
        if (myStats.jugador == player)
        {
            // >>>>>>> DETECCION ENEMIGOS
            myStats.jugador.DetectarPokesActivados(); //leer que hay que hacer cosas, haciendolo asi, no dara error el ultimo? Porque buscara un target cuando no lo hay.
                                                        // Lo mejor seria hacerlo despues de matar al bicho, pero fuera de encontrar el target <<<<<<----------
            target = myStats.jugador.DetectarEnemigoMasCercano(transform, target); //NO SE SI FUNCIONARA
        }
        // LOGICA SI ES UN POKE DEL ENEMIGO
        else
        {
            // >>>>>>> DETECCION ENEMIGOS            
            enemigoSupervivencia.DetectarPokesActivados();// leer que hay que hacer cosas
            target = enemigoSupervivencia.DetectarEnemigoMasCercano(transform, target); // NO SE SI FUNCIONA
        }

        agent.SetDestination(target.position);

        #region Segundo Modo
        // --------------------------SEGUNDO MODO ------------------------------------//


        //ESTO TENDREMOS QUE MODIFICARLO MAS ADELANTE, AHORA MISMO DETECTA SI ESTE ES EL JUGADOR Y NO LA IA, PERO EN MULTIPLAYER NO SE COMO SE HARA
        // NO SE COMO FUNCIONARA EL MODIFICAR EL TAMAÑO DEL ARRAY DE ESA MANERA EN EL ELSE LA VERDAD, OJALA USASEN LISTAS LOS DE FINDGAMEOBJECTWITHTAG
        /*  if (!enemigosEncontrados)
          {
              if (myStats.jugador == GameObject.FindGameObjectWithTag("Player").GetComponent<Jugador>())
              {                
                  enemigosList.AddRange(myStats.jugador.listaPokesEnemigo); 


              }
              else
              {               
                  enemigosList.AddRange(GameObject.FindObjectOfType<EnemigoSupervivencia>().listaPokesPlayer);

              }
              enemigosEncontrados = true;
          }

              // >>>>>>> DETECCION ENEMIGOS
          //PARA DETECTAR QUE NO HAY MAS ENEMIGOS ACTIVOS EN LA ESCENA Y SE ACABE LA RONDA, PROBLEMA, QUE PUEDE QUE LO HAGA MAS DE UNO AHORA MISMO
          for(int i = 0; i < enemigosList.Count; i++)
          {
              if (enemigosList[i].gameObject.activeSelf)
              {
                  break;        
              }
              if (i == enemigosList.Count - 1) //ULTIMO OBJETO DE LA LISTA
              {
                  enCombate = false;
                  return;
              }
          }

          foreach (Personaje pokeEnemigo in enemigosList)
          {
              if (!pokeEnemigo.gameObject.activeSelf) continue;
              // CALCULAMOS AL ENEMIGO MAS CERCANO
              distanciaEnemigo = Vector3.Distance(transform.position, pokeEnemigo.transform.position);

              // MIRAMOS SI HAY UN TARGET O NO, SI NO LO HAY ASIGNAMOS, SINO, COMPARAMOS
              if(target != null)
              {
                  if(distanciaEnemigo < Vector3.Distance(transform.position, target.position)) { target = pokeEnemigo.transform; }

              }  
              else
              {
                  target = pokeEnemigo.transform;
              }

          }

          agent.SetDestination(target.position);*/
        #endregion
    }

    private bool HaLlegadoASuDestino()
    {
        if(!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance) { return true; }
        else { return false; }
    }


    public void AtaqueBasico()
    {
        if(target == null) { return; }
        Personaje poke = target.GetComponent<Personaje>();

       
        // CALCULAMOS EL % DE LO QUE MITIGAN ESAS DEFENSAS Y LO RESTAMOS DE 100 PARA SABER CUANTO DAÑO PASA
        
        float mitigacionPorDefensa = 100f -(((float)poke.defMax / (100 + (float)poke.defMax))*100); 
        float dañoHecho = myStats.atkMax * (mitigacionPorDefensa/100);
        dañoHecho = dañoHecho * poke.reduccionDaño; // SI TIENE REDUCCION DE DAÑO AQUI SE LO QUITAREMOS, reduccionDaño = 1 si no hay reduccion, 0.8 si hay 20% etc.
        //REDUCIMOS AL DAÑO HECHO LA MITIGACION DE LA SINERGIA TANQUE, ES 0 SI NO LA TIENE
        dañoHecho -= poke.mitigacionTanque;
        // MIRAMOS SI EL OPONENTE TIENE ESCUDO
        if(poke.vidaEscudo > 0)
        {
            dañoHecho -= poke.vidaEscudo;
            poke.vidaEscudo -= (int)dañoHecho;
            //Actualizar la cantidad de escudo en el Slider del target
            if(dañoHecho < 0) { dañoHecho = 0; } //PARA QUE NO HAYA DAÑO NEGATIVO
            if(poke.vidaEscudo < 0) { poke.vidaEscudo = 0; } // PARA QUE NO HAYA ESCUDO NEGATIVO


        }

        poke.vidaActual -= (int)dañoHecho;
        // HAY QUE ACTUALIZAR LA VIDA EN EL SLIDER QUE SE LE VAYA A HACER
        if(poke.vidaActual <= 0 && !poke.CompareTag("Muerto"))
        {
            //Lanzo el SetTrigger de Muerto tanto aqui como en la corrutina AnimacionMorir, PUEDO por favor decidir donde hacerlo

            poke.tag = "Muerto"; // HABRA QUE MODIFICAR LOS TAGS AL INICIAR EL COMBATE
            poke.animator.SetTrigger("Muerto");           

            target = null;
            // LOGICA SI SON POKES DEL PLAYER
            if(myStats.jugador == player)
            {
                myStats.jugador.listaPokesEnemigo.Remove(poke);
            }
            else //SI NO LO SON
            {
                enemigoSupervivencia.listaPokesPlayer.Remove(poke);
            }
            StartCoroutine(AnimacionMorir(/*duracionAnim,*/ poke));
            
            EncontrarTargetMasCercano();

            //LOGICA DE CUANDO EL POKE MUERE, ANIMATOR, DESTROY O LO QUE HAGA FALTA
            // NO ME GUSTA DESTROY PORQUE PERDERIAMOS TODOS LOS DATOS Y HABRIA QUE GUARDAR SU POSICION ETC, SetActive(false), parece mejor
            // y luego los volvemos a poner en su posicion, que seria (0,0.5f,0) si mi memoria no falla
        }


    }


    // LO HAREMOS COMO ESTA HECHO EN EL SCRIPT TESTANIM, LA BOOLEANA CON UN EVENTO EN EL ULTIMO FRAME DE TODAS LAS ANIMACIONES Y AL CARAJO 
    IEnumerator AnimacionMorir(Personaje poke)
    {
        ControladorPoke controlPoke = poke.gameObject.GetComponent<ControladorPoke>();
        yield return null;
        Animator pokeMuerto = poke.animator;
        pokeMuerto.SetTrigger("Muerto");
        /*AnimatorStateInfo estadoActual = pokeMuerto.GetCurrentAnimatorStateInfo(0);
       
        AnimatorClipInfo[] animatorinfo = pokeMuerto.GetCurrentAnimatorClipInfo(0);*/

        while (!controlPoke.animMuerteEnded)
        {
            poke.transform.localScale = Vector3.Lerp(poke.transform.localScale, Vector3.zero, Time.deltaTime * 2);
            yield return null;
        }

        controlPoke.animMuerteEnded = false;
        poke.transform.localScale = Vector3.zero;

        poke.gameObject.SetActive(false);        

    }
    public void AtaqueRanged()
    {
        GameObject esfera = Instantiate(proyectil, puntoInstanciaProyectil);
        ControladorProyectil controlProyectil = esfera.GetComponent<ControladorProyectil>();
        controlProyectil.pokeAtacante = this;
        controlProyectil.target = target;
    }

    public void DetectarFinAnimacionMuerte()
    {
        animMuerteEnded = true;
    }
}
