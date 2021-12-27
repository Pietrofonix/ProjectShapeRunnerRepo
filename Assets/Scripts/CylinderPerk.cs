using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CylinderPerk : MonoBehaviour
{
    Image m_cylinderPerkBar;
    Image m_cylinderPerkBarFill;
    float maxCylinderPerkTimer = 5f;
    //public float currentCylinderPerkTimer;
    public static bool startCooldownCylinder = false;
    public static bool startCylinderPerkBar = false;
    public Image cylinderPerkBarBG;
    [SerializeField] GameObject cylinder;

    void Start()
    {
        m_cylinderPerkBar = GetComponent<Image>();
        m_cylinderPerkBarFill = GetComponent<Image>();
        m_cylinderPerkBar.fillAmount = 1f;
        startCylinderPerkBar = false;
    }

    void Update()
    {
        CylinderPerkBar();
        CooldownCylinderBar();

        if (cylinder.activeInHierarchy)
        {
            cylinderPerkBarBG.enabled = true;
            m_cylinderPerkBarFill.enabled = true;
        }
        else
        {
            cylinderPerkBarBG.enabled = false;
            m_cylinderPerkBarFill.enabled = false;
        }
        /*Debug.Log("Il perk è: " + startCylinderPerkBar);
        Debug.Log("Il cooldown è: " + startCooldownCylinder);
        Debug.Log("La quantità è: " + cylinderPerkBar.fillAmount);*/
    }

    void CylinderPerkBar()
    {
        if(startCylinderPerkBar)
        {
            m_cylinderPerkBar.fillAmount -= Time.deltaTime / maxCylinderPerkTimer;
            if (m_cylinderPerkBar.fillAmount <= 0f)
            {
                startCylinderPerkBar = false;
                startCooldownCylinder = true;
            }
        }
    }

    void CooldownCylinderBar()
    {
        if(!startCylinderPerkBar)
        {
            m_cylinderPerkBar.fillAmount += Time.deltaTime / (maxCylinderPerkTimer * 2);
            if (((int)m_cylinderPerkBar.fillAmount) >= 1)
            {
                startCooldownCylinder = false;
            }
        }
    }
}
