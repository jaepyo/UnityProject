using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuddenEnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;
    CapsuleCollider2D capsuleCollider;
    PlayerMove player;

    public int nextMove;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    private void FixedUpdate()
    {
        Vector2 upVec = new Vector2(rigid.position.x - 0.3f, rigid.position.y);
        Debug.DrawRay(upVec, Vector3.up, new Color(0, 8, 0));

        RaycastHit2D rayHit = Physics2D.Raycast(upVec, Vector3.up, 100, LayerMask.GetMask("Player"));
        if (rayHit.collider != null)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, nextMove);
        }
    }
}
