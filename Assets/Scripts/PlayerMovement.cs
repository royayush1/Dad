using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 5f;
    public float minimumMoveSpeed = 1f;
    public float rateOfSpeed = 0.5f;
    public float rateOfColorChange = 0.5f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    public Rigidbody2D rb;
    public Animator animator;
    Vector2 movement;   // Stores x and y
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    //new color
    private float colorChangeTimer = 0f;
    public float colorChangeInterval = 0.01f; // How frequently the color changes (in seconds).
    public float greenIncreaseAmount = 0.01f;
    SpriteRenderer m_SpriteRenderer;
    Color m_NewColor;
    float m_Red, m_Blue, m_Green;

    //timer
    private float timer;

    public void Start()
    { 
        m_SpriteRenderer= GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        m_SpriteRenderer.color = Color.white; // Start with white color
        //moveSpeed = minimumMoveSpeed;
        timer = 0f; // Initialize timer
    }

    
    // Update is called once per frame
    void Update()
    {
        // Handle input here
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        // Update color over time
        colorChangeTimer += Time.deltaTime;
        if (colorChangeTimer >= colorChangeInterval)
        {
            colorChangeTimer = 0f; // Reset timer
            Color currentColor = m_SpriteRenderer.color;
            float newGreen = Mathf.Clamp(currentColor.g + greenIncreaseAmount, 0f, 1f);
            // Adjusting the red and blue components slightly to make the green more noticeable
            float newRed = Mathf.Clamp(currentColor.r - greenIncreaseAmount / 2, 0f, 1f);
            float newBlue = Mathf.Clamp(currentColor.b - greenIncreaseAmount / 2, 0f, 1f);
            m_SpriteRenderer.color = new Color(newRed, newGreen, newBlue, 1f);
        }
        Debug.Log("spepeeeeeeeddd");
        // Slow down movement over time
        if (moveSpeed > minimumMoveSpeed)
        {
            moveSpeed -= rateOfSpeed * Time.deltaTime;
            Debug.Log("movespeed change = -" + moveSpeed);
            moveSpeed = Mathf.Max(moveSpeed, minimumMoveSpeed);
        }

    }

    // Executed on a fixed timer, instead of frame
    // By default, called 50 times/s
    private void FixedUpdate()
    {
        // Handle movements here
        // rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        bool success = MovePlayer(movement);

        if (!success)
        {
            // Try left/right
            success = MovePlayer(new Vector2(movement.x, 0));
        }

        if (!success)
        {
            success = MovePlayer(new Vector2(0, movement.y));
        }
    }

    public bool MovePlayer(Vector2 direction)
    {
        // Check for potential collisions
        int count = rb.Cast(
            direction,
            movementFilter,
            castCollisions,
            moveSpeed * Time.fixedDeltaTime + collisionOffset);

        if (count == 0)
        {
            Vector2 moveVector = direction * moveSpeed * Time.fixedDeltaTime;

            // No collisions
            rb.MovePosition(rb.position + moveVector);
            return true;
        }

        else
        {
            return false;
        }
    }
}
