using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MuovingStates : MonoBehaviour
{
    Animator anim;
    NavMeshAgent agent;
    NavMeshPath path;
    public GameObject zombieLand;
    Vector3 centr;
    Vector3 targetPosition;

    public int health = 100;
    
    public GameObject player;
    float waitTime;
    
    PlayerState state;
    AnimationState animat;
    LayerMask zombieLayerMask = 8;

    public AK.Wwise.Event roarEvent;

    enum PlayerState 
    {
        chill,
        hunt
    }

    enum AnimationState 
    {
        Idle = 0,
        Walk,
        Run,
        Attack,
        Damage,
        Death
    }
        


              
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        centr = zombieLand.transform.position;
        path = new NavMeshPath();

        Destination();
        agent.updatePosition = false;
        animat = AnimationState.Idle;

    }


    void Update()
    {
        switch (animat)
        {
            case AnimationState.Idle:
                anim.SetInteger("State", 0);
                break;
            case AnimationState.Walk:
                anim.SetInteger("State", 1);
                break;
            case AnimationState.Run:
                anim.SetInteger("State", 2);
                break;
            case AnimationState.Attack:
                anim.SetInteger("State", 3);
                break;
            case AnimationState.Damage:
                anim.SetInteger("State", 4);
                break;
            case AnimationState.Death:
                anim.SetInteger("State", 5);
                break;
        }

        if (health > 0)
        {
            var dis = Vector3.Distance(transform.position, player.transform.position);
            // проверка необходимого состояния персонажа
            Debug.DrawRay(transform.position + Vector3.up , (player.transform.position - transform.position));




            // написать вместо ифов свитч кейс
            // switch (state)
            // {
            //     case (dis < 10 && (!Physics.Linecast(transform.position += Vector3.up, (player.transform.position - transform.position) + Vector3.up):
                


            // }









            if (dis < 10 )
            {
            if (!Physics.Linecast(transform.position += Vector3.up, (player.transform.position - transform.position) + Vector3.up, zombieLayerMask ))
                {
                state = PlayerState.hunt;
                Debug.Log("Hunt");
                }
            }

            //
            else if (dis >= 10)
            {
                state = PlayerState.chill;
                Debug.Log("Chill");
            }

            // присвоение состояния персонажу
            switch (state)
            {
                case PlayerState.chill:
                    Chill();
                    break;
                case PlayerState.hunt:
                    Hunt();
                    break;
                    //(здесь должно быть состояние смерти ещё)
            }


           
            // так как мы отключили автоматическое перемещение navMeshAgent, то перемещаем его в след за аниматором, то же делаем для трансформа
            agent.nextPosition += anim.deltaPosition;
            transform.position = agent.nextPosition;

            // вращаем персонажа
            Vector3 direction = (agent.steeringTarget - transform.position).normalized;
            float angle = Vector3.Angle(transform.forward, direction);

            if (angle >= 180)
            {
                direction = (transform.forward - agent.steeringTarget).normalized;
            }

            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 50 * Time.deltaTime);
        }
        else
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Death"))
            {
                do
                {
                    animat = AnimationState.Death;
                }
                while (animat != AnimationState.Death);
            }
        }
    }

    private void OnDrawGizmos()
    {
        // отрисовка позиции цели персонажа для дебага
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(targetPosition, 1);
    }

    
    // описание состояний персонажа
    void Chill()
    {
        // если персонаж доошел до цели на расстояние равное или меньше одного метра, назначить новую цель
        if ((targetPosition - transform.position).magnitude <=1)                
        {
            Destination();
        }
        //если наша цель это игрок и мы находимся в состоянии chill, назначить новую цель
        if(targetPosition == player.transform.position)
        {
            Destination();

        }

        // персонаж останавливается на некоторое время после достижения цели в состоянии chill
        if (waitTime > 0)
        {
            animat = AnimationState.Idle;
            waitTime -= Time.deltaTime;
        }
        else if (waitTime <= 0) animat = AnimationState.Walk;

    }
    void Hunt()
    {
        // если игрок подошел к персонажу меньше чем на 10 метров и между ними нет стены, игрок становиться целью персонажа
        if (Vector3.Distance(player.transform.position, transform.position) >= 1.5 )   //Vector3.Distance(player.position, transform.position) <= 15  && (!Physics.Linecast(player.position, transform.position)
        {
            targetPosition = player.transform.position;
            agent.SetDestination(targetPosition);
            animat = AnimationState.Run;
        }
        // если игрок подошел к персонажу менее одного метра, начать его атаку
        //  && (!Physics.Linecast(player.transform.position, transform.position)))
        else if ( Vector3.Distance(player.transform.position, transform.position) < 1.5)
        {
            //agent.isStopped = true;
            animat = AnimationState.Attack;
            targetPosition = player.transform.position;
            //agent.SetDestination(targetPosition);             //(не хватает вращения в сторону игрока) 
                                                                // в состоянии хант какой то exeption, перс крутиться пока не переходит в состояние чилл
            
        }
    }

    void Destination()
    {
        do
        {
            waitTime = Random.Range(3, 7);
            targetPosition.x = centr.x + Random.Range(-10, 10);
            targetPosition.z = centr.z + Random.Range(-10, 10);
            targetPosition.y = centr.y;
            agent.CalculatePath(targetPosition, path);
        }
        while (path.status != NavMeshPathStatus.PathComplete);
        agent.SetDestination(targetPosition);                          // есть подозрение что он вращается из за этого куска (каждый раз обновляя место назначения)
    }

    public void OnTakeDamage(int damage)
    {
        if (health > 0)
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Damage"))
            {
                animat = AnimationState.Damage;
            }
            health -= damage;
        }
        //else
        //{
        //    if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Death"))
        //    {
        //        animat = AnimationState.Death;

        //    }
        //}
    }

    void Roar()
    {
        roarEvent.Post(gameObject);
    }
}
