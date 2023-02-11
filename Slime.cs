using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Slime : MonoBehaviour
{
    public string tagTarget = "Player";
    public float damage = 1;
    public float knockbackForce = 1000;
    public DetectionZone detectionZone;
    public float realSpeed = 100f;
    Rigidbody2D  rb;
 

    void Start(){
        rb = GetComponent<Rigidbody2D>();
    }
    
    public void FixedUpdate(){
        if(detectionZone.detectedObj.Count > 0){

            Vector2 direction = (detectionZone.detectedObj[0].transform.position - transform.position).normalized;
           
                realSpeed = 100;
            rb.AddForce(direction * realSpeed * Time.deltaTime); 
            }
           
        }
    
    public float stopMoving{
        set{
                realSpeed = 0;
            }
        get{
                return realSpeed;
            }
    }

    void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.tag == tagTarget){
        
        Collider2D collider = col.collider;
       IDamagable damagable = collider.GetComponent<IDamagable>();
       if(damagable != null) {
         
       
        Vector2 direction = (Vector2) (collider.transform.position - transform.position).normalized;
        Vector2 knockback = direction * knockbackForce;
        damagable.OnHit(damage, knockback);
        }
        }
    }
}
    

