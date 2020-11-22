using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolFire : MonoBehaviour
{
    public Animator animatorOfCharacter;
    public float animationChangeTime;
    public GameObject fireHandler;
    public string type = "Pistol";

    private void Start()
    {

    }
    public void Fire()
    {
        if(gameObject.activeSelf == false)
        {
            animatorOfCharacter.SetBool("Shoot Pistol", false);

        }
        if (Input.GetMouseButtonDown(0))
        {
            animationChangeTime = 1f;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 20f))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    hit.transform.SendMessage("OnTakeDamage");
                }
            }
        }
    }

    private void Update()
    {

        if (animationChangeTime > 0)
        {
            animationChangeTime -= Time.deltaTime;
            animatorOfCharacter.SetBool("Shoot Pistol", true);
        }
        if (gameObject.activeSelf == false)
        {
            animatorOfCharacter.SetBool("Shoot Pistol", false);
        }
        if ( animationChangeTime <= 0)
        {
            animatorOfCharacter.SetBool("Shoot Pistol", false);
        }


    }

    private void OnDrawGizmos()
    {
        if (Input.GetMouseButton(0))
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(fireHandler.transform.position, fireHandler.transform.forward * 10);
        }
    }

}
