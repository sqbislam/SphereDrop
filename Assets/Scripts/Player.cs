using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public TimeManager timeManager;
    public Rigidbody2D rb2D;
    public float speed = 5f;
    public int clickForce = 500;
    public Text text;
    public ParticleSystem particles;
    public ParticleSystem slowParticles;
    public Image timeBar;
    private Animator animator;
    private LineRenderer line;

    private float health;
    private float horizontalPos;
    private Vector3 mouseDir;
    private Vector3 mousePos;
    private bool ispressed;
    private float timePressed;
    
    void Start() {
        line = GetComponent<LineRenderer>();
        animator = GetComponent<Animator>();
        health = 10f;
        text.text = "Health " + health;
        timePressed = 100f;
    }


    // Update is called once per frame
    void Update()
    {
        horizontalPos = Input.GetAxisRaw("Horizontal");
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
    }

    void FixedUpdate() {

        rb2D.AddForce(transform.right * horizontalPos * speed);
        if (ispressed) {

            timePressed -= 0.1f;
            timeBar.fillAmount = timePressed / 100f;
        }

    }


    void OnMouseDrag() {

        line.SetPosition(0, transform.position);
        line.SetPosition(1, mousePos);


    }


    void OnMouseDown()
    {


        ispressed = true;
        if (timePressed > 0f)
        {
            timeManager.DoSlowMotion();
            Instantiate(slowParticles, transform.position, Quaternion.identity).Play();
            
        }
        else {
            ReloadScene();
        }

    }

    void OnMouseUp() {
        if (ispressed)
        {

            timeManager.StopSlowMotion();
            slowParticles.Pause();
            //Calculate Direction Vector and AddForce to RigBody
            mouseDir = mousePos - transform.position;
            rb2D.AddForce(mouseDir * clickForce);


            ispressed = false;

            //Remove Visual Line
            line.SetPosition(0, Vector3.zero);
            line.SetPosition(1, Vector3.zero);
        }
    }


    void OnCollisionEnter2D(Collision2D collision) {
        animator.Play("Ball_jump", 0, 0.2f);

        //If other object is life then add health and destroy object
        if (collision.gameObject.name == "Life"){
            Destroy(collision.gameObject);
            health++;
        }

        //Intantiate Collsion effect
        Instantiate(particles, collision.contacts[0].point, Quaternion.identity).Play();



        text.text = "Health " + health;
        if (health > 0)
        {
            health--;
        }
        else {

            ReloadScene();
        }
        

    }


    void ReloadScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

}
