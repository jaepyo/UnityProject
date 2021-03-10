using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashEnemy : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;
    CapsuleCollider2D capsuleCollider;
    PlayerMove player;
    public int nextMove;
    // Start is called before the first frame update

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }
/*
    // Update is called once per frame
    void FixedUpdate()
    {
        // Move
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        // Platform Check
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 5, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.left, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.left, 0.5f, LayerMask.GetMask("Platform"));
        if (rayHit.collider == null) {
            Turn();
        }
    }
*/
    void FixedUpdate()
    {
       Vector2 upVec = new Vector2(rigid.position.x - 0.3f, rigid.position.y);
       Debug.DrawRay(upVec, Vector2.left+Vector2.down, new Color(10, 0, 0));
       RaycastHit2D rayHit = Physics2D.Raycast(upVec, Vector2.left+Vector2.down, 40, LayerMask.GetMask("Player"));
       if (rayHit.collider != null)
        {
            rigid.velocity = new Vector2(nextMove, rigid.velocity.y);
        }

    }
}
   
