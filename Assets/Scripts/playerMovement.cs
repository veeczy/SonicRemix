using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
//using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{
    //Variables for Player Movement
    private float horizontal;
    public float speed = 150f;
    public float jumpingPower = 400f;
    private bool isFacingRight = true;
    public bool jumping;
    public bool spindash;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    //Variables for animations
    Animator animator;
    public bool slope = false;

    //variables for health, lives, damage
    public bool IsInvincible = false;
    public int lives = 3;
    public TextMeshProUGUI livesText;

    //Variables for Collectibles
    public int coinCount;
    public int total = 100;
    public TextMeshProUGUI coinText;

    //Variables for Timer
    public float timeRemaining;
    public bool timerIsRunning;
    public float minutes;
    public float seconds;
    public TextMeshProUGUI timeText;

    //Variables for Win/Lose
    public bool win = false;
    public bool lose = false;
    //Variables for win/lose UI
    public GameObject winScreen;
    public GameObject loseScreen;

    //Variables for VFX
    public AudioSource backgroundMusic;
    public AudioSource jumpSource;
    public AudioSource winSource;
    public AudioSource loseSource;
    public AudioSource coinSource;
    public AudioSource damagePlantSource;
    public AudioSource damageDeerSource;
    public AudioSource enemyDamageSource;
    public AudioSource runSource;
    [SerializeField] private ParticleSystem damageParticles;
    [SerializeField] private ParticleSystem jumpParticles;
    [SerializeField] private ParticleSystem coinParticles;

    // Start is called before the first frame update
    void Start()
    {
        timeRemaining = 180f;
        // if cutscene for start is done
        timerIsRunning = true;
        win = false;
        lose = false;

        spindash = false;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //win lose ui
        if (!win) { winScreen.gameObject.SetActive(false); }
        if (!lose) { loseScreen.gameObject.SetActive(false); }


        // player movement 
        //detect movement input
        horizontal = Input.GetAxis("Horizontal");
        if (horizontal < 0) { Flip(); }
        if (horizontal > 0) { Flip(); }
        animator.SetFloat("horizontal", horizontal);
        //speed up more you run
        if (horizontal != 0)
        {
            runSource.Play();
            speed = speed + 10 * (Time.deltaTime);
        }
        if (horizontal == 0) { speed = 100; }

        // jump
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            jumping = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            //vfx
            jumpParticles.Play();
            jumpSource.Play();
        }

        if (Input.GetButtonUp("Jump"))
        {
            jumping = false;
        }

        //jump up more you jumping
        if (jumping)
        {
            jumpingPower = jumpingPower + 10 * (Time.deltaTime);
            animator.SetBool("jumping", true);
        }
        if (!jumping)
        {
            jumpingPower = 300;
            animator.SetBool("jumping", false);
        }


        //if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            //rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        // spindash ?
        if (Input.GetKeyDown(KeyCode.S))
        {
            //spindash animation
            spindash = true;
            animator.SetBool("spindash", true);
            jumpParticles.Play();
            //hitbox change size
            //speed change
        }
        if(Input.GetKeyUp(KeyCode.S))
        {
            spindash = false;
            animator.SetBool("spindash", false);
        }
        if(spindash) { IsInvincible = true; }
        if(!spindash) { IsInvincible = false; }

        //run 45
        if (horizontal > 0 && slope)
        {
            animator.SetBool("run45", true);
        }
        if (!slope) { animator.SetBool("run45", false); }

        //timer
        if (timeRemaining > 0 && timerIsRunning)
        {
            timeRemaining -= Time.deltaTime;
        }
        if (timeRemaining <= 0 && timerIsRunning) //if time runs out
        {
            loseSource.Play();
            lose = true;
            Debug.Log("Time has run out!");
            timeRemaining = 0;
            //timerIsRunning = false;
        }

        //timer display in UI
        minutes = Mathf.FloorToInt(timeRemaining / 60);
        seconds = Mathf.FloorToInt(timeRemaining % 60);
        timeText.text = string.Format("TIME {0:00}:{1:00}", minutes, seconds);
        coinText.text = string.Format("RINGS {0}", coinCount);
        livesText.text = string.Format("{0}", lives);

        if (timeRemaining == 0)
        {
            timeText.text = ("0:00");
            gameLost();
        }

    }

    // FixedUpdate has the same call rate as the physics system
    void FixedUpdate()

    {
        //physics calculation
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip() //what direction you are facing
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // collect coin
        if (other.gameObject.CompareTag("Coin"))
        {
            // vfx
            //coinParticles.Play();
            coinSource.Play();

            //remove collectible
            Destroy(other.gameObject);

            // coin counter change
            coinCount++;
        }

        //damage system
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (coinCount > 0 && (!IsInvincible))
            {
                //damageParticles.Play();
                coinParticles.Play();
                damageDeerSource.Play();
                coinCount = 0;
            }
            if (coinCount <= 0 && (!IsInvincible))
            {
                damageParticles.Play();
                damageDeerSource.Play();
                if (lives > 0 && coinCount == 0)
                {
                    lives--;
                    //move player position to start
                }
                if (lives == 0)
                {
                    gameLost();
                }
            }
            if(IsInvincible)
            {
                enemyDamageSource.Play();
                Destroy(other.gameObject);
            }
        }

        if (other.gameObject.CompareTag("winzone"))
        {
            timerIsRunning = false;
            gameWon();
        }
    }
    bool cheatOn()
    {
        IsInvincible = true;
        return IsInvincible;
    }

    //script for win and lose, where you change scene and play sound
    void gameWon()
    {
        //win script
        backgroundMusic.Stop();
        winSource.Play();
        winScreen.gameObject.SetActive(true);
    }
    void gameLost()
    {
        //lose script
        backgroundMusic.Stop();
        loseSource.Stop();
        loseScreen.gameObject.SetActive(true);
    }

}
