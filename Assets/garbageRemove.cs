using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class garbageRemove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Triggered");
        if(other.gameObject.tag != "Player") {
            Destroy(other.gameObject);
            
        }
    }
}
