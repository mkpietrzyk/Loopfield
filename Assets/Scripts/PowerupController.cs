using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupController : MonoBehaviour
{
    public GameManager gameManager;
    private Material _material;
    private bool _isDestroyed = false;
    private float _fade = 1f;

    private void Awake()
    {
        _material = GetComponent<SpriteRenderer>().material;
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
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
            gameManager.GetComponent<GameManager>().UpdateScore(30);
            DestroyOnImpact();
        }

        if (other.gameObject.CompareTag("Player"))
        {
            gameManager.GetComponent<GameManager>().UpdateScore(150);
            Destroy(gameObject);
        }
    }

    private void DestroyOnImpact()
    {
        _isDestroyed = true;
        Destroy(gameObject, 3f);
    }
}
