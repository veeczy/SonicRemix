using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class enemyScript : MonoBehaviour
{

    public int EnemySpeed;
    public Rigidbody2D rb;
    Animator animator;
    bool detection = false;
    int direction = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.AddComponent<Rigidbody2D>();
        //animator = gameObject.AddComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!detection)
        {
            return;
        }
    }

    //FixedUpdate has the same call rate as the physics system
    private void FixedUpdate()
    {
        if(!detection)
        {
            return;
        }

        Vector2 position = rb.position;
        position.x = position.x + EnemySpeed * direction * Time.deltaTime;
        rb.MovePosition(position);
    }
    void OnTriggerEnter2D(Collider2D enemy)
    {
        if (enemy.tag == "player")
        {
            detection = true;
        }

        if(enemy.tag == "Ground")
        {
            // stop enemy movement OR flip the enemy to run other way
        }
    }
}
