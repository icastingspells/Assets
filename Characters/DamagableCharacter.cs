using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DamagableCharacter : MonoBehaviour, IDamagable
{
    Animator animator;
    Rigidbody2D rb;
    public Collider2D collider;
    public Healthbar healthBar;

    private void Start() {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        healthBar.SetMaxHealth(_health);
        collider.enabled = true;
    }

    public float Health{
        set {
            if(value < _health)
            {animator.SetTrigger("Hit");
            
            }
             _health = value;
            
            
            if(_health <= 0) {
                collider.enabled = false;
                animator.SetTrigger("Defeated");
            }
        }
        
        get {
            return _health;
        }
    }
    public float _health = 9;
    
    public void OnHit(float damage, Vector2 knockback){
        Health -= damage;
        rb.AddForce(knockback);   
        healthBar.SetHealth(Health); 
        
    }
    public void OnHit(float damage){
         
    }
    public void OnObjectDestroyed(){
    Destroy(gameObject);
    }
    
        
   
}
