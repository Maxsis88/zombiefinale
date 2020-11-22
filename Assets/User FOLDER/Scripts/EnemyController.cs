using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyController : MonoBehaviour
{
    NavMeshAgent agent;
    Rigidbody rb ;
    Animator anim;
    public Transform player;
    Vector3 currentPosition;
    [SerializeField]
    Vector3 targetPosition;
    public string state;
    public GameObject test;
    Vector3 centr;
    [SerializeField]
    float waitTime = 0;
    [SerializeField]
    float velocity;
    [SerializeField]
    float angleVelocity;
    float nextAngelVelocity;
    float previusAngelVelocity;
    float runVelocity = 5;
    float walkVelocity = 3.5f;
    FieldOfWiev fow;
    void Start()
    {

        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        centr = test.transform.position;
        currentPosition = transform.position;
        state = "Exploration";
        targetPosition = transform.position;
        agent = gameObject.GetComponent<NavMeshAgent>();
        StartCoroutine(FindPath());
        StartCoroutine(PlayerDetect());

        targetPosition.x = centr.x + Random.Range(-10, 10);
        targetPosition.z = centr.z + Random.Range(-10, 10);
        targetPosition.y = centr.y;
        agent.SetDestination(targetPosition);
    }

    IEnumerator PlayerDetect()
    {
        while (true)
        {
            if (player != null)
                break;
            if (Vector3.Distance(transform.position, player.position) < 1)
            {
                // animator.SetBool("Attak", true);
                //player.SendMessage("TakeDamage");
            }
            yield return new WaitForSeconds(.3f);
        }
    }

    IEnumerator FindPath()
    {
        while (true)
        {
            if (player != null)
            {
                if (Vector3.Distance(player.position, transform.position) <= 15 && (!Physics.Linecast(player.position, transform.position)))    //Vector3.Distance(player.position, transform.position) <= 15  && (!Physics.Linecast(player.position, transform.position)
                {
                    Vector3.RotateTowards(transform.position, player.position, 0, 0);
                        agent.speed = runVelocity;
                        agent.SetDestination(player.position);
                }
                else
                {
                    agent.speed = walkVelocity;
                    agent.SetDestination(targetPosition);

                    if ((transform.position.x - targetPosition.x) <= 2f && (transform.position.z - targetPosition.z) <= 2f)
                    {
                        if ((transform.position.x - targetPosition.x) >= -2f && (transform.position.z - targetPosition.z) >= -2f)
                        {
                            agent.isStopped = true;
                            targetPosition.x = centr.x + Random.Range(-10, 10);
                            targetPosition.z = centr.z + Random.Range(-10, 10);
                            targetPosition.y = centr.y;
                            waitTime = 3;
                            agent.SetDestination(targetPosition);
                        }
                    }
                }
               
                yield return new WaitForSeconds(0.3f);
            }
            else break;
        }
    }
           
    void IncrementTime()
    {
        if (waitTime > 0) waitTime -= Time.deltaTime;

        if (waitTime <= 0)
        {
            agent.isStopped = false;
        }
    }
    private void OnDrawGizmos()
    {
       // Gizmos.color = new Color(1,0,0,0.5f);
       // Gizmos.DrawSphere(centr, 10);
       // Gizmos.DrawLine(player.position, transform.position);
    }
    void Update()
    {
        IncrementTime();
        velocity = agent.velocity.magnitude;
        nextAngelVelocity = transform.eulerAngles.magnitude;
        angleVelocity = nextAngelVelocity - previusAngelVelocity;
        previusAngelVelocity = nextAngelVelocity;
        AnimatorZombie();


    }

    public void OnTakeDamage()
    {
        Destroy(gameObject);
    }
    void AnimatorZombie()
    {
        if (velocity > 0) anim.SetBool("Walk", true);
        else anim.SetBool("Walk", false);

        anim.SetFloat("Angular Speed", angleVelocity);
        anim.SetFloat("Speed", velocity);

        if (Vector3.Distance(player.position, transform.position) <= 15)
        {
            anim.SetBool("Detected", true);
        }
        else anim.SetBool("Detected", false);

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Detected"))
        {
            agent.isStopped = true;
        }
       

    }









}
