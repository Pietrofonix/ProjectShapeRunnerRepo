using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class giacomo : MonoBehaviour
{
    [SerializeField] Transform xPlayerPos;
    float xPos = 0f;
    float timer = 0f;

    private void Start()
    {
        xPos = transform.position.x;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (xPos == xPlayerPos.position.x)
            {
                timer = 0f;
            }
            else
            {
                timer += Time.deltaTime;
                xPos = Mathf.Lerp(xPos, xPlayerPos.position.x, timer / 50f);
                transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
            }
        }
    }

}
