using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCameraController : MonoBehaviour
{
    public Camera camera;
    public Transform target;
    float speedX = 200;
    float speedY = 100;
    float limitY = 40;
    float minDistance = 1.5f;
    float maxDistance;
    Vector3 localPosition;
    float currentYRotation;
    public LayerMask obstacles;
    public LayerMask noPlayer;
    Vector3 _position
    {
        get { return transform.position; }
        set { transform.position = value;}
    }
    // Start is called before the first frame update
    void Start()
    {
        localPosition = target.InverseTransformPoint(_position);
        maxDistance = Vector3.Distance(_position, target.position);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        _position = target.TransformPoint(localPosition);
        CameraRotation();
        ObstaclesReact();
        PlayerReact();
        localPosition = target.InverseTransformPoint(_position);
    }

   

    void CameraRotation() 
    {
        var mx = Input.GetAxis("Mouse X");
        var my = Input.GetAxis("Mouse Y");

        if (my != 0)
        {
            var tmp = Mathf.Clamp(currentYRotation + my * speedY * Time.deltaTime, -limitY, limitY);
            if (tmp != currentYRotation)
            {
                var rot = tmp - currentYRotation;
                transform.RotateAround(target.position, transform.right, rot);
                currentYRotation = tmp;
            }
        }
        if(mx != 0)
        {
            transform.RotateAround(target.position, Vector3.up, mx * speedX * Time.deltaTime);
        }
        transform.LookAt(target);
    }

    void ObstaclesReact()
    {

    }

    void PlayerReact()
    {

    }
}
