using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class new_footstep_script : MonoBehaviour
{
    public GameObject footLeft;  // это объект левой ноги. перетаскиваем в компонент
    public GameObject footRight; // объект правой ноги
    public AK.Wwise.Event footevent; // выбираем ивент Wwise
    //public LayerMask lm;
    public GameObject hero;
    public float currentSpeed;
    private void Start()
    {
        currentSpeed = hero.gameObject.transform.forward.magnitude;

    }
    private void Update()
    {

        //AkSoundEngine.SetRTPCValue("RTPC_ext_InputMagnitude", hero.cc.inputMagnitude);// передаем параметр скорости в ввайс
       // Debug.Log(hero.GetComponent<Mover>().currentSpeed);

    }


    void footstep_walking(string arg) // вызываем эту функцию из аниматора. Аргумент стринг - строчка стринг для каждой функции. Её нужно прописать в анимции
    {
        if (arg == "left") // если вызвали функцию с аргументом Left 
        {
            Playfootstep(footLeft); // запускаем функцию для объекта левой ноги
        }
        else if (arg == "right") // то же самое для правой ноги
        {
            Playfootstep(footRight);
        }
       
    }

    void Playfootstep(GameObject footObject) // функция проверки поверхности для нужного геймобъекта
    {
        //Debug.Log("123");
        // Debug.Log(hero.gameObject.transform.forward.magnitude);

        if (Physics.Raycast(footObject.transform.position, Vector3.down, out RaycastHit hit, 0.3f)) // запускаем рейкаст из объекта нужной ноги вниз
            {
            //Debug.Log("123");
            AkSoundEngine.SetSwitch("surface_type_Run", hit.collider.tag, footObject); // выставляем свитч нужной свитч-группы в положение такое же как тэг поверхности, на которую наступила нога, применяем свитч для нужной ноги
                footevent.Post(footObject); // запускаем ивент для из нужной ноги
                Debug.Log(hit.collider.tag);
            }

       
    }

    private void OnDrawGizmos()
    {
        //Ray ray;
        //Gizmos.color = Color.red;
        //Gizmos.DrawRay()
    }


}