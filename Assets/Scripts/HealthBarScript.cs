using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    private Image healthBar;
    public float currentHealth;
    private float maxHealth = 100f;
    PlayerController player;
    //private GameObject cylinder;

    void Start()
    {
        //cylinder = GameObject.Find("ShapeManager/Cylinder");
        healthBar = GetComponent<Image>();
        player = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        currentHealth = player.health;
        healthBar.fillAmount = currentHealth / maxHealth;
    }
}
