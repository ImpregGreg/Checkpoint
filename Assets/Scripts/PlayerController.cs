using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;

    public float speed = 10;
    public float maxSpeed = 10;
    public float jumpForce = 10;
    bool jump = false;
    public bool onGround;

    // Respawn positions
    float RespawnX;
    float RespawnY;

    // Player position and Checkpoint position
    public GameObject Checkpoint;
    public GameObject ClearedCheckpoint;
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Setting the Respawn position the same as spawn position
        RespawnX = rb.position.x;
        RespawnY = rb.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            jump = true;
        }
    }

    private void FixedUpdate()
    {
        float direction = Input.GetAxis("Horizontal"); // [-1, 1] left/right
        rb.AddForce(Vector2.right * direction * speed);

        if (rb.velocity.x > maxSpeed)
        {
            rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
        }
        if (rb.velocity.x < -maxSpeed)
        {
            rb.velocity = new Vector2(-maxSpeed, rb.velocity.y);
        }

        if (jump)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jump = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DeathTrigger"))
        {
            rb.position = new Vector2(RespawnX, RespawnY);
        }

        // Respawn Position changes when touching a checkpoint
        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            float calc = collision.transform.position.y;
            RespawnX = collision.transform.position.x;
            RespawnY = calc += 1f;
            Debug.Log("respawn set");

            // Replace checkpoint with new object to indicate that it's reached
            Instantiate(ClearedCheckpoint, collision.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            onGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            onGround = false;
        }
    }


}
