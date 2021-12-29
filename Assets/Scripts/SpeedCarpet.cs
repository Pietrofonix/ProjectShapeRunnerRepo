using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedCarpet : MonoBehaviour
{

    void Start()
    {

    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        EventManager.StartBoost();
    }
}
