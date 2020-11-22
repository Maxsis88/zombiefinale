using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterMuover : MonoBehaviour
{
    Animator anim;
    CharacterController chController;
    float gravityForce = -1;
    [SerializeField]
    float speedWalk = 1;
    [SerializeField]
    float speedRun = 3;
    [SerializeField]
    float speedSprint = 5;
    [SerializeField]
    float currentSpeed;
    float speedRotation = 100;
    float jumpPower = 10;
    Vector3 moveVector;
   
   
    // Start is called before the first frame update
    void Start()
    {
       
        chController = gameObject.GetComponent<CharacterController>();
        anim = gameObject.GetComponent<Animator>();

    }

    
    // Update is called once per frame
    void Update()
    {
        GamingGravity();
        CharacterMover();       
        AnimationController();
        if(chController.isGrounded)
        {
            Debug.Log("QWEQWEQWE");
        }

    }

   

    void GamingGravity()
    {
        if (!chController.isGrounded) gravityForce -= 20 * Time.deltaTime;
        else gravityForce = -5;

       
    }

   
    void CharacterMover() 
    {
        // вращение персонажа мышкой
        if (Input.GetAxis("Mouse X") != 0)
        {
            transform.Rotate(transform.up * Input.GetAxis("Mouse X") * speedRotation * Time.deltaTime);
        }

        //ходьба

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

        // контроллеры ходьбы

       
        float currentSpeedRight;



     if (Input.GetAxis("Vertical") != 0) chController.Move(transform.forward * Input.GetAxis("Vertical") * currentSpeed * Time.deltaTime);

     if (Input.GetAxis("Horizontal") != 0)
            {
                chController.Move(transform.right * Input.GetAxis("Horizontal") * currentSpeed * Time.deltaTime);
                currentSpeedRight = currentSpeed * Input.GetAxis("Horizontal");
            }
            else currentSpeedRight = 0;

      anim.SetFloat("CurrentSpeed", currentSpeed * (Input.GetAxis("Vertical")));
                anim.SetFloat("CurrentSpeedRight", currentSpeedRight);       

        // прыжки
        chController.Move(transform.up * gravityForce * Time.deltaTime);



        if (chController.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            gravityForce = jumpPower;
            Debug.Log("Прыжек");
        }

        


    }

    void AnimationController()
    {
        if (currentSpeed == speedSprint)
            
        if (chController.isGrounded) anim.SetBool("IsGrounded", true);
        if (!chController.isGrounded) anim.SetBool("IsGrounded", false);

        if ((Input.GetAxis("Vertical") == 0 && (Input.GetAxis("Horizontal") == 0)))
        {
            anim.SetBool("Stay", true);
        }
        else anim.SetBool("Stay", false);

        if (anim.GetBool("Stay") == false)
        {
           
            anim.SetFloat("SpeedSide", (Input.GetAxis("Horizontal")));            
        }            

    }

    void CharacterFight()
    {

    }
}

