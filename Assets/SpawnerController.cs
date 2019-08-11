using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public GameObject[] objects;
    // Update is called once per frame
    public int count = 0;

    void FixedUpdate()
    {
        count++;

        if (count> 5000)
        {
            Vector3 pos_t = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            Vector3 pos_b = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));
            transform.position = pos_t;
            Instantiate(objects[0], transform.position, new Quaternion(0,0,90,90));
            transform.position = pos_b;
            Instantiate(objects[0], transform.position, new Quaternion(0, 0, 90, 90));
            count = 0;
        }
    }
}
