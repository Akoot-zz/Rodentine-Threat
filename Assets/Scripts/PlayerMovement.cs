using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float jumpHeight;

    public float runSpeed = 5f;
    public float runMultiplier = 2f;
    public Rigidbody2D rb;
    public bool onGround = true;

    public float health = 1f;
    public float stamina = 1f;

    public bool tired;
    public bool sprinting;
    public bool walking;

    public LayerMask groundLayer;
    public Transform feet;

    private float _activeRunMultiplier;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (!Input.GetButtonDown("Jump") || !onGround) return;

        rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
        onGround = false;

        stamina -= sprinting ? 0.05F : 0;
    }

    // private void FixedUpdate()
    // {
    //     var movement = new Vector2(Input.GetAxis("Horizontal") * runSpeed, rb.velocity.y);
    //     rb.velocity = movement;
    // }

    private void FixedUpdate()
    {
        if (health < 0) health = 0;
        if (stamina < 0) stamina = 0;
        if (health > 1) health = 1;
        if (stamina > 1) stamina = 1;

        onGround = Physics2D.OverlapCircle(feet.position, 0.5F, groundLayer);

        var horizontalAxis = Input.GetAxis("Horizontal");

        walking = Math.Abs(horizontalAxis) != 0;

        if (horizontalAxis != 0 && Input.GetButton("Fire3") && !tired)
        {
            if (stamina > 0)
            {
                _activeRunMultiplier = runMultiplier;
                if (stamina >= 0.01F)
                {
                    stamina -= 0.005F;
                    sprinting = true;
                }
                else
                {
                    tired = true;
                    sprinting = false;
                }
            }
            else
            {
                tired = true;
            }
        }
        else
        {
            _activeRunMultiplier = 1.0F;
            sprinting = false;
        }

        if (stamina < 1 && !sprinting)
        {
            if (tired || walking)
            {
                if (tired && walking)
                    stamina += 0.00025F;
                else
                    stamina += 0.0005F;
            }
            else
            {
                stamina += 0.001F;
            }
        }

        if (stamina >= 0.1F) tired = false;

        if (tired) _activeRunMultiplier = 0.5F;

        var deltaSpeed = _activeRunMultiplier * runSpeed * Time.deltaTime;

        transform.Translate(Vector2.right * (horizontalAxis * deltaSpeed));

        if (transform.position.y <= 0) // krill zone (underworld)
            Hurt(0.01F);

        if (health <= 0) Die();
    }

    public void Hurt(float amount)
    {
        health -= amount;
    }

    public void Die()
    {
        // transform.position = new Vector2(0,1);
        health = 1;
    }
}