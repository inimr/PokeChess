using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnseñarInfoPoke : MonoBehaviour
{
    private Personaje poke;
    private bool isHovering;
    private bool isShowing;
    private float tiempoMinimo;

    
    public GameObject canvasInfo;
    private string colorActivo = "#FFFFFF"; /// COLOR BLANCO
    private string colorDesactivo = "#808080"; /// COLOR GRIS

    /// -------------------- ZONA DEL TITULO --------------------///
    Transform zonaTitulo;
    Image iconoPoke;
    Transform cosasZonaTitulo;
    TextMeshProUGUI textoNombre;
    Image iconoTipo;
    Image iconoRol;

    /// -------------------- ZONA DEL CUERPO --------------------///
    Transform zonaCuerpo;
    TextMeshProUGUI descripcionHabilidad;
    TextMeshProUGUI numerosHabilidad;

    /// -------------------- ZONA DE SLIDERS --------------------///

    Transform zonaSliders;
    Slider sliderVida;
    Slider sliderMana;
    TextMeshProUGUI textMaxHP;
    TextMeshProUGUI textActualHP;
    TextMeshProUGUI textMaxMana;
    TextMeshProUGUI textActualMana;

    /// -------------------- ZONA DE STATS --------------------///
    Transform zonaBottom;
    TextMeshProUGUI textAtaque;
    TextMeshProUGUI textSpAtaque;    
    TextMeshProUGUI textDef;
    TextMeshProUGUI textSpDef;    
    TextMeshProUGUI textVelAtaque;
    TextMeshProUGUI textRango;

    // Start is called before the first frame update
    void Start()
    {
        poke = GetComponent<Personaje>();
      
    }

    private void RecogerInfoCanvas()
    {
        /// -------------------- ZONA DEL TITULO --------------------///
        zonaTitulo = canvasInfo.transform.GetChild(0);
        iconoPoke = zonaTitulo.GetChild(0).GetComponent<Image>(); /// ICONO POKE
        cosasZonaTitulo = zonaTitulo.GetChild(1);
        textoNombre = cosasZonaTitulo.GetChild(0).GetComponent<TextMeshProUGUI>(); /// NOMBRE POKE
        iconoTipo = cosasZonaTitulo.GetChild(1).GetComponent<Image>(); /// ICONO TIPO
        iconoRol = cosasZonaTitulo.GetChild(2).GetComponent<Image>(); /// ICONO ROL

        /// -------------------- ZONA DEL CUERPO --------------------///
        zonaCuerpo = canvasInfo.transform.GetChild(1);
        descripcionHabilidad = zonaCuerpo.GetChild(0).GetComponent<TextMeshProUGUI>();
        numerosHabilidad = zonaCuerpo.GetChild(1).GetComponent<TextMeshProUGUI>();

        /// -------------------- ZONA DE SLIDERS --------------------///

        zonaSliders = canvasInfo.transform.GetChild(2);
        sliderVida = zonaSliders.GetChild(0).transform.GetChild(0).GetComponent<Slider>();        
        Transform textosSliderVida = zonaSliders.GetChild(0).transform.GetChild(1);
        textActualHP = textosSliderVida.GetChild(0).GetComponent<TextMeshProUGUI>();
        textMaxHP = textosSliderVida.GetChild(2).GetComponent<TextMeshProUGUI>();
        sliderMana = zonaSliders.GetChild(1).transform.GetChild(0).GetComponent<Slider>();
        Transform textosSliderMana = zonaSliders.GetChild(1).transform.GetChild(1);
        textActualMana = textosSliderMana.GetChild(0).GetComponent<TextMeshProUGUI>();
        textMaxMana = textosSliderMana.GetChild(2).GetComponent<TextMeshProUGUI>();

        /// -------------------- ZONA DE STATS --------------------///
        zonaBottom = canvasInfo.transform.GetChild(3);
        textAtaque = zonaBottom.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        textSpAtaque = zonaBottom.GetChild(0).transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        textDef = zonaBottom.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        textSpDef = zonaBottom.GetChild(1).transform.GetChild(1).GetComponent<TextMeshProUGUI>();        
        textVelAtaque = zonaBottom.GetChild(2).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        textRango = zonaBottom.GetChild(2).transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        
       

    }

    // NO SE SI ES MEJOR QUE COJAN EL VALOR ACTUAL O EL MAXIMO EN LAS STATS LA VERDAD
    private void ModificarCanvasUI()
    {
        switch (poke.nivelEstrellas)
        {
            case 0:
                iconoPoke.sprite = poke.data.icono1estrella;
                textoNombre.text = poke.data.Name;
                numerosHabilidad.SetText($"<color={colorActivo}>{poke.data.numerosHab1Estrella}</color>" + " / " + $"<color={colorDesactivo}>{poke.data.numerosHab2Estrella}</color>" + " / " + $"<color={colorDesactivo}>{poke.data.numerosHab3Estrella}</color>");
                break;
            case 1:
                iconoPoke.sprite = poke.data.icono2estrellas;
                textoNombre.text = poke.data.Name2Estrellas;
                numerosHabilidad.SetText($"<color={colorDesactivo}>{poke.data.numerosHab1Estrella}</color>" + " / " + $"<color={colorActivo}>{poke.data.numerosHab2Estrella}</color>" + " / " + $"<color={colorDesactivo}>{poke.data.numerosHab3Estrella}</color>");
                break;
            case 2:
                iconoPoke.sprite = poke.data.icono3estrellas;
                textoNombre.text = poke.data.Name3Estrellas;
                numerosHabilidad.SetText($"<color={colorDesactivo}>{poke.data.numerosHab1Estrella}</color>" + " / " + $"<color={colorDesactivo}>{poke.data.numerosHab2Estrella}</color>" + " / " + $"<color={colorActivo}>{poke.data.numerosHab3Estrella}</color>");
                break;
        }
        iconoTipo.sprite = poke.data.iconoTipo;
        iconoRol.sprite = poke.data.iconoRol;
        descripcionHabilidad.text = poke.data.descripcionHabilidad;
       
        
        textAtaque.text = "Atk: " + poke.atkMax.ToString();
        textDef.text = "Def: " + poke.defMax.ToString();
        textSpAtaque.text = "Sp.A: " + poke.spAMax.ToString();
        textSpDef.text = "Sp.D: " + poke.spDMax.ToString();
        textVelAtaque.text = "Vel.A: " + poke.atkSpeedMax.ToString();
        textMaxHP.text = poke.vidaMax.ToString();
        textActualHP.text = poke.vidaActual.ToString();
        
     
                
        textActualMana.text = poke.manaActual.ToString();
        textMaxMana.text = poke.manaMax.ToString();
        
        textRango.text = "Rango: " + poke.data.Rango.ToString();

        if (!poke.data.usaMana)
        {
            zonaSliders.GetChild(1).gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (isHovering && !isShowing)
        {
            tiempoMinimo += Time.deltaTime;
            if(tiempoMinimo > 3f)
            {
                if (poke.jugador == null) return;
                poke.jugador.panelInfoPoke.SetActive(true);
                canvasInfo = poke.jugador.panelInfoPoke; //NO SE COMO FUNCIONARA EN EL MULTIJUGADOR
                if(zonaTitulo == null)
                {
                    RecogerInfoCanvas();
                }                
                ModificarCanvasUI();
                canvasInfo.SetActive(true);
                isShowing = true;
            }
        }
    }

    private void OnMouseEnter()
    {
        isHovering = true;
    }

    private void OnMouseExit()
    {
        isHovering = false;
        isShowing = false;
        tiempoMinimo = 0;
        if(canvasInfo != null) canvasInfo.SetActive(false);

    }

    private void ActualizarInfo()
    {

    }
}
