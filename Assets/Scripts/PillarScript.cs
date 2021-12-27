using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarScript : MonoBehaviour
{
    public Transform player;
    Animator m_anim;

    void Start()
    {
        m_anim = GetComponent<Animator>(); 
    }

    void Update()
    {
        /*foreach (Transform pillar in transform)
        {
            float distance = Vector3.Distance(player.position, pillar.position);
            //Debug.Log("La distanza vale: " + distance);
            if (distance <= 10.0f)
                pillar.GetComponent<Animator>().enabled = true;
        }*/

        float distance = Vector3.Distance(player.position, transform.position);
        //Debug.Log("La distanza vale: " + distance);
        if (distance <= 10.0f)
            m_anim.enabled = true;
    }


}
