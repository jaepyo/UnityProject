using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public GameManager gameManager;
    public AudioClip audioJump;
    public AudioClip audioAttack;
    public AudioClip audioDamaged;
    public AudioClip audioItem;
    public AudioClip audioDie;
    public AudioClip audioFinish;
    public GameObject finish;
    public GameObject dis;


    public float maxSpeed;
    public float jumpPower;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;
    CapsuleCollider2D capsuleCollider;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Awake() // 초기화는 awake에서
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()  // 1초에 60번 단발적인 키입력해는 update에 다가
    {
        // Jump
        // anim.getbool ~~ 2단점프 방지, 점프하고 있을때는 X
        if (Input.GetButtonDown("Jump") && !anim.GetBool("isJumping")) {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("isJumping", true);
            PlaySound("JUMP");
            audioSource.Play();
        }
            
        
        // Stop Speed
        if (Input.GetButtonUp("Horizontal"))
        {
            // 가다가 키 땠을 때 속도 멈추기
            // normalized ; 벡터크기를 1로 만든 상태 (단위벡터), 단위구할 때 사용, 방향구할 때
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }

        // Direction Sprite
        if(Input.GetButton("Horizontal"))
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;

        // Animation
        if (Mathf.Abs(rigid.velocity.x) < 0.3)
            anim.SetBool("isWalking", false);
        else
            anim.SetBool("isWalking", true);

    }

    // Update is called once per frame
    void FixedUpdate() // 보통 1초에 50회 돈다. 1초동안 꾹 누르면 힘을 1초에 50번 주는 거
    {
        // Move Speed  Move By Key Control,
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h*3, ForceMode2D.Impulse);
        
        // Max Speed
        if (rigid.velocity.x > maxSpeed) // Right Max Speed
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);  // 여기서  y축 0으로 하면 공중에서 멈춤
        else if (rigid.velocity.x < maxSpeed*(-1)) // Left Max Speed
            rigid.velocity = new Vector2(maxSpeed*(-1), rigid.velocity.y);

        // Landing Platform
        if (rigid.velocity.y < 0)
        {
            // 이 밑에껀 눈에 보이게 하기위한 도구, 안쓰면 안보임 
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));

            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));
            if (rayHit.collider != null){
                if (rayHit.distance < 0.5f)
                    anim.SetBool("isJumping", false);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy"){
            // Attack
            if(rigid.velocity.y < 0 && transform.position.y > collision.transform.position.y)
            {
                OnAttack(collision.transform);
            } 
            else  // Damaged
                OnDamaged(collision.transform.position);
        }
        if (collision.gameObject.tag == "SuperEnemy")
        {
            OnDamaged(collision.transform.position);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Item")
        {
            bool isBronze = collision.gameObject.name.Contains("Bronze");
            bool isSilver = collision.gameObject.name.Contains("Silver");
            if (isBronze)
            {
                gameManager.LifeNumberUp();
            }
            else if (isSilver)
            {finish.SetActive(true); }


            // Deactive Item
            collision.gameObject.SetActive(false);

            // Sound
            PlaySound("ITEM");
            audioSource.Play();
        }
        else if(collision.gameObject.tag == "Finish")
        {
            // Next Stage
            gameManager.NextStage();

            // Sound
            PlaySound("FINISH");
            audioSource.Play();
        }
        else if(collision.gameObject.tag == "Disappear")
        {
            collision.gameObject.SetActive(false);
            //disappear.SetActive(false);
        }
        else if (collision.gameObject.tag == "dis")
        {
            dis.SetActive(false);
        }

        else if (collision.gameObject.tag == "CheckPoint")
        {
            collision.gameObject.SetActive(false);
            GameManager.checkPoint = true;
        }
        
    }
   
    void OnAttack(Transform enemy)
    {
        // Point
        gameManager.stagePoint += 100;
        // Reaction Force
        rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

        // Enemy Die
        EnemyMove enemyMove = enemy.GetComponent<EnemyMove>();
        if(enemyMove != null)
            enemyMove.OnDamaged();
        
        FlyEnemy flyEnemy = enemy.GetComponent<FlyEnemy>();
        if(flyEnemy != null)
            flyEnemy.OnDamaged();

        PopEnemy popEnemy = enemy.GetComponent<PopEnemy>();
        if (popEnemy != null)
            popEnemy.OnDamaged();


        // Sound
        PlaySound("ATTACK");
        audioSource.Play() ;
    }

    void OnDamaged(Vector2 targetPos)
    {
        // Health Down
        //gameManager.HealthDown();
        // LifeNumber UP
        gameManager.LifeNumberUp();
        
        // Chage Layer (immortal Active)
        gameObject.layer = 11;

        // View Alpha
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        // Reaction Force
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1)*7, ForceMode2D.Impulse);

        // Animation
        anim.SetTrigger("doDamaged");

        Invoke("OffDamaged", 1);

        // Sound
        PlaySound("DAMAGED");
        audioSource.Play();
    }

    void OffDamaged()
    {
        gameObject.layer = 10;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }
    /*
    public void OnDie()
    {
        // Sprite Alpha
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        // Sprite Flip Y
        spriteRenderer.flipY = true;
        // Collider Disable
        capsuleCollider.enabled = false;
        // Die Effect Jump
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        // Sound
        PlaySound("DIE");
        audioSource.Play();
    }
    */

    public void VelocityZero()
    {
        rigid.velocity = Vector2.zero;
    }

    void PlaySound(string action)
    {
        switch (action)
        {
            case "JUMP":
                audioSource.clip = audioJump;
                break;
            case "ATTACK":
                audioSource.clip = audioAttack;
                break;
            case "DAMAGED":
                audioSource.clip = audioDamaged;
                break;
            case "ITEM":
                audioSource.clip = audioItem;
                break;
            case "DIE":
                audioSource.clip = audioDie;
                break;
            case "FINISH":
                audioSource.clip = audioFinish;
                break;
        }
    }
}
