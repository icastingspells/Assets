using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    
    public float moveSpeed = 150f;
    public float realSpeed;
    public Collider2D CharacterCollider;
    public float sprint = 300f;
    public float idleFriction = 0.09f;
    public float maxSpeed = 8f;
    public SwordHit swordHit;
    public float dashforce = 6f;
    public Camera cam;
    Vector2 mousePos;
    public float angle;
    public Vector2 lookDir;
    public double stamina = 3;
    public float jumpforce = 200f;
    
    

    Vector2 moveInput = Vector2.zero;
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;
    bool isMoving = false;

    bool IsMoving {
      set {
        isMoving = value;
        animator.SetBool("isMoving", isMoving);
      }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        realSpeed = moveSpeed;
    }
    
    void FixedUpdate() 
    {   
      if(stamina < 3){
        stamina += 0.5 * Time.deltaTime;
      }
      if (stamina >=1){
        animator.SetBool("Stamina",true);
      }
      else {animator.SetBool("Stamina",false);}

        if(moveInput != Vector2.zero)
        {
          rb.velocity = Vector2.ClampMagnitude(rb.velocity +(moveInput * realSpeed *Time.deltaTime), maxSpeed); 
              IsMoving = true;

            if(moveInput.x < 0){
               spriteRenderer.flipX = true;
             
            } 
            else {
              spriteRenderer.flipX = false;
            }
        } else {
          rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, idleFriction);
          IsMoving = false;

        }
        
    }
    
    void OnMove(InputValue value) {
        moveInput = value.Get<Vector2>(); 
    }
     void OnFire(){
      animator.SetTrigger("Attack");
    }
    void OnSpark(){
      
      animator.SetTrigger("Spark");
    }
    
    void OnDash(){
      rb.AddForce(Vector2.up * jumpforce);
    }

    public void SwordHit(){
      if(spriteRenderer.flipX == true){
        swordHit.AttackLeft();
      }else{
        swordHit.AttackRight();
      } 
    }
    
    public void SwordHitStop(){
      
      swordHit.AttackStop();
    }
    

    public void Attackdash(){
      if(stamina >= 1){
      if(spriteRenderer.flipX == false){
     rb.AddForce(Vector2.right * dashforce);
      }else{
        rb.AddForce(Vector2.left * dashforce);
      }
      stamina -=1;
    }
    }
    public void AttackSpark(){
      
    }
    public void rotation(){
      rb.rotation = 0;
    }

   

  }
    
    


