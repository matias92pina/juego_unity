using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player obj;

    public int lives = 3;

    public bool isGrounded = false;
    public bool isMoving = false;
    public bool isInmune = false;

    public float speed = 5f; //Si o si la F cuando estámos con flotante en Unity
    public float jumpForce = 3f;
    public float movHor;

    public float inmuneTimeCent = 0f;
    public float inmuneTime = 0.5f;

    public LayerMask groundLayer;
    public float radius = 0.3f;
    public float groundRayDist = 0.5f;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer spr;

    void Awake()
    {
        obj = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Game.obj.gamePaused)
        {
            movHor = 0;
            return;
        }

        movHor = Input.GetAxisRaw("Horizontal");

        isMoving = (movHor != 0f);

        isGrounded = Physics2D.CircleCast(transform.position, radius, Vector3.down, groundRayDist, groundLayer);

        if (Input.GetKeyDown(KeyCode.Space))
            jump();

        if (isInmune)
        {
            spr.enabled = !spr.enabled;

            inmuneTimeCent -= Time.deltaTime;

            if (inmuneTimeCent <= 0)
            {
                isInmune = false;
                spr.enabled = true;
            }
        }

        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isGrounded", isGrounded);

        flip(movHor);
    }

    void GoInmune()
    {
        isInmune = true;
        inmuneTimeCent = inmuneTime;
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(movHor * speed, rb.velocity.y);
    }

    public void jump()
    {
        if (!isGrounded) return;

        rb.velocity = Vector2.up * jumpForce;

        AudioManager.obj.playJump();
    }

    private void flip(float _xValue)
    {
        Vector3 TheScale = transform.localScale;

        if (_xValue < 0)
            TheScale.x = Mathf.Abs(TheScale.x) * -1;
        else
        if (_xValue > 0)
            TheScale.x = Mathf.Abs(TheScale.x);

        transform.localScale = TheScale;

    }

    public void getDamage()
    {
        lives--;

        AudioManager.obj.playHit();

        UIManager.obj.updateLives();

        GoInmune();

        if (lives <= 0)
        {
            FXManager.obj.showPop(transform.position);
            Game.obj.gameOver();
        }
            
    }

    void OnDestroy()
    {
        obj = null;
    }

    public void addLive()
    {
        lives++;

        if (lives > Game.obj.maxLives)
            lives = Game.obj.maxLives;
    }
}
