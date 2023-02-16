using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHit : MonoBehaviour
{
    public Collider2D AttackCollider;
    public float damage = 3;
    public float knockbackForce = 1;
    Vector2 rightAttackOffset;
    
    
    private void Start(){
        rightAttackOffset = transform.localPosition;
        AttackCollider.enabled = false;
    }

    public void AttackRight() {
        AttackCollider.enabled = true;
        transform.localPosition = rightAttackOffset;
    }
    public void AttackLeft() {
        AttackCollider.enabled = true;
        transform.localPosition = new Vector3(rightAttackOffset.x * -1, rightAttackOffset.y);
    }
    public void AttackStop() {
        AttackCollider.enabled = false;
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {   
        IDamagable damagableObject = (IDamagable) collider.GetComponent<IDamagable>();
        if(damagableObject != null){
          

        Vector3 parentPosition = transform.parent.position;
        Vector2 direction = (Vector2) (collider.gameObject.transform.position - parentPosition).normalized;
        Vector2 knockback = direction * knockbackForce;
        damagableObject.OnHit(damage, knockback);
        }
    }
}
