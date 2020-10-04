using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour
{
    public Camera cam;
    public GameManager gameManager;
    public float barrierTimer;

    public Rigidbody2D rb;
    // Update is called once per frame

    private Vector2 _mousePos;

    private void Start()
    {
        barrierTimer = gameManager.timer;
    }

    void Update()
    {
        if (gameManager.gameStarted && !gameManager.gamePaused)
        {
            barrierTimer = gameManager.timer;   
        }
        _mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        if (gameManager.gameStarted && !gameManager.gamePaused)
        {
            if (rb.CompareTag("Barrier"))
            {
            
                var currentTimer = 60 - (int) barrierTimer;
                Vector2 lookDir = _mousePos - rb.position;
                float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg -180f - 3 * currentTimer;
                rb.rotation = angle;
            }
            else
            {
                Vector2 lookDir = _mousePos - rb.position;
                float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
                rb.rotation = angle;
            }   
        }
    }
}
