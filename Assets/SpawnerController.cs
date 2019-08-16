using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public GameObject[] objects;
    // Update is called once per frame
    public int count = 0;
    public int limit = 2000;
    void FixedUpdate()
    {
        count++;

        if (count> 2000)
        {
            Vector3 pos_t = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            Vector3 pos_b = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));
            
                float y_var = Random.Range(pos_t.y-10, pos_b.y+10);
                int i = Mathf.RoundToInt(Random.Range(0f, 2.1f));
                transform.position = new Vector3(pos_t.x, y_var, pos_t.z);
                
                Instantiate(objects[i], transform.position, Quaternion.identity);
                
           
            count = 0;
        }

        
    }

    void changeLimit() {
        limit = Random.Range(2000, 5000);

    }
}
