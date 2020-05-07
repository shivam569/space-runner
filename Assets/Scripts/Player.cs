using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float speed = 150f;
    public float jumpForce;
    private float horizontal;
    private float vertical;

    private SpriteRenderer renderer;
    private Rigidbody2D myBody;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        myBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal") * speed * Time.fixedDeltaTime;
        vertical = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        Move();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Spikes")
            gameObject.SetActive(false);

        if (collision.tag == "Pickables")
            collision.gameObject.SetActive(false);

    }

    private void Move()
    {
        if(!Mathf.Approximately(horizontal , 0))
        {
            renderer.flipX = (horizontal < 0);
        }

        Vector3 targetVelocity = new Vector2(horizontal, myBody.velocity.y);
        Vector3 refVelocity = Vector3.zero;
        myBody.velocity = Vector3.SmoothDamp(myBody.velocity, targetVelocity, ref refVelocity, 0.03f);
        


        if (!Mathf.Approximately(vertical, 0))
        {
            if(Mathf.Approximately(myBody.velocity.y, 0))
            {
                if(vertical < 0 != myBody.gravityScale > 0)
                {
                    myBody.gravityScale *= -1;
                }
            }
        }

        if(Mathf.Approximately(myBody.velocity.y , 0))
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Vector3 direction = (myBody.gravityScale > 0) ? Vector3.up : Vector3.down;
                myBody.AddForce(jumpForce * direction, ForceMode2D.Impulse);
            }
        }
    }

}
