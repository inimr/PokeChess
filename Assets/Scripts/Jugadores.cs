using System.Collections.Generic;
using TMPro;
using UnityEngine;



//Clase que guarda los metodos que usan los diferentes jugadores, tanto IA como no

// Pasarselo a ChatGPT si no nos resuelven las dudas Antonio/Oscar
// Tenia que hacer algo con virtual y se me ha olvidado
public class Jugadores : MonoBehaviour
{
    //Lista de las piezas en el tablero
    public List<Personaje> listaPiezasEnTablero = new();

    //Arrays de los tipos y clases
    public int[] arrayTipos;
    public int[] arrayClases;
    public List<int> IDPiezasEnTablero;

    public void AplicarSinergias(Jugadores jugador)
    {
       
        if (jugador is Player player)
        {           
            player.CheckEnTablero();
        }

        if(jugador is EnemigoSupervivencia enemigo)
        {
            enemigo.PrepararArrays();
        }

        ActualizarSinergias(jugador);
    }

   
    public void ActualizarSinergias(Jugadores jugador)
    {
        SinergiasTipos(jugador);
        SinergiasClases(jugador);
    }

    void SinergiasTipos(Jugadores jugador)
    {
        for (int i = 0; i < jugador.arrayTipos.Length; i++)
        {
            GameObject UIDesactivada = null; 
            TextMeshProUGUI numPiezasDesactivada = null;

            GameObject UIActivada = null;
            TextMeshProUGUI numPiezasActivada = null;

            if(jugador is Player player)
            {
                UIDesactivada = player.listaUITiposDesactivadas[i];
                numPiezasDesactivada = player.listaUITiposDesactivadas[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>();

                UIActivada = player.listaUITipoActivadas[i];
                numPiezasActivada = player.listaUITipoActivadas[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            }
            switch (i)
            {
                case 0: ///////////// LOGICA TIPO ACERO 
                    LogicaTipoAcero(i, jugador, UIDesactivada, UIActivada, numPiezasDesactivada, numPiezasActivada);
                    break;
                case 1: //////////// LOGICA TIPO HIELO
                    LogicaTipoHielo(i, jugador, UIDesactivada, UIActivada, numPiezasDesactivada, numPiezasActivada);
                    break;
                case 2: //////////// LOGICA TIPO FANTASMA
                    LogicaTipoFantasma(i, jugador, UIDesactivada, UIActivada, numPiezasDesactivada, numPiezasActivada);
                    break;
                case 3: //////////// LOGICA TIPO ELECTRICO
                    LogicaTipoElectrico(i, jugador, UIDesactivada, UIActivada, numPiezasDesactivada, numPiezasActivada);
                    break;
                case 4: /////////// LOGICA TIPO VOLADOR
                    LogicaTipoVolador(i, jugador, UIDesactivada, UIActivada, numPiezasDesactivada, numPiezasActivada);
                    break;
                case 5: /////////// LOGICA TIPO FUEGO
                    LogicaTipoFuego(i, jugador, UIDesactivada, UIActivada, numPiezasDesactivada, numPiezasActivada);
                    break;
                case 6: ////////// LOGICA TIPO DRAGON
                    LogicaTipoDragon(i, jugador, UIDesactivada, UIActivada, numPiezasDesactivada, numPiezasActivada);
                    break;
            }
        }
    }

    void SinergiasClases(Jugadores jugador)
    {
        // HAY QUE PASAR EL RESTO TAMBIEN 
        for(int i = 0; i< jugador.arrayClases.Length; i++)
        {
            GameObject UIDesactivada = null;
            TextMeshProUGUI numPiezasDesactivada = null;

            GameObject UIActivada = null;
            TextMeshProUGUI numPiezasActivada = null;

            if (jugador is Player player)
            {
                UIDesactivada = player.listaUIRolesDesactivadas[i];
                numPiezasDesactivada = player.listaUIRolesDesactivadas[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>();

                UIActivada = player.listaUIRolesActivadas[i];
                numPiezasActivada = player.listaUIRolesActivadas[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            }
            switch (i) 
            {
                case 0: ///////// LOGICA CLASE TANQUE
                    LogicaClaseTanque(i, jugador, UIDesactivada, UIActivada, numPiezasDesactivada, numPiezasActivada);
                    break;
                case 1: //////// LOGICA CLASE LUCHADOR
                    LogicaClaseLuchador(i, jugador, UIDesactivada, UIActivada, numPiezasDesactivada, numPiezasActivada);
                    break;
                case 2: /////// LOGICA CLASE CAZADOR
                    LogicaClaseCazador(i, jugador, UIDesactivada, UIActivada, numPiezasDesactivada, numPiezasActivada);
                    break;
                case 3: ////// LOGICA CLASE LANZAHABILIDADES
                    LogicaClaseLanzaHabilidades(i, jugador, UIDesactivada, UIActivada, numPiezasDesactivada, numPiezasActivada);
                    break;
                case 4: ////// LOGICA CLASE GUARDIAN
                    LogicaClaseGuardian(i, jugador, UIDesactivada, UIActivada, numPiezasDesactivada, numPiezasActivada);
                    break;
                case 5: ///// LOGICA CLASE MISTICO
                    LogicaClaseMistico(i, jugador, UIDesactivada, UIActivada, numPiezasDesactivada, numPiezasActivada);
                    break;
            }
        }
    }
    #region LogicaTipos
    void LogicaTipoAcero(int numeroArray, Jugadores jugador, GameObject UIDesactivada, GameObject UIActivada, TextMeshProUGUI numPiezasDesactivada, TextMeshProUGUI numPiezasActivada)
    {
        switch (jugador.arrayTipos[numeroArray])
        {
            case < 2:
                foreach (Personaje poke in jugador.listaPiezasEnTablero)
                {
                    poke.defExtraSinergias += 0;
                }
                switch (jugador.arrayTipos[numeroArray])
                {
                    case < 2:
                        foreach (Personaje poke in jugador.listaPiezasEnTablero)
                        {
                            poke.defExtraSinergias += 0;

                        }

                        if (jugador.arrayTipos[numeroArray] > 0) jugador.SinergiaUIDesactivadaActualizar(numeroArray, UIDesactivada, numPiezasDesactivada);

                        else jugador.SinergiaUIDesactivadaFalse(numeroArray, UIDesactivada);

                        jugador.SinergiaUIActivadaFalse(numeroArray,UIActivada);
                        break;
                    case >= 2:

                        jugador.SinergiaUIActivadaActualizar(numeroArray, UIDesactivada, UIActivada, numPiezasActivada);
                        if (jugador.arrayTipos[numeroArray] >= 4)
                        {
                            foreach (Personaje poke in jugador.listaPiezasEnTablero)
                            {
                                poke.defExtraSinergias += 100;
                            }

                        }
                        else
                        {
                            foreach (Personaje poke in jugador.listaPiezasEnTablero)
                            {
                                poke.defExtraSinergias += 40;

                            }
                        }
                        break;
                }
                break;
        }
    }

    void LogicaTipoHielo(int numeroArray, Jugadores jugador, GameObject UIDesactivada, GameObject UIActivada, TextMeshProUGUI numPiezasDesactivada, TextMeshProUGUI numPiezasActivada)
    {
        switch (jugador.arrayTipos[numeroArray])
        {
            case < 2:
                if (jugador.arrayTipos[numeroArray] > 0)
                {
                    jugador.SinergiaUIDesactivadaActualizar(numeroArray, UIDesactivada, numPiezasDesactivada);
                }
                else { jugador.SinergiaUIDesactivadaFalse(numeroArray, UIDesactivada); }
                jugador.SinergiaUIActivadaFalse(numeroArray, UIActivada);


                break;
            case >= 2:
                if (jugador.arrayTipos[numeroArray] >= 4)
                {
                    //Activar solamente la sinergia 4

                }
                else
                {
                    //Activar solamente el de sinergia 2
                }

                jugador.SinergiaUIActivadaActualizar(numeroArray, UIDesactivada, UIActivada, numPiezasActivada);


                break;
        }

    }

    void LogicaTipoFantasma(int numeroArray, Jugadores jugador, GameObject UIDesactivada, GameObject UIActivada, TextMeshProUGUI numPiezasDesactivada, TextMeshProUGUI numPiezasActivada)
    {
        switch (arrayTipos[numeroArray])
        {
            case < 2:
                //Desactivar la booleana de la sinergia fantasma
                if (arrayTipos[numeroArray] > 0)
                {
                    jugador.SinergiaUIDesactivadaActualizar(numeroArray, UIDesactivada, numPiezasDesactivada);
                }
                else
                {
                    jugador.SinergiaUIDesactivadaFalse(numeroArray, UIDesactivada);
                }
                jugador.SinergiaUIActivadaFalse(numeroArray, UIActivada);

                break;
            case >= 2:
                //Activar la booleana de la sinergia fantasma
                jugador.SinergiaUIActivadaActualizar(numeroArray, UIDesactivada, UIActivada, numPiezasActivada);

                break;
        }
    }

    void LogicaTipoElectrico(int numeroArray, Jugadores jugador, GameObject UIDesactivada, GameObject UIActivada, TextMeshProUGUI numPiezasDesactivada, TextMeshProUGUI numPiezasActivada)
    {
        switch (arrayTipos[numeroArray])
        {
            case < 2:
                //Desactivar las booleanas de las sinergias de electrico
                if (arrayTipos[numeroArray] > 0)
                {
                    jugador.SinergiaUIDesactivadaActualizar(numeroArray, UIDesactivada, numPiezasDesactivada);
                }
                else
                {
                    jugador.SinergiaUIDesactivadaFalse(numeroArray, UIDesactivada);
                }
                jugador.SinergiaUIActivadaFalse(numeroArray, UIActivada);
                break;
            case >= 2:
                if (arrayTipos[numeroArray] >= 4)
                {
                    //Activar la booleana de 4, desactivar el resto

                }
                else
                {
                    //Activar la de 2
                }
                jugador.SinergiaUIActivadaActualizar(numeroArray, UIDesactivada, UIActivada, numPiezasActivada);


                break;
        }
    }
    void LogicaTipoVolador(int numeroArray, Jugadores jugador, GameObject UIDesactivada, GameObject UIActivada, TextMeshProUGUI numPiezasDesactivada, TextMeshProUGUI numPiezasActivada)
    {
        switch (arrayTipos[numeroArray])
        {
            case < 2:
                foreach (Personaje poke in listaPiezasEnTablero)
                {
                    poke.atkSpeedSinergias += 0;

                }
                if (arrayTipos[numeroArray] > 0)
                {
                    jugador.SinergiaUIDesactivadaActualizar(numeroArray, UIDesactivada, numPiezasDesactivada);

                }
                else
                {
                    jugador.SinergiaUIDesactivadaFalse(numeroArray, UIDesactivada);
                }
                jugador.SinergiaUIActivadaFalse(numeroArray, UIActivada);
                break;
            case >= 2:
                if (arrayTipos[numeroArray] >= 6)
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
                else if (arrayTipos[numeroArray] >= 4 && arrayTipos[numeroArray] < 6)
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

                jugador.SinergiaUIActivadaActualizar(numeroArray, UIDesactivada, UIActivada, numPiezasActivada);

                break;

        }
    }

    void LogicaTipoFuego(int numeroArray, Jugadores jugador, GameObject UIDesactivada, GameObject UIActivada, TextMeshProUGUI numPiezasDesactivada, TextMeshProUGUI numPiezasActivada)
    {
        switch (arrayTipos[numeroArray])
        {
            case < 2:
                //Desactivar todas las booleanas de las sinergias
                if (arrayTipos[numeroArray] > 0)
                {
                    jugador.SinergiaUIDesactivadaActualizar(numeroArray, UIDesactivada,numPiezasDesactivada);

                }
                else
                {
                    jugador.SinergiaUIDesactivadaFalse(numeroArray, UIDesactivada);
                }
                jugador.SinergiaUIActivadaFalse(numeroArray, UIActivada);
                break;
            case >= 2:
                if (arrayTipos[numeroArray] >= 6)
                {
                    //Activar la sinergia de fuego 3
                    //Desactivar el resto

                }
                else if (arrayTipos[numeroArray] >= 4 && arrayTipos[numeroArray] < 6)
                {
                    //Activar la segunda sinergia, desactivar el resto

                }
                else
                {
                    //Activar la primera sinergia
                }
                jugador.SinergiaUIActivadaActualizar(numeroArray, UIDesactivada, UIActivada, numPiezasActivada);
                break;
        }
    }

    void LogicaTipoDragon(int numeroArray, Jugadores jugador, GameObject UIDesactivada, GameObject UIActivada, TextMeshProUGUI numPiezasDesactivada, TextMeshProUGUI numPiezasActivada)
    {
        switch (arrayTipos[numeroArray])
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
                jugador.SinergiaUIDesactivadaFalse(numeroArray, UIDesactivada);
                jugador.SinergiaUIActivadaFalse(numeroArray, UIActivada);
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
                jugador.SinergiaUIActivadaActualizar(numeroArray, UIDesactivada, UIActivada, numPiezasActivada);
                break;
            case > 1:
                if (jugador.IDPiezasEnTablero.Contains(30)) // EL ID DE PALKIA
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

                    jugador.SinergiaUIActivadaActualizar(numeroArray,UIDesactivada, UIActivada, numPiezasActivada);
                }
                else
                {
                    jugador.SinergiaUIDesactivadaActualizar(numeroArray, UIDesactivada, numPiezasDesactivada);
                    SinergiaUIActivadaFalse(numeroArray, UIActivada);


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
    }
    #endregion

    #region LogicaClases
    void LogicaClaseTanque(int numeroArray, Jugadores jugador, GameObject UIDesactivada, GameObject UIActivada, TextMeshProUGUI numPiezasDesactivada, TextMeshProUGUI numPiezasActivada)
    {
        switch (arrayClases[numeroArray])
        {
            case < 2:
                //Poner a 0 la reduccion de daño plana al calcular daño
                foreach (Personaje poke in jugador.listaPiezasEnTablero)
                {
                    poke.mitigacionTanque = 0;
                }
                if (arrayClases[numeroArray] > 0)
                {
                    jugador.SinergiaUIDesactivadaActualizar(numeroArray, UIDesactivada, numPiezasDesactivada);
                }
                else
                {
                    jugador.SinergiaUIDesactivadaFalse(numeroArray, UIDesactivada);
                }
                jugador.SinergiaUIActivadaFalse(numeroArray, UIActivada);
                break;
            case >= 2:
                if (arrayClases[numeroArray] >= 4)
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
                jugador.SinergiaUIActivadaActualizar(numeroArray, UIDesactivada, UIActivada, numPiezasActivada);
                break;
        }
    }
    void LogicaClaseLuchador(int numeroArray, Jugadores jugador, GameObject UIDesactivada, GameObject UIActivada, TextMeshProUGUI numPiezasDesactivada, TextMeshProUGUI numPiezasActivada)
    {
        switch (arrayClases[numeroArray])
        {
            case < 2:
                //Quitar las posibles addiciones de esta sinergia
                if (arrayClases[numeroArray] > 0)
                {
                    jugador.SinergiaUIDesactivadaActualizar(numeroArray, UIDesactivada, numPiezasDesactivada);

                }
                else
                {
                    jugador.SinergiaUIDesactivadaFalse(numeroArray, UIDesactivada);
                }
                jugador.SinergiaUIActivadaFalse(numeroArray, UIActivada);
                foreach (Personaje poke in listaPiezasEnTablero)
                {
                    poke.vidaExtraSinergias += 0;
                }
                break;
            case >= 2:
                if (arrayClases[numeroArray] >= 6)
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
                else if (arrayClases[numeroArray] >= 4 && arrayClases[numeroArray] < 6)
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
                jugador.SinergiaUIActivadaActualizar(numeroArray, UIDesactivada, UIActivada, numPiezasActivada);

                break;
        }
    }
    void LogicaClaseCazador(int numeroArray, Jugadores jugador, GameObject UIDesactivada, GameObject UIActivada, TextMeshProUGUI numPiezasDesactivada, TextMeshProUGUI numPiezasActivada)
    {
        switch (arrayClases[numeroArray])
        {
            case < 3:
                //Quitar las posibles adiciones de esta sinergia
                if (arrayClases[numeroArray] > 0)
                {
                    jugador.SinergiaUIDesactivadaActualizar(numeroArray, UIDesactivada, numPiezasDesactivada);

                }
                else
                {
                    jugador.SinergiaUIDesactivadaFalse(numeroArray, UIDesactivada);

                }
                jugador.SinergiaUIActivadaFalse(numeroArray, UIActivada);
                break;
            case >= 3:
                if (arrayClases[numeroArray] >= 6)
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
                jugador.SinergiaUIActivadaActualizar(numeroArray, UIDesactivada, UIActivada, numPiezasActivada);

                break;

        }
    } 
    void LogicaClaseLanzaHabilidades(int numeroArray, Jugadores jugador, GameObject UIDesactivada, GameObject UIActivada, TextMeshProUGUI numPiezasDesactivada, TextMeshProUGUI numPiezasActivada)
    {
        switch (arrayClases[numeroArray])
        {
            case < 3:
                //Quitar las posibles adiciones de la sinergia
                if (arrayClases[numeroArray] > 0)
                {
                    jugador.SinergiaUIDesactivadaActualizar(numeroArray, UIDesactivada, numPiezasDesactivada);

                }
                else
                {
                    jugador.SinergiaUIDesactivadaFalse(numeroArray, UIDesactivada);
                }
                jugador.SinergiaUIActivadaFalse(numeroArray, UIActivada);
                break;
            case >= 3:
                if (arrayClases[numeroArray] >= 6)
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
                jugador.SinergiaUIActivadaActualizar(numeroArray, UIDesactivada, UIActivada, numPiezasActivada);

                break;

        }
    }
    void LogicaClaseGuardian(int numeroArray, Jugadores jugador, GameObject UIDesactivada, GameObject UIActivada, TextMeshProUGUI numPiezasDesactivada, TextMeshProUGUI numPiezasActivada)
    {
        switch (arrayClases[numeroArray])
        {
            case < 2:
                //Quitar las posibles booleanas
                if (arrayClases[numeroArray] > 0)
                {
                    jugador.SinergiaUIDesactivadaActualizar(numeroArray, UIDesactivada, numPiezasDesactivada);

                }
                else
                {
                    jugador.SinergiaUIDesactivadaFalse(numeroArray, UIDesactivada);
                }
                jugador.SinergiaUIActivadaFalse(numeroArray, UIActivada);
                break;
            case >= 2:
                if (arrayClases[numeroArray] >= 4)
                {
                    //Activar la booleana de 4 guardianes
                }
                else
                {
                    //Activar booleana de 2 guardianes
                }
                jugador.SinergiaUIActivadaActualizar(numeroArray, UIDesactivada, UIActivada, numPiezasActivada);

                break;
        }
    }
    void LogicaClaseMistico(int numeroArray, Jugadores jugador, GameObject UIDesactivada, GameObject UIActivada, TextMeshProUGUI numPiezasDesactivada, TextMeshProUGUI numPiezasActivada)
    {
        switch (arrayClases[numeroArray])
        {
            case < 2:
                //Quitar las posibles mejoras
                if (arrayClases[numeroArray] > 0)
                {
                    jugador.SinergiaUIDesactivadaActualizar(numeroArray, UIDesactivada, numPiezasDesactivada);

                }
                else
                {
                    jugador.SinergiaUIDesactivadaFalse(numeroArray, UIDesactivada);
                }
                jugador.SinergiaUIActivadaFalse(numeroArray, UIActivada);
                break;
            case >= 2:
                if (arrayClases[numeroArray] >= 4)
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
                jugador.SinergiaUIActivadaActualizar(numeroArray, UIDesactivada, UIActivada, numPiezasActivada);

                break;
        }
    }
    #endregion

    #region MetodosVacios
    public virtual void SinergiaUIDesactivadaActualizar(int posArray, GameObject UIDesactivada, TextMeshProUGUI numPiezasDesactivada)
    {
        
    }

    public virtual void SinergiaUIDesactivadaFalse(int posArray, GameObject UIDesactivada)
    {
        
    }

    public virtual void SinergiaUIActivadaFalse(int posArray, GameObject UIActivada)
    {
    }

    public virtual void SinergiaUIActivadaActualizar(int posArray, GameObject UIDesactivada, GameObject UIActivada, TextMeshProUGUI numPiezasActivada)
    {
    }

    #endregion
}
