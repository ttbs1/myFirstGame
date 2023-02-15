using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float Speed;
    public float JumpForce;
    private Rigidbody2D rig;
    private Animator anim;
    private bool isJumping;
    public Transform footPosition; 
    private Vector2 move;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        

    }

    void FixedUpdate() {
        rig.velocity = new Vector2(move.x * Speed * Time.deltaTime, rig.velocity.y);
        if (Input.GetAxis("Horizontal") > 0) 
        {
            transform.eulerAngles = new Vector3(0f,0f,0f);    
            anim.SetBool("walk", true);
        } 
        else if (Input.GetAxis("Horizontal") < 0) 
        {
            transform.eulerAngles = new Vector3(0f,180f,0f);
            anim.SetBool("walk", true);
        }
        else anim.SetBool("walk", false);
        
        Jump();
    }

    void Move()
    {
        move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        

        if (Input.GetAxis("Horizontal") > 0) 
        {
            rig.AddForce(Vector2.right * Speed);
            transform.eulerAngles = new Vector3(0f,0f,0f);    
            anim.SetBool("walk", true);
        } 
        else if (Input.GetAxis("Horizontal") < 0) 
        {
            rig.AddForce(Vector2.left * Speed);
            transform.eulerAngles = new Vector3(0f,180f,0f);
            anim.SetBool("walk", true);
        }
        else anim.SetBool("walk", false);
    }

    /*void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.layer == 8) {
            isJumping = false;
        }
    }*/

    void OnCollisionStay2D(Collision2D collision) {
        if(collision.gameObject.layer == 8) {
            isJumping = false;
        }
    }

    void OnCollisionExit2D(Collision2D collision) {
        if(collision.gameObject.layer == 8) {
            isJumping = true;
        }
    }

    void Jump()
    {
        if(Input.GetButtonDown("Jump") && !isJumping)
        {
            rig.AddForce(new Vector2(rig.velocity.x, JumpForce));
            if(rig.velocity.y > 0) {
                anim.SetBool("jump", true);
                anim.SetBool("fall", false);
            }
        }
        // TROCA ANIMACOES ENTRE CAINDO E PULANDO
        
        if (isJumping) {
            anim.SetBool("fall", true);
            anim.SetBool("jump", false);
        } else {
            anim.SetBool("jump", false);
            anim.SetBool("fall", false);
        }
    }
}
