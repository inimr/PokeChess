using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorProyectil : MonoBehaviour
{
    public ControladorPoke pokeAtacante;
    public Transform target;
    public float speed = 5f;


    private void Update()
    {

        if(target != null)
        {
            Vector3 objetivo = new Vector3(target.position.x, target.position.y + 0.5f, target.position.z);
            
            transform.position = Vector3.MoveTowards(transform.position,objetivo, speed * Time.deltaTime);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == target.gameObject)
        {
            pokeAtacante.AtaqueBasico();
            Destroy(gameObject);
        }
    }
}
