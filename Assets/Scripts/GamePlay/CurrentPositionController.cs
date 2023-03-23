using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentPositionController : MonoBehaviour
{
    public static CurrentPositionController instance;
    public Transform playerTransform;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
}
