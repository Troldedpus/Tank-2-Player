using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveMe : MonoBehaviour {
    public float power = 2.5f;
    private Rigidbody2D rb;
    private float haxis;
    private float vaxis;
    public Camera cam;
    public Transform rocket;
    public string Horizontalinput = "Horizontal";
    public string Verticalinput = "Vertical";
    public string Firebutton = "Fire1";
    public int health = 100;
    public float score = 0;
    public Text HealthUI;
    public Text MessageUI;
    public Text ScoreUI;
    public int healthpickupvalue = 10;
    public int scorepickupvalue = 100;


    // public float Gamewidth = 60;
    // public float Gameheight = 60;
    public float minX = -25, maxX = 50, minY =-25, maxY = 25;
    public float minX2, maxX2, minY2, maxY2;


	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.inertia = 1;

        Vector3 objectsize = GetComponent<Renderer>().bounds.size;
        float viewportheight = cam.orthographicSize * 2f;
        float viewportwidth = viewportheight * cam.aspect;

        // cam limits
        minX2 = minX + viewportwidth / 2;
        maxX2 = maxX - viewportwidth / 2;
        minY2 = minY + viewportheight / 2;
        maxY2 = maxY - viewportheight / 2;

        // gameworld limits
        minX += (objectsize.x / 2);
        maxX -= (objectsize.x / 2);
        minY += (objectsize.y / 2);
        maxY -= (objectsize.y / 2);


        /*
        //work out cam limits
        minX2 = -(Gamewidth / 2) + viewportwidth / 2;
        maxX2 = (Gamewidth / 2) - viewportwidth / 2;
        minY2 = -(Gameheight / 2) + viewportheight / 2;
        maxY2 = (Gameheight / 2) - viewportheight / 2;

        //works out gameworld cam limit
        minX = -(Gamewidth / 2) + (objectsize.x / 2);
        maxX = (Gamewidth / 2) - (objectsize.x / 2);
        minY = -(Gameheight / 2) + (objectsize.y / 2);
        maxY = (Gameheight / 2) - (objectsize.y / 2);
        */
    }

    // pick up hp boost
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "hpboost" && health <= 100 - healthpickupvalue)
        {
            health += healthpickupvalue;
            Destroy(collision.gameObject);
            
        }
        // pick up scoreboost
        if (collision.gameObject.tag == "scoreboost")
        {
            score += scorepickupvalue;
            Destroy(collision.gameObject);
        }

    }

    // Update is called once per frame
    void Update () {
        haxis = Input.GetAxis(Horizontalinput);
        vaxis = Input.GetAxis(Verticalinput);

        // update score for both tanks
        ScoreUI.text = "Score " + score.ToString("0");
        HealthUI.text = "Health " + health.ToString();

        // cam.transform.position = new Vector3(transform.position.x, transform.position.y, cam.transform.position.z);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), Mathf.Clamp(transform.position.y, minY, maxY), 0);
        cam.transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX2, maxX2), Mathf.Clamp(transform.position.y, minY2, maxY2),
            cam.transform.position.z);

        // Fires the rocket in the Tanks barrel pos
        if (Input.GetButtonDown(Firebutton))
            Instantiate(rocket, transform.position + transform.up * 0.55f, transform.rotation);

        if (Input.GetKeyDown(KeyCode.Escape))
            UnityEngine.SceneManagement.SceneManager.LoadScene("Start");

	}
    

    private void FixedUpdate()
    {
        rb.AddTorque(power * -haxis, ForceMode2D.Force);
        rb.AddRelativeForce(new Vector2(0, 1) * power * -vaxis, ForceMode2D.Force);
    }

    // adds health counter, removes 5 health on impact and end game msg
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.name == "redtank" && collision.gameObject.tag == "bluerocket")
        {
            collision.gameObject.GetComponent<Fireme>().timer = 0;
            health -= 5;
            GameObject.Find("bluetank").GetComponent<MoveMe>().score += 10;
            

            HealthUI.text = "Health " + health.ToString();
            if (health <= 0)
            {
                gameObject.SetActive(false);
                MessageUI.text = "Blue Tank Wins";
            }
        }
        if (gameObject.name == "bluetank" && collision.gameObject.tag == "redrocket")
            {
            collision.gameObject.GetComponent<Fireme>().timer = 0;
            health -= 5;
            GameObject.Find("redtank").GetComponent<MoveMe>().score += 10;
            

            HealthUI.text = "Health " + health.ToString();
            if (health <= 0)
            {
                gameObject.SetActive(false);
                MessageUI.text = "Red Tank Wins";
            }
        }
    }
     
}
