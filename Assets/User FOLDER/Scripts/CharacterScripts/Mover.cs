using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    Animator animator;
    CharacterController controller;
    [SerializeField]
    float gravityForce;
    [SerializeField]
    float speedWalk = 2;
    [SerializeField]
    float speedRun = 5;
    [SerializeField]
    float speedSprint = 7;
    public float speedRotation = 100;
    
    public float currentSpeed;
    float jumpPower = 10;
    [SerializeField]
    float currentSpeedRight;
    Vector3 dritto;
    Vector3 destro;
    public bool isGrounded;
    public GameObject aim;
    public GameObject healthbar;
    public float health = 100;
    bool isAlive;
    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
        animator = gameObject.GetComponent<Animator>();
        controller = gameObject.GetComponent<CharacterController>();

           }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (health > 0) isAlive = true;
        else isAlive = false;

        if(isAlive == true) CharacterMover();


        GamingGravity();
        AnimationController();
        healthbar.transform.localScale = new Vector3(health / 100, 1, 1);


    }

    void GamingGravity() // метод гравитации и прыжка || если двигать персонажа после просчета гравитации то его земля всё время толкает назад и сбивается isGrounded
    {
        controller.Move(transform.up * gravityForce * Time.deltaTime);

        if (controller.isGrounded)
        {
            gravityForce = -1;
            isGrounded = true;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                gravityForce = jumpPower;
                isGrounded = false;

            }
        }
        if (controller.isGrounded == false)
        {
            
            gravityForce -= 20 * Time.deltaTime;
        }

    }




    void CharacterMover()
    {
        //вращение персонажа
        if (Input.GetAxis("Mouse X") != 0)
        {
            transform.Rotate(transform.up * Input.GetAxis("Mouse X") * speedRotation * Time.deltaTime);
        }

        //проверка режима передвижения персонажа
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            currentSpeed = speedWalk;

        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = speedSprint;
        }
        else
        {
            currentSpeed = speedRun;
        }



        // перемещение персонажа прямо
        if (Input.GetAxis("Vertical") != 0)
        {
            dritto = (transform.forward * Input.GetAxis("Vertical") * currentSpeed * Time.deltaTime);
        }
        else
        {
            currentSpeed = 0;
            dritto = Vector3.zero;
        }

       


        //проверка режима передвижения персонажа
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            currentSpeedRight = speedWalk;
        }
        else
        {
            currentSpeedRight = speedRun;
        }

       
        // персонаж ходит в бок только на земле и не в спринте
        if (isGrounded)
        {

            if (Input.GetAxis("Horizontal") != 0)
            {
                currentSpeedRight *= Input.GetAxis("Horizontal");
                destro = (transform.right * currentSpeedRight * Time.deltaTime);
            }
            else
            {
                currentSpeedRight = 0;
                destro = Vector3.zero;
            }

            if (currentSpeed >= 6)
            {
                currentSpeedRight = 0;
                destro = Vector3.zero;
            }

        }
        controller.Move(dritto + destro);
    }

    void AnimationController()
    {
        if (isAlive)
        {

            if (isGrounded)
            {
                animator.SetBool("IsGrounded", true);
            }
            else
            {
                animator.SetBool("IsGrounded", false);
            }

            if ((Input.GetAxis("Vertical") == 0 && (Input.GetAxis("Horizontal") == 0)))
            {
                animator.SetBool("Stay", true);
            }
            else animator.SetBool("Stay", false);

            animator.SetFloat("CurrentSpeed", currentSpeed * (Input.GetAxis("Vertical")));
            animator.SetFloat("CurrentSpeedRight", currentSpeedRight);
        
        if (Input.GetMouseButtonDown(1) && (animator.GetBool("Riffle") == true || animator.GetBool("Pistol") == true) && aim.activeSelf == false)
        {
            animator.SetBool("Shoot", true);
            aim.SetActive(true);
        }
        else if (Input.GetMouseButtonDown(1) && (animator.GetBool("Riffle") == true || animator.GetBool("Pistol") == true) && aim.activeSelf == true)
        {
            animator.SetBool("Shoot", false);
            aim.SetActive(false);
        }
    }
        else
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Dead")) animator.SetTrigger("Dead");
        }
    }

   public void OnTakeDamage(float damage)
{
    if (health > 0) health -= damage;
    else health = 0;
}
}

    