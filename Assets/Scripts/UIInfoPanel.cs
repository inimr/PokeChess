using UnityEngine;

public class UIInfoPanel : MonoBehaviour
{

    private Camera camara;
    private RectTransform rectTrans;


    // Start is called before the first frame update
    void Start()
    {
        camara = Camera.main;   
        rectTrans = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.mousePosition.y > Screen.height / 2)
        {
            if(Input.mousePosition.x > Screen.width / 2) // MOUSE ARRIBA-DER
            {
                rectTrans.anchorMin = new Vector2(1, 1);
                rectTrans.anchorMax = new Vector2(1, 1);
            }
            else                                        //MOUSE ARRIBA-IZQ
            {
                rectTrans.anchorMin = new Vector2(0, 1);
                rectTrans.anchorMax = new Vector2(0, 1);
            }
        }
        else                                            // MOUSE ABAJO-DER
        {
            if(Input.mousePosition.x > Screen.width / 2)
            {
                rectTrans.anchorMin = new Vector2(1, 0);
                rectTrans.anchorMax = new Vector2(1, 0);
            }
            else                                        // MOUSE ABAJO-IZQ
            {
                rectTrans.anchorMin = new Vector2(0, 0);
                rectTrans.anchorMax = new Vector2(0, 0);
            }
        }

        transform.position = Input.mousePosition;
        
    }
}
