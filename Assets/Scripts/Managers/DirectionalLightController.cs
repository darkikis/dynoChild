using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalLightController : MonoBehaviour
{
    public float speed = 100.0f;
    Vector3 angle;
    float rotation = 0f;
    public enum Axis
    {
        X,
        Y,
        Z
    }
    // Start is called before the first frame update
    public Axis axis = Axis.X;
    public bool direction = true;
    float m_timer = 2.0f;
    bool isRunning = false;

    IEnumerator MyCoroutine()
    {
        isRunning = true;
        transform.localEulerAngles = new Vector3(0, angle.y, angle.z);
        yield return new WaitForSeconds(3);
        print("3 seconds elapsed");
        transform.localEulerAngles = new Vector3(65, angle.y, angle.z);
        yield return new WaitForSeconds(3);
        print("ended");
        isRunning = false;
    }
    void Update()
    {
        if (!isRunning) StartCoroutine(MyCoroutine());
    }

    
    void Start()
    {
        angle = transform.localEulerAngles;
    }
    /*
    void Update()
    {
        
        switch (axis)
        {
            case Axis.X:
                transform.localEulerAngles = new Vector3(Rotation(), angle.y, angle.z);
                break;
            case Axis.Y:
                transform.localEulerAngles = new Vector3(angle.x, Rotation(), angle.z);
                break;
            case Axis.Z:
                transform.localEulerAngles = new Vector3(angle.x, angle.y, Rotation());
                break;
        }
        
        
    }

    float Rotation()
    {
        rotation += speed * Time.deltaTime;
        if (rotation >= 360f)
            rotation -= 360f; // this will keep it to a value of 0 to 359.99...

        return direction ? rotation : -rotation;
    }
    */
    /*
    void Start()
    {
        angle = transform.localEulerAngles;
        StartCoroutine(waiter());
    }

    IEnumerator waiter()
    {
        //Rotate 90 deg
        //transform.Rotate(new Vector3(5, 0, 0), Space.World);
        transform.localEulerAngles = new Vector3(-10, angle.y, angle.z);

        //Wait for 4 seconds
        yield return new WaitForSecondsRealtime(2);

        //Rotate 40 deg
        //transform.Rotate(new Vector3(-5, 0, 0), Space.World);
        transform.localEulerAngles = new Vector3(+10, angle.y, angle.z);

        //Wait for 2 seconds
        yield return new WaitForSecondsRealtime(2);

    }
    */
}
