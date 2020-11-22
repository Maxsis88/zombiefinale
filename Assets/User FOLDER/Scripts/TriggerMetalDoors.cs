using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMetalDoors : MonoBehaviour
{
    public string nametrigger;
    public GameObject obj;
    public GameObject objj;

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
        }
        if (col.tag == "Player")
        {
            objj.GetComponent<Animator>().SetTrigger(nametrigger);
        }

    }
    
   
}
