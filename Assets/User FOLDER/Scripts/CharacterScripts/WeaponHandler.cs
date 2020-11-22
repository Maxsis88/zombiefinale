using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public GameObject rightHand;
    Animator anim;
    public GameObject pistol = null;
    public GameObject riffle = null;
    public GameObject pressE;

    public GameObject riffleImage;
    public GameObject pistolImage;

    //public AK.Wwise.Event shoot;
    private void Start()
    {
        anim = GetComponent<Animator>();
        pistolImage.SetActive(false);
        riffleImage.SetActive(false);
    }
    private void OnTriggerStay(Collider other)
    {


        if (other.CompareTag("Weapon"))
            pressE.SetActive(true);

        {
            if (Input.GetKey(KeyCode.E))
            {
                if (other != null)
                {

                    if (other.gameObject.GetComponent<RiffleFire>().type == "Riffle")
                    {
                        riffle = Instantiate(other.gameObject);
                        riffle.SetActive(false);
                        Destroy(other.gameObject);

                        riffle.transform.SetParent(rightHand.transform);
                        riffle.transform.position = rightHand.transform.position;
                        riffle.transform.rotation = rightHand.transform.rotation;
                        pressE.SetActive(false);
                        riffleImage.SetActive(true);
                    }
                    else if (other.gameObject.GetComponent<RiffleFire>().type == "Pistol")
                    {
                        pistol = Instantiate(other.gameObject);
                        pistol.SetActive(false);
                        Destroy(other.gameObject);

                        pistol.transform.SetParent(rightHand.transform);
                        pistol.transform.position = rightHand.transform.position;
                        pistol.transform.rotation = rightHand.transform.rotation;
                        pressE.SetActive(false);
                        pistolImage.SetActive(true);
                    }
                }
            }

        }


    }
    private void OnTriggerExit(Collider other)
    {
        pressE.SetActive(false);
    }


    private void Update()
    {
        TakeRiffle();
        TakePistol();
        ShootController();
    }

    void TakeRiffle()

    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (riffle != null)
            {
                if (riffle.activeSelf == false)
                {
                    if (pistol != null)
                    {
                        pistol.SetActive(false);
                    }
                    riffle.GetComponent<Collider>().enabled = false;
                    riffle.SetActive(true);
                    anim.SetBool("Riffle", true);
                }

                else
                {
                    riffle.SetActive(false);
                    anim.SetBool("Shoot", false);
                    anim.SetBool("Riffle", false);
                }

            }
        }
    }

    public void TakePistol()
    {

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (pistol != null)
            {
                if (pistol.activeSelf == false)
                {
                    if (riffle != null)
                    {
                        riffle.SetActive(false);
                    }
                    pistol.GetComponent<Collider>().enabled = false;
                    pistol.SetActive(true);
                    anim.SetBool("Pistol", true);
                }

                else
                {
                    pistol.SetActive(false);
                    anim.SetBool("Shoot", false);
                    anim.SetBool("Pistol", false);
                }

            }

        }

    }
    void ShootController()
    {
        if (Input.GetMouseButton(0))
        {
            if (pistol != null)
            {
                if (pistol.activeSelf == true)
                {
                    if (Input.GetMouseButtonDown(0)) pistol.SendMessage("Fire");
                }
            }

            if (riffle != null)
            {
                if (riffle.activeSelf == true)
                {
                    riffle.SendMessage("Fire");
                }
            }
           // shoot.Post(gameObject);
        }
        
    }

    private void OnDrawGizmos()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2, 0));
        Debug.DrawRay(ray.origin, ray.direction * 20, Color.yellow);
    }
}


