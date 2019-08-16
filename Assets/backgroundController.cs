using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundController : MonoBehaviour
{

    private float length, startPos;
    public float parallax_speed;


    public GameObject cam;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.y+150;
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float tempo = (cam.transform.position.x * (1 - parallax_speed));
        float dist = (cam.transform.position.x * parallax_speed);
        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);

        if (tempo > startPos + length)startPos += length;
        else if (tempo < startPos - length)startPos -= length;
       
    }
}
