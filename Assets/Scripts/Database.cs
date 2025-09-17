using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Database : MonoBehaviour
{
    public CharacterDatabaseSO database;

    
    public List<CharactersDataSO> poolPersonajesCoste1 = new();
    public List<CharactersDataSO> poolPersonajesCoste2 = new();
    public List<CharactersDataSO> poolPersonajesCoste3 = new();
    public List<CharactersDataSO> poolPersonajesCoste4 = new();
    public List<CharactersDataSO> poolPersonajesCoste5 = new();
    //ARRAY DE LISTAS, VALOREMOS CREAR OTRA CLASE
    public List<CharactersDataSO>[] arrayListas = new List<CharactersDataSO>[5];   

    public static Database instance;

    private void Awake()
    {
        instance = this;
        CrearPoolPersonajes();
        CrearArrayLista();
    }

    void Update()
    {
        // Pulsa la tecla P para hacer un pantallazo
        if (Input.GetKeyDown(KeyCode.P))
        {
            TakeScreenshot();
        }
    }

    void TakeScreenshot()
    {
        // Obtiene la ruta del escritorio del usuario
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        // Nombre del archivo con fecha y hora
        string fileName = "UnityScreenshot_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";

        // Ruta completa
        string fullPath = Path.Combine(desktopPath, fileName);

        // Captura la pantalla
        ScreenCapture.CaptureScreenshot(fullPath);

        Debug.Log("Pantallazo guardado en: " + fullPath);
    }
    public void CrearArrayLista()
    {
        arrayListas[0] = poolPersonajesCoste1;
        arrayListas[1] = poolPersonajesCoste2;
        arrayListas[2] = poolPersonajesCoste3;
        arrayListas[3] = poolPersonajesCoste4;
        arrayListas[4] = poolPersonajesCoste5;
    }
    public void CrearPoolPersonajes()
    {
        foreach (CharactersDataSO personaje in database.charactersData)
        {
            switch (personaje.Coste)
            {
                case 1:
                    for (int i = 0; i < 27; i++)
                    {
                        poolPersonajesCoste1.Add(personaje);
                    }                     
                    break;
                case 2:
                    for(int i = 0;i < 22; i++) 
                    {
                        poolPersonajesCoste2.Add(personaje);
                    }
                    break;
                case 3:
                    for(int i = 0; i < 18; i++)
                    {
                        poolPersonajesCoste3.Add(personaje);
                    }
                    break;
                case 4:
                    for(int i = 0; i< 12; i++)
                    {
                        poolPersonajesCoste4.Add(personaje);
                    }
                    break;
                case 5:
                    for(int i= 0; i< 10; i++)
                    {
                        poolPersonajesCoste5.Add(personaje);
                    }
                    break;
                default:
                    Debug.LogError("El valor del personaje está fuera de lo esperado");
                    break;
            }
        }
    }
    public void AñadirAlPool(CharactersDataSO pokeData, int nivelEstrellas)
    {            
        //EL DINERO EN EL OTRO LADO MEJOR
       
        if(nivelEstrellas == 0)        // POKE 1 ESTRELLA
        {
            arrayListas[pokeData.Coste -1].Add(pokeData);
            
        }
        else if(nivelEstrellas == 1)   // POKE 2 ESTRELLAS
        {
            arrayListas[pokeData.Coste - 1].Add(pokeData);
            arrayListas[pokeData.Coste - 1].Add(pokeData);
            arrayListas[pokeData.Coste - 1].Add(pokeData);
            
        }
        else
        {
            for(int i = 0; i < 9; i++)  // POKE 3 ESTRELLAS
            {
                arrayListas[pokeData.Coste - 1].Add(pokeData);
                
            }
        }
    }
    // ---------------------------- LOGICA TIENDA ----------------------------- // 

    public void CrearTienda(Player player)
    {

        for (int i = 0; i < player.listaButtons.Count; i++)
        {
            SwitchPool(i, player);
        }

    }

    private void SwitchPool(int i, Player player)
    {
        switch (player.nivel)
        {
            case 1:

                SeleccionarPersonajePool(poolPersonajesCoste1.Count, poolPersonajesCoste1, i, player);

                break;

            case 2:
                SeleccionarPersonajePool(poolPersonajesCoste1.Count, poolPersonajesCoste1, i, player);

                break;

            case 3:
                int selectorCosteTres = UnityEngine.Random.Range(0, 100);
                if (selectorCosteTres < 65)
                {
                    SeleccionarPersonajePool(poolPersonajesCoste1.Count, poolPersonajesCoste1, i, player);

                }
                else
                {
                    SeleccionarPersonajePool(poolPersonajesCoste2.Count, poolPersonajesCoste2, i, player);
                }
                break;

            case 4:
                int selectorCosteCuatro = UnityEngine.Random.Range(0, 100);

                if (selectorCosteCuatro < 55)
                {
                    SeleccionarPersonajePool(poolPersonajesCoste1.Count,poolPersonajesCoste1, i, player);
                }
                else if (selectorCosteCuatro > 54 && selectorCosteCuatro < 85)
                {
                    SeleccionarPersonajePool(poolPersonajesCoste2.Count, poolPersonajesCoste2, i, player);
                }
                else
                {
                    SeleccionarPersonajePool(poolPersonajesCoste3.Count, poolPersonajesCoste3, i, player);
                }

                break;
            case 5:
                int selectorCosteCinco = UnityEngine.Random.Range(0, 100);
                if (selectorCosteCinco < 45)
                {
                    SeleccionarPersonajePool(poolPersonajesCoste2.Count, poolPersonajesCoste2, i, player);
                }
                else if (selectorCosteCinco > 44 && selectorCosteCinco < 77)
                {
                    SeleccionarPersonajePool(poolPersonajesCoste2.Count, poolPersonajesCoste2, i, player);
                }
                else if (selectorCosteCinco > 76 && selectorCosteCinco < 98)
                {
                    SeleccionarPersonajePool(poolPersonajesCoste3.Count, poolPersonajesCoste3, i, player);
                }
                else
                {
                    SeleccionarPersonajePool(poolPersonajesCoste4.Count, poolPersonajesCoste4, i, player);
                }
                break;
            case 6:
                int selectorCosteSeis = UnityEngine.Random.Range(0, 100);
                if (selectorCosteSeis < 31)
                {
                    SeleccionarPersonajePool(poolPersonajesCoste1.Count, poolPersonajesCoste1, i, player);
                }
                else if (selectorCosteSeis > 30 && selectorCosteSeis < 71)
                {
                    SeleccionarPersonajePool(poolPersonajesCoste2.Count, poolPersonajesCoste2, i, player);
                }
                else if (selectorCosteSeis > 70 && selectorCosteSeis < 96)
                {
                    SeleccionarPersonajePool(poolPersonajesCoste3.Count, poolPersonajesCoste3, i, player);
                }
                else
                {
                    SeleccionarPersonajePool(poolPersonajesCoste4.Count, poolPersonajesCoste4, i, player);

                }
                break;
                // FALTAN LOS NIVELES 7-9 O 10 VAMOS, QUE ME DABA PEREZA


        }

    }
    private void SeleccionarPersonajePool(int numLimite, List<CharactersDataSO> data, int numFor, Player player)
    {
        int numPieza = UnityEngine.Random.Range(0, numLimite);
        if (numLimite == 0)
        {
            SwitchPool(numFor, player);
            return;
        }
        player.personajesTienda.Add(data[numPieza]);
        data.Remove(data[numPieza]);
        player.listaButtons[numFor].interactable = true;
        player.listaButtons[numFor].GetComponent<Image>().sprite = player.personajesTienda[numFor].spriteTienda;
    }
}
