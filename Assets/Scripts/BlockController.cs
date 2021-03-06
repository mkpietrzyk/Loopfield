﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public GameManager gameManager;
    private Material _material;
    private bool _isDestroyed = false;
    private float _fade = 1;
    public AudioSource audioSource;

    private void Awake()
    {
        _material = GetComponent<SpriteRenderer>().material;
        gameManager = FindObjectOfType<GameManager>();
        audioSource.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (gameManager.gameEnd)
        {
            DestroyOnImpact();
        }
        if (_isDestroyed)
        {
            _fade -= Time.deltaTime;

            if (_fade <= 0f)
            {
                _fade = 0f;
                _isDestroyed = false;
            }
            
            _material.SetFloat("_Fade", _fade);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Barrier"))
        {
            audioSource.PlayOneShot(audioSource.clip, 0.3f);
            gameManager.GetComponent<GameManager>().UpdateScore(100);
            DestroyOnImpact();
        }

        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    private void DestroyOnImpact()
    {
        _isDestroyed = true;
        Destroy(gameObject, 1f);
    }
}