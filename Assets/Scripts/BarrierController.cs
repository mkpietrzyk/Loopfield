using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class BarrierController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TextMeshProUGUI shieldTimer;
    public GameManager gameManager;
    private PolygonCollider2D _polygonCollider2D;
    public Sprite[] spritesArray;
    private SpriteRenderer _spriteRenderer;
    public float barrierTimer;
    void Start()
    {
        barrierTimer = gameManager.timer;
        StartCoroutine(nameof(DecreaseBarrier));
    }

    private void Update()
    {
        barrierTimer = gameManager.timer;
        if (barrierTimer > 0) {
            var intTimer = (int) barrierTimer;
            shieldTimer.text = intTimer.ToString();
        }
    }

    IEnumerator DecreaseBarrier()
    {
        while (barrierTimer > 0)
        {
            var timeBarrierIndex = 60 - (int) barrierTimer;
            if (timeBarrierIndex < 0 || timeBarrierIndex > 59)
            {
                timeBarrierIndex = 59;
            }
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.sprite = spritesArray[timeBarrierIndex];
            _polygonCollider2D = GetComponent<PolygonCollider2D>();
            _polygonCollider2D.pathCount = _spriteRenderer.sprite.GetPhysicsShapeCount();
 
            List<Vector2> path = new List<Vector2>();
            for (int i = 0; i < _polygonCollider2D.pathCount; i++) {
                path.Clear();
                _spriteRenderer.sprite.GetPhysicsShape(i, path);
                _polygonCollider2D.SetPath(i, path.ToArray());
            }
            yield return new WaitForSeconds(1);
        }
    }
}