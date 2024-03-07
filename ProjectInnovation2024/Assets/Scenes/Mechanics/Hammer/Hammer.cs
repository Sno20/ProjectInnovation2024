using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{

    private int swingCount = 0;
    private bool check = false;

    void Start()
    {

    }

    void Update()
    {

        if (Input.acceleration.sqrMagnitude > 30f)
        {
            Debug.Log("swing");
            swingCount += 1;
        }

        if (!check && swingCount >= 10)
        {
            Debug.Log("glass broken");
            check = true;
        }
    }

}

