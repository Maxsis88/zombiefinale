using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombie_footsteps : MonoBehaviour
{
    public GameObject footLeft;  // это объект левой ноги. перетаскиваем в компонент
    public GameObject footRight; // объект правой ноги
    public AK.Wwise.Event footevent; // выбираем ивент Wwise
    public AK.Wwise.Event runevent;
    //public LayerMask lm;
    public GameObject zombie;
    public float currentSpeed;
    private void Start()
    {
        currentSpeed = zombie.gameObject.transform.forward.magnitude;

    }
    private void Update()
    {

        //AkSoundEngine.SetRTPCValue("RTPC_ext_InputMagnitude", hero.cc.inputMagnitude);// передаем параметр скорости в ввайс
        // Debug.Log(hero.GetComponent<Mover>().currentSpeed);

    }


    void zombie_walking(string arg) // вызываем эту функцию из аниматора. Аргумент стринг - строчка стринг для каждой функции. Её нужно прописать в анимции
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

    void RunZombie(string arg) // вызываем эту функцию из аниматора. Аргумент стринг - строчка стринг для каждой функции. Её нужно прописать в анимции
    {
        if (arg == "left") // если вызвали функцию с аргументом Left 
        {
            Runfootstep(footLeft); // запускаем функцию для объекта левой ноги
        }
        else if (arg == "right") // то же самое для правой ноги
        {
            Runfootstep(footRight);
        }

    }

    void Playfootstep(GameObject footObject) // функция проверки поверхности для нужного геймобъекта
    {
        

        if (Physics.Raycast(footObject.transform.position, Vector3.down, out RaycastHit hit, 0.3f)) // запускаем рейкаст из объекта нужной ноги вниз
        {
            AkSoundEngine.SetSwitch("surface_type_Run", hit.collider.tag, footObject); // выставляем свитч нужной свитч-группы в положение такое же как тэг поверхности, на которую наступила нога, применяем свитч для нужной ноги
            footevent.Post(footObject); // запускаем ивент для из нужной ноги

        }
    }

    void Runfootstep(GameObject footObject) // функция проверки поверхности для нужного геймобъекта
    {


        if (Physics.Raycast(footObject.transform.position, Vector3.down, out RaycastHit hit, 0.3f)) // запускаем рейкаст из объекта нужной ноги вниз
        {
            
            AkSoundEngine.SetSwitch("surface_type_Run", hit.collider.tag, footObject); // выставляем свитч нужной свитч-группы в положение такое же как тэг поверхности, на которую наступила нога, применяем свитч для нужной ноги
            runevent.Post(footObject); // запускаем ивент для из нужной ноги

        }
    }
}

    



