using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Chest : MonoBehaviour
{

    Animator animator;
    public ChestDetectionZone detectionZone;
    public string tagTarget = "Player";
    public ItemsLoot itemsLoot;


    void Start(){
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        

        if (detectionZone.detectedObj.Count > 0)
        {
                animator.SetTrigger("IsOpen");
                
                
        }
    }
    void startLoot(){
        itemsLoot.startLoot();
    }
    void Destroy(){
       Destroy(gameObject);
    }
}

    


