using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fallinground : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;
    Rigidbody2D rigidBody;

    public int nextMove;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        Vector2 downVec = new Vector2(rigidBody.position.x - 0.5f, rigidBody.position.y);
        Debug.DrawRay(downVec, Vector3.down, new Color(0, 8, 0));

        RaycastHit2D rayHit = Physics2D.Raycast(downVec, Vector3.down, 100, LayerMask.GetMask("Player"));
        if (rayHit.collider != null)
        {
            rigidBody.gravityScale = 3;
        }
    }
}