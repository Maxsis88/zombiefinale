using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    public GameObject rightHand;
    public GameObject lefttHand;
    void On()
    {
        rightHand.SetActive(true);
        lefttHand.SetActive(true);
    }

    void Off()
    {
        rightHand.SetActive(false);
        lefttHand.SetActive(false);
    }
}
