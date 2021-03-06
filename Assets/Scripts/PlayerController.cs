﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameManager gameManager;
    public Slider healthSlider;
    public AudioClip blockHit;
    public AudioClip healthHit;
    public AudioClip barrierRefreshHit;
    public AudioClip destroyCraft;
    public AudioSource audioSource;
    public int health = 100;

    public bool isDead = false;
    private float _fade;

    private Material _material;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        _material = GetComponent<SpriteRenderer>().material;

    }
    void Update()
    {
        if (gameManager.isPlayerDead)
        {
            _fade -= Time.deltaTime;

            if (_fade <= 0f)
            {
                _fade = 0f;
            }
            
            _material.SetFloat("_Fade", _fade);
        }
        else
        {
            _material.SetFloat("_Fade", 1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("BarrierRefresh"))
        {
            audioSource.PlayOneShot(barrierRefreshHit, 0.3f);
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
            audioSource.PlayOneShot(healthHit, 0.3f);
            healthSlider.value = health;
        }

        if (other.gameObject.CompareTag("Block"))
        {
            audioSource.PlayOneShot(blockHit, 0.3f);
            if (health - 10 <= 0)
            {
                audioSource.PlayOneShot(destroyCraft, 0.3f);
                gameManager.isPlayerDead = true;
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
