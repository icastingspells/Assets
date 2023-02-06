using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthText : MonoBehaviour
{

    public float timeToLive = 0.5f;
    public float floatSpeed = 30;
    float timeElapsed = 0.0f;
    public Vector3 floatDirection = new Vector3 (0, 1 ,0);
    public TextMeshProUGUI textMesh;
    Color startingColor;
    public RectTransform rTransform;

    
    void start(){
       startingColor = textMesh.color;
       textMesh.color = new Color(1,0,0);
    rTransform = GetComponent<RectTransform>();
    }
    // Update is called once per frame
    public void Update()
    {
       
        timeElapsed += Time.deltaTime;
        textMesh.color = new Color(1, startingColor.g, startingColor.b, 1 - (timeElapsed / timeToLive));
         rTransform.position += floatDirection * floatSpeed * Time.deltaTime;
        if (timeElapsed > timeToLive){
            Destroy(gameObject);
        }
    }
}
