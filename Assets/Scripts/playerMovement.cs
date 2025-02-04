using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class playerMovement : MonoBehaviour
{
    //Variables for Player Movement
    // input for up down left right etc define it

    //Variables for Collectibles
    public int coinCount;
    public int total = 100;
    public TextMeshProUGUI coinText;

    //Variables for Position, Speed, and Angles
    public float XPosition;
    public float YPosition;
    public float XSpeed;
    public float YSpeed;
    public float groundSpeed;
    public float moveSpeed;
    public float groundAngle;
    public float widthRadius;
    public float heightRadius;

    //Variables for hitbox and pixel ratio
    public float pixel;
    public float subpixel;

    //Variables for Timer
    public float timeRemaining;
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
    [SerializeField] private ParticleSystem damaageParticles;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // player movement if tree
    }

    // FixedUpdate has the same call rate as the physics system
    void FixedUpdate()

    {
        //physics calculation
    }

    //functions related to speed calculations
    float Subpixel_To_Decimal()
    {
        return pixel + (subpixel / 256);
    }
}
