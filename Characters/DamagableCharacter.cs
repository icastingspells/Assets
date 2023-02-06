using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DamagableCharacter : MonoBehaviour, IDamagable
{
    Animator animator;
    Rigidbody2D rb;
    public GameObject healthText;
   

    private void Start() {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public float Health{
        set {
            if(value < _health)
            {animator.SetTrigger("Hit");
            RectTransform textTransform = Instantiate(healthText).GetComponent<RectTransform>();
            textTransform.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position); 

            Canvas canvas = GameObject.FindObjectOfType<Canvas>();
            textTransform.SetParent(canvas.transform);
            }
             _health = value;
            
            
            if(_health <= 0) {
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
    }
    public void OnHit(float damage){
        
    }
    public void OnObjectDestroyed(){
    Destroy(gameObject);
    }
}
