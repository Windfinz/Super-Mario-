using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    private Camera cam;
    private new Collider2D collider;

    public AudioSource smallJumpSound;
    public AudioSource bigJumpSound;

    private Vector2 velocity;
    private float inputAxis;

    public float moveSpeed;
    public float maxJumpHeight;
    public float maxJumpTime;
    public float jumpForce => (2f * maxJumpHeight) / (maxJumpTime / 2f);
    public float gravity => (-2f * maxJumpHeight) / Mathf.Pow((maxJumpTime / 2f), 2);

    public bool grounded { get; private set; }
    public bool jumping { get; private set; }
    public bool sliding => (inputAxis > 0f && velocity.x < 0f) || (inputAxis < 0f && velocity.x > 0f);
    public bool running => Mathf.Abs(velocity.x) > 0.25f || Mathf.Abs(inputAxis) > 0.25f;

    Player player;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        cam = Camera.main;
        player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        rb.isKinematic = false;
        collider.enabled = true;
        velocity = Vector2.zero;
        jumping = false;
    }

    private void OnDisable()
    {
        rb.isKinematic = true;
        collider.enabled = false;
        velocity = Vector2.zero;
        jumping = false;
    }

    private void Update()
    {
        HorizontalMovement();
        grounded = rb.Raycast(Vector2.down);

        if (grounded)
        {
            Jumping();
        }

        ApplyGravity();

    }

    private void HorizontalMovement()
    {
        inputAxis = Input.GetAxis("Horizontal");
        velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * moveSpeed, moveSpeed * Time.deltaTime);

        if (rb.Raycast(Vector2.right * velocity.x))
        {
            velocity.x = 0f;
        }

        if(velocity.x > 0f)
        {
            transform.eulerAngles = Vector3.zero;

        }
        else if(velocity.x < 0f)
        {
            transform.eulerAngles = new Vector3(0f, 200f, 0f);
        }

    }

    private void ApplyGravity()
    {
        bool falling = velocity.y < 0f || !Input.GetButton("Jump");
        float multiplier = falling ? 2f : 1f;
        velocity.y += gravity * multiplier * Time.deltaTime;
        velocity.y = Mathf.Max(velocity.y, gravity / 2f);
    }

    private void Jumping()
    {
        velocity.y = Mathf.Max(velocity.y, 0f);
        jumping = velocity.y > 0f;

        if (Input.GetButtonDown("Jump") && player.big)
        {
            bigJumpSound.Play();
            velocity.y = jumpForce;
            jumping = true;
        }
        else if(Input.GetButtonDown("Jump") && player.small)
        {
            smallJumpSound.Play();
            velocity.y = jumpForce;
            jumping = true;
        }

    }

    private void FixedUpdate()
    {
        Vector2 position = rb.position;
        position += velocity * Time.fixedDeltaTime;

        Vector2 leftEdge = cam.ScreenToWorldPoint(Vector2.zero);
        Vector2 rightEdge = cam.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        position.x = Mathf.Clamp(position.x, leftEdge.x + 0.5f, rightEdge.x - 0.5f);

        rb.MovePosition(position);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {   
            if(transform.DotTest(collision.transform, Vector2.down))
            {
                velocity.y = jumpForce / 2f;
                jumping = true;
            }
        }
        else if(collision.gameObject.layer != LayerMask.NameToLayer("PowerUp"))
        {
            if(transform.DotTest(collision.transform, Vector2.up))
            {
                velocity.y = 0f;
            }
        }
        
    }
    

}
