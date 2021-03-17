using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearController : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;
    PlayerMove player;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

        spriteRenderer.color = new Color(1, 1, 1, 0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            Active();
    }

    void Active()
    {
        spriteRenderer.color = new Color(1, 1, 1, 1f);
    }
}
