using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{

    public Transform playerTransform;
    public float cameraDistance;



    // Start is called before the first frame update
    void Start()
    {
        cameraDistance = -cameraDistance;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y - cameraDistance, playerTransform.position.z);
    }
}
