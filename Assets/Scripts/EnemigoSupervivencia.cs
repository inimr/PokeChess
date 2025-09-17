using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EquiposEnemigo
{
    //Tiene que ser lista para que sea mas de uno
    public List<GameObject> objectsEnemigos;
    
    //LA POSICION DE ESTE OBJETO ESTA MOVIDA A POSTA PARA QUE ENCAJE CON EL TABLERO
}
public class EnemigoSupervivencia : Jugadores
{

    public List<EquiposEnemigo> enemigo;
    public List<Personaje> listaPokesPlayer;
    public Player player;
    private void Start()
    {
        arrayClases = new int[System.Enum.GetValues(typeof(CharactersDataSO._Rol)).Length];
        arrayTipos = new int[System.Enum.GetValues(typeof(CharactersDataSO._Tipo)).Length];
    }

    public void DecidirTeam()
    {
        IDPiezasEnTablero.Clear();
        listaPiezasEnTablero.Clear();

        int numero = Random.Range(0, enemigo[ControladorFases.instance.ronda - 1].objectsEnemigos.Count);

        // HACEMOS AL EQUIPO HIJO DEL HIJO DE ESTE SCRIPT
        GameObject equipoAInstanciar = Instantiate(enemigo[ControladorFases.instance.ronda - 1].objectsEnemigos[numero], gameObject.transform);

        int numeroPokes = equipoAInstanciar.transform.childCount;

       

        for (int i = 0; i < numeroPokes; i++)
        {
           
            listaPiezasEnTablero.Add(equipoAInstanciar.transform.GetChild(i).GetComponent<Personaje>());
            equipoAInstanciar.transform.GetChild(i).gameObject.tag = "Oponente";
        }


    }

    public void MostrarEquipoCombate()
    {
        DecidirTeam();
        AplicarLasSinergias();        
    }

