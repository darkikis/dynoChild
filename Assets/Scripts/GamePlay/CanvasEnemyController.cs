using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasEnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    private Camera cameraInScene;
    void Start()
    {
        cameraInScene = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.LookAt(transform.position + cameraInScene.transform.rotation * Vector3.forward, cameraInScene.transform.rotation * Vector3.up);

        transform.forward = Camera.main.transform.forward;
    }
}
