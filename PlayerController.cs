using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    bool IsMoving {
      set {
        isMoving = value;
        animator.SetBool("isMoving", isMoving);
      }
    }
    public float moveSpeed = 150f;
    public float realSpeed;
    public Collider2D CharacterCollider;
    public float sprint = 300f;
    public float idleFriction = 0.09f;
    public float maxSpeed = 8f;
    public SwordHit swordHit;
    public IEnumerator Stopdash(){
        yield return new WaitForSeconds(0.2f);
        CharacterCollider.enabled = true;
        realSpeed = moveSpeed;
      }

    Vector2 moveInput = Vector2.zero;
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;
    bool isMoving = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        realSpeed = moveSpeed;
        if(realSpeed == moveSpeed){
          CharacterCollider.enabled = true;
        }
        
    }
    void OnDash(){
      realSpeed = sprint; 
      if ( realSpeed == sprint ){
      StartCoroutine(Stopdash());
      }
    }
    
    void FixedUpdate() 
    {
        if(moveInput != Vector2.zero)
        {
          rb.velocity = Vector2.ClampMagnitude(rb.velocity +(moveInput * realSpeed *Time.deltaTime), maxSpeed); 
            if(moveInput.x < 0){
              spriteRenderer.flipX = true;
              IsMoving = true;
              animator.SetBool("MoveDown", false);
              animator.SetBool("MoveUp", false);
            } 
            else if (moveInput.x > 0) {
            spriteRenderer.flipX = false;
            IsMoving = true;
            animator.SetBool("MoveDown", false);
            animator.SetBool("MoveUp", false);
            }
            if(moveInput.y < 0){
             animator.SetBool("MoveDown", true);
              animator.SetBool("MoveUp", false);
              IsMoving = false;
            } 
            else if (moveInput.y > 0) {
              animator.SetBool("MoveDown", false);
            animator.SetBool("MoveUp", true);
            IsMoving = false;

            }
           
        } else {
          rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, idleFriction);
          IsMoving = false;
          animator.SetBool("MoveDown", false);
          animator.SetBool("MoveUp", false);
        }
    }
    
    void OnMove(InputValue value) {
        moveInput = value.Get<Vector2>(); 
    }
     void OnFire(){
      animator.SetTrigger("Attack");
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
    

  }
    
    


