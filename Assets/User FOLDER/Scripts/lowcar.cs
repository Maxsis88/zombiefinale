using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lowcar : MonoBehaviour
{
    public Animation anim;
    // Start is called before the first frame update
    private void Start()
    {
        //GetComponent<Animation>().Play("LowCarr");
        anim.Play();
    }
}
