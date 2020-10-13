using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{

    public Transform playerTransform;
    public Vector3 cameraDistance;



    // Start is called before the first frame update
    void Start()
    {
        cameraDistance = -cameraDistance;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newCamPos = playerTransform.position - cameraDistance;
        transform.position = newCamPos;
    }



    
}
