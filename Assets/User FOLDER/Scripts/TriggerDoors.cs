using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoors : MonoBehaviour
{

    public string nametrigger;
    public string exittrigger;
    public GameObject obj;
    public AK.Wwise.Event doorsound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            obj.GetComponent<Animator>().SetTrigger(nametrigger);
            doorsound.Post(gameObject);
        }

    }
    void OnTriggerExit (Collider col)
    {
        if (col.tag == "Player")
        {
            obj.GetComponent<Animator>().SetTrigger(exittrigger);
        }

    }
}
