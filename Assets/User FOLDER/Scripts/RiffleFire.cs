using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiffleFire : MonoBehaviour
{
    public Animator animatorOfCharacter;
    public float animationChangeTime;
    public GameObject fireHandler;
    public string type ;
    public AK.Wwise.Event shoot;
    public AK.Wwise.Event tale;

    float time = 0;
    [SerializeField]
    float waitTime = .05f;
    public int damage;
    public GameObject aim;
    

    public void Fire()
    {
        if (aim.activeSelf == true)
        {
            if (time <= 0)
            { 
                time = waitTime;
                shoot.Post(gameObject);
                
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2, 0));
                if (Physics.Raycast(ray.origin, ray.direction, out hit, 20f))
                {
                    if (hit.collider.CompareTag("Enemy"))
                    {
                        hit.transform.GetComponent<MuovingStates>().OnTakeDamage(damage);
                        
                    }
                }
            }
            
        }
       
    }



    private void Update()
    {
        //if (gameObject.activeSelf == false || (Input.GetMouseButtonUp(0)))
        //{
        //    animatorOfCharacter.SetBool("Shoot", false);
        //    aim.SetActive(false);
        //}
    }

    private void FixedUpdate()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
            
        }

       if(aim.activeSelf == true)
        {
            if(Input.GetMouseButtonUp(0))
            {
                    tale.Post(gameObject);
            }
        }
        
       
    }

}
