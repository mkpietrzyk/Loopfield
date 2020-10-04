using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameManager gameManager;
    public Slider healthSlider;
    public int health = 100;

    public bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("BarrierRefresh"))
        {
            gameManager.RefreshBarrier();
        }

        if (other.gameObject.CompareTag("HealthCube"))
        {
            if (health + 30 > 100)
            {
                healthSlider.value = 100;
            }
            else
            {
                health += 30;
                healthSlider.value = health;
            }

            healthSlider.value = health;
        }

        if (other.gameObject.CompareTag("Block"))
        {
            if (health - 10 < 0)
            {
                isDead = true;
                healthSlider.value = 0;
            }
            else
            {
                health -= 10;
                healthSlider.value = health;
            }
            
        }
    }
}
