using System.Collections;   
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class idle : MonoBehaviour
{
    float vel = 2f;
    private float movendo;
    private Rigidbody2D rb;
    private Animator animator;
    private float jump = 2f;
    private int runningHash = Animator.StringToHash("running");
    private int jupingHash = Animator.StringToHash("jumping");
    private int shootingHash = Animator.StringToHash("shooting");
    private int runshootHash = Animator.StringToHash("runshoot");
    private SpriteRenderer spriterender;
    private bool tiro;
    [SerializeField] private GameObject balas;
    [SerializeField] private GameObject cano;
    [SerializeField] private GameObject cano2;

    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriterender = GetComponent<SpriteRenderer>(); 
    }

    // Update is called once per frame
    void Update()
    {
        movendo = Input.GetAxis("Horizontal");
        
        
        if (movendo < 0f)
        {
            spriterender.flipX = true;
        }
        else if (movendo > 0f)
        {
            spriterender.flipX = false;
        }

        if ((Input.GetKeyDown(KeyCode.Space)| Input.GetKey(KeyCode.UpArrow)) & jump>0)
        {
            jump--;
            rb.AddForce(Vector2.up * 350);
        }

        if (Input.GetKey(KeyCode.Q))

        {
            tiro = true;
            if (spriterender.flipX)
            {
                Instantiate(balas, new Vector3(cano2.transform.position.x, cano2.transform.position.y, 0), cano2.transform.rotation * quaternion.Euler(0f, 180f, 0f)); 
            }
            else
            {
                Instantiate(balas, new Vector3(cano.transform.position.x, cano.transform.position.y, 0), cano.transform.rotation);
            }
        }
        else
        {
            tiro = false;
        }
        animator.SetBool(runningHash, movendo != 0 & tiro==false);
        animator.SetBool(jupingHash, jump != 2f);
        animator.SetBool(shootingHash, tiro & movendo==0);
        animator.SetBool(runshootHash, tiro & movendo!=0);
    }

   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        jump = 2f;
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(vel * movendo, rb.velocity.y);
    }
}
