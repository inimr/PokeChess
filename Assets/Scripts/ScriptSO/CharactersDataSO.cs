using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



    [CreateAssetMenu]
    [Serializable]
public class CharactersDataSO : ScriptableObject
 {
    [Header("Estadísticas de la pieza")]
    public string Name;
    public int ID;
    public GameObject Prefab1Estrella;
    public GameObject Prefab2Estrellas;
    public GameObject Prefab3Estrellas;
    public Sprite spriteTienda;
    public int Coste;
    public int HP;
    public int Ataque;
    public int Defensa;
    public int AtaqueEspecial;
    public int DefensaEspecial;
    public float VelocidadAtq;
    public bool isRanged;
    public int Rango; // No se si sera bool, creamos otro y ya
    public int ManaMax;
    public int startingMana;
    public bool usaMana;
   

        
    public _Tipo Tipo;
    public _Rol Rol;
    [Header("Cosas a añadir al panel UI")]
    public Sprite icono1estrella;
    public Sprite icono2estrellas;
    public Sprite icono3estrellas;
    public string Name2Estrellas;
    public string Name3Estrellas;
    public Sprite iconoTipo;
    public Sprite iconoRol;
    public string descripcionHabilidad;
    public int numerosHab1Estrella;
    public int numerosHab2Estrella;
    public int numerosHab3Estrella;
    

    public enum _Rol
    {
        Tanque,
        Luchador,
        Cazador,
        Lanzahabilidades,
        Guardian,
        Mistico
    }
    public enum _Tipo
    {
        Acero,
        Hielo,
        Fantasma,
        Electrico,
        Volador,
        Fuego,
        Dragon

    }

        // TEST DE COMO PODRIAMOS DETECTAR LOS ENUM COMO ARRAY
        /* void kk()

         {
             int[] arr=new int[3];
             _Rol rol= _Rol.Low;
             int low = 0;

             arr[(int)rol]++;
             if (rol == 0)
             {
                 low++;
             }

             string a = string.Format("{0}", rol);
         }*/
    }

