using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{   
    public string tagTarget = "Player";
    public List<Collider2D> detectedObj = new List<Collider2D>();
    public Collider2D col;

    void Start()
    {
        col.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == tagTarget){
             detectedObj.Add(collider);
        }
       
    }

    void OnTriggerExit2D(Collider2D collider){
        if(collider.gameObject.tag == tagTarget){
        detectedObj.Remove(collider);
        }
    }
}
