using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiePunch : MonoBehaviour
{
    public AK.Wwise.Event hit;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Mover>().OnTakeDamage(5);
            hit.Post(gameObject);
        }

    }
}
