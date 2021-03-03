using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopBox : MonoBehaviour
{

    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;

    void Awake() // 초기화는 awake에서
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer.color = new Color(1, 1, 1, 0f);
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            spriteRenderer.color = new Color(1, 1, 1, 1f);
        }
    }
}