    public void ActivarLosEnemigos()
    {
        listaPokesPlayer.AddRange(player.listaPiezasEnTablero);
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
        for (int i = 0; i < listaPokesPlayer.Count; i++)
        {
            if (listaPokesPlayer[i].gameObject.activeSelf) { break; }
            if (i == listaPokesPlayer.Count - 1)
            {             
                /* Activar la logica de cambiar de fase.
                 Pasar todos los pokes a modo NO combate
                Pasar a false su booleana en combate.*/

                //PENSAR SI SE PUEDE COGER EL PLAYER DE OTRA MANERA
                Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
                int fichasRestantes = 0;
                foreach (Personaje poke in listaPiezasEnTablero)
                {
                    if (poke.gameObject.activeSelf) fichasRestantes++;
                }

                player.QuitarVida(fichasRestantes);
                if (ControladorFases.instance.state == ControladorFases.FaseJuego.Final) return;

                StartCoroutine(ControladorFases.instance.PreparacionFaseTienda());
                return;
            }
        }
    }
    public void DesactivarLosEnemigos()
    {
        foreach(Personaje poke in listaPiezasEnTablero)
        {
            Destroy(poke.gameObject);
        }
        listaPiezasEnTablero.Clear();
        // Destruimos el objeto vacio
        if(transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
        
    }
    public Transform DetectarEnemigoMasCercano(Transform miPokeTransform, Transform target)
    {
        foreach (Personaje pokeEnemigo in listaPokesPlayer)
        {
            if (!pokeEnemigo.gameObject.activeSelf) continue;
            // CALCULAMOS AL ENEMIGO MAS CERCANO
           float distanciaEnemigo = Vector3.Distance(miPokeTransform.position, pokeEnemigo.transform.position);

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

    public void PrepararArrays()
    {
        foreach (Personaje poke in listaPiezasEnTablero)
        {
            CharactersDataSO characterData = poke.data;
            if (!IDPiezasEnTablero.Contains(characterData.ID))
            {
                IDPiezasEnTablero.Add(characterData.ID);
                CharactersDataSO._Tipo _tipo = characterData.Tipo;
                arrayTipos[(int)_tipo]++;

                CharactersDataSO._Rol _rol = characterData.Rol;
                arrayClases[(int)_rol]++;

            }
            poke.vidaExtraSinergias = 0;
            poke.atkExtraSinergias = 0;
            poke.defExtraSinergias = 0;
            poke.spAExtraSinergias = 0;
            poke.spDExtraSinergias = 0;
            poke.atkSpeedSinergias = 0;
        }
    }
    private void AplicarLasSinergias()
    {   

        for (int i = 0; i < arrayTipos.Length; i++)        
        {
            
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
                                

                            }
                            else
                            {

                            }
                           
                            break;
                        case >= 2:

                           
                            if (arrayTipos[i] >= 4)
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
                           
                           
                            if (arrayTipos[i] > 0)
                            {
                                
                            }
                            else {  }


                            break;
                        case >= 2:
                            if (arrayTipos[i] >= 4)
                            {
                                //Activar solamente la sinergia 4

                            }
                            else
                            {
                                //Activar solamente el de sinergia 2
                            }
                          

                            break;
                    }
                    break;//// LOGICA TIPO HIELO
                case 2: ///////////// LOGICA DEL TIPO FANTASMA ////////// 
                    switch (arrayTipos[i])
                    {
                        case < 2:
                            //Desactivar la booleana de la sinergia fantasma
                            if (arrayTipos[i] > 0)
                            {
                                
                            }
                            else
                            {
                                
                            }
                           
                            break;
                        case >= 2:
                            //Activar la booleana de la sinergia fantasma
                            
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
                                
                            }
                            else
                            {
                                
                            }
                           
                            break;
                        case >= 2:
                            if (arrayTipos[i] >= 4)
                            {
                                //Activar la booleana de 4, desactivar el resto

                            }
                            else
                            {
                                //Activar la de 2
                            }
                            

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
                                
                            }
                            else
                            {
                                
                            }
                            
                            break;
                        case >= 2:
                            if (arrayTipos[i] >= 6)
                            {
                                foreach (Personaje poke in listaPiezasEnTablero)
                                {
                                    if (poke.data.Tipo == CharactersDataSO._Tipo.Volador)
                                    {
                                        poke.atkSpeedSinergias += 0.6f;
                                    }
                                    else
                                    {
                                        poke.atkSpeedSinergias += 0.3f;
                                    }
                                }
                            }
                            else if (arrayTipos[i] >= 4 && arrayTipos[i] < 6)
                            {
                                foreach (Personaje poke in listaPiezasEnTablero)
                                {
                                    if (poke.data.Tipo == CharactersDataSO._Tipo.Volador)
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
                                foreach (Personaje poke in listaPiezasEnTablero)
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

                         
                            break;

                    }
                    break;//// LOGICA TIPO VOLADOR
                case 5: ///////// LOGICA DEL TIPO FUEGO /////////////////
                    switch (arrayTipos[i])
                    {
                        case < 2:
                            //Desactivar todas las booleanas de las sinergias
                            if (arrayTipos[i] > 0)
                            {
                               
                            }
                            else
                            {
                                
                            }
                            
                            break;
                        case >= 2:
                            if (arrayTipos[i] >= 6)
                            {
                                //Activar la sinergia de fuego 3
                                //Desactivar el resto

                            }
                            else if (arrayTipos[i] >= 4 && arrayTipos[i] < 6)
                            {
                                //Activar la segunda sinergia, desactivar el resto

                            }
                            else
                            {
                                //Activar la primera sinergia
                            }
                          

                            break;
                    }
                    break;//// LOGICA TIPO FUEGO
                case 6: ////////////////LOGICA DEL TIPO DRAGON ///////////////
                    switch (arrayTipos[i])
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
                         
                            break;
                        case 1:
                            foreach (Personaje poke in listaPiezasEnTablero)
                            {
                                if (poke.data.Tipo == CharactersDataSO._Tipo.Dragon)
                                {
                                    poke.atkExtraSinergias += 50;
                                    poke.spAExtraSinergias += 50;
                                    poke.vidaExtraSinergias += 200;
                                }
                            }
                          
                            break;
                        case > 1:
                            if (IDPiezasEnTablero.Contains(30)) // EL ID DE PALKIA
                            {
                                foreach (Personaje poke in listaPiezasEnTablero)
                                {
                                    if (poke.data.Tipo == CharactersDataSO._Tipo.Dragon)
                                    {
                                        poke.atkExtraSinergias += 50;
                                        poke.spAExtraSinergias += 50;
                                        poke.vidaExtraSinergias += 200;
                                    }
                                }

                             

                            }
                            else
                            {
                             


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

        for (int i = 0; i < arrayClases.Length; i++)        {
            
            switch (i)
            {
                case 0: //////// LOGICA DE LA SINERGIA TANQUE/////////
                    switch (arrayClases[i])
                    {
                        case < 2:
                            //Poner a 0 la reduccion de daño plana al calcular daño
                            foreach (Personaje poke in listaPiezasEnTablero)
                            {
                                poke.mitigacionTanque = 0;
                            }
                            if (arrayClases[i] > 0)
                            {
                                
                            }
                            else
                            {
                                
                            }
                            
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
                                
                            }
                            else
                            {
                                
                            }
                            
                            foreach (Personaje poke in listaPiezasEnTablero)
                            {
                                poke.vidaExtraSinergias += 0;
                            }
                            break;
                        case >= 2:
                            if (arrayClases[i] >= 6)
                            {
                                foreach (Personaje poke in listaPiezasEnTablero)
                                {
                                    if (poke.data.Rol == CharactersDataSO._Rol.Luchador)
                                    {
                                        poke.vidaExtraSinergias += 600;
                                    }
                                    else
                                    {
                                        poke.vidaExtraSinergias += 100;
                                    }
                                }
                            }
                            else if (arrayClases[i] >= 4 && arrayClases[i] < 6)
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
                               
                            }
                            else
                            {
                                
                            }
                            
                            break;
                        case >= 3:
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
                           
                            break;

                    }
                    break;//// LOGICA SINERGIA CAZADOR
                case 3: ////////////// LOGICA DE LA SINERGIA LANZAHABILIDADES////////////////////
                    switch (arrayClases[i])
                    {
                        case < 3:
                            //Quitar las posibles adiciones de la sinergia
                            if (arrayClases[i] > 0)
                            {
                               
                            }
                            else
                            {
                               
                            }
                         
                            break;
                        case >= 3:
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
                                
                            }
                            else
                            {
                                
                            }
                         
                            break;
                        case >= 2:
                            if (arrayClases[i] >= 4)
                            {
                                //Activar la booleana de 4 guardianes
                            }
                            else
                            {
                                //Activar booleana de 2 guardianes
                            }
                           
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
                                
                            }
                            else
                            {
                               
                            }
                            
                            break;
                        case >= 2:
                            if (arrayClases[i] >= 4)
                            {
                                foreach (Personaje poke in listaPiezasEnTablero)
                                {
                                    if (poke.data.Rol == CharactersDataSO._Rol.Mistico)
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
                           
                            break;
                    }
                    break;//// LOGICA SINERGIA MISTICO

            }
        }
    }
}
