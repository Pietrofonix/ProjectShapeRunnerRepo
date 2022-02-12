using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] Transform m_target;
    [SerializeField] float m_speed;

    void FixedUpdate()
    {
        transform.LookAt(m_target);
        transform.Translate(Vector3.right * m_speed * Time.fixedDeltaTime);
    }
}
