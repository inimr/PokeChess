using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnim : MonoBehaviour
{

    Animator animator;
    AnimatorStateInfo animStateInfo;
    private bool animEnded;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            StartCoroutine(TEST());
            

        }
    }

    IEnumerator TEST()
    {
        animator.SetTrigger("Muerto");
        yield return null;
        animStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        
        while(!animEnded) 
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, Time.deltaTime*2);
            print("esperando");
            yield return null;
            
        }
        print("While terminado");
        transform.localScale = Vector3.one * 2;
        animEnded = false;
    }

    public void DetectarFinAnim()
    {
        animEnded = true;
    }
}
