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
    public Text score_text;
    public ParticleSystem particles;
    public ParticleSystem slowParticles;
    public Image timeBar;
    private Animator animator;
    private LineRenderer line;

    private float health;
    public float score;
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
        rb2D.velocity = Vector2.ClampMagnitude(rb2D.velocity, speed);
        
        if (ispressed) {

            timePressed -= 0.1f;
            timeBar.fillAmount = timePressed / 100f;
        }
        if (timePressed <= 0) {
            ReloadScene();

        }
        text.text = "Health " + health;
        score_text.text = ""+ score;
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

        

        //Intantiate Collsion effect
        Instantiate(particles, collision.contacts[0].point, Quaternion.identity).Play();



        
        if (health > 0)
        {
            health--;
        }
        else {

            ReloadScene();
        }
        

    }

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Trigger");
        if (other.gameObject.tag == "hook") {

            //transform.position = other.transform.position;
            score += 50;
            rb2D.velocity = rb2D.velocity / 5;

            timePressed += 20f;
            Instantiate(particles, other.transform.position, Quaternion.identity).Play();
            Destroy(other.gameObject);
        }

        //If other object is life then add health and destroy object
        if (other.gameObject.tag == "Life")
        {
            health++;
            Destroy(other.gameObject);
            

        }
    }


    public void ReloadScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

}
