using UnityEngine;

public class PlayerMovement : EntityLiving
{
    public int totalJumpsPossible = 2;

    public float runMultiplier = 2;

    public float stamina;

    public bool tired;
    public bool sprinting;

    public bool blocking;

    public int facing = 1;

    public Rigidbody2D deflector;


    private float _activeRunMultiplier;
    private int _jumpsLeft;

    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();
        _jumpsLeft = totalJumpsPossible;
    }

    // Update is called once per frame
    protected new void Update()
    {
        base.Update();

        // blocking = Input.GetButtonDown("Fire1");
        blocking = Input.GetAxisRaw("Fire1") > 0;
        if (blocking) // LOL
        {
            var position = rb.position;
            deflector.position = new Vector2(position.x - 0.9f, position.y);
        }
        else
        {
            deflector.position = new Vector2(10000, 10000);
        }

        // very cool N jump formula (do not steal)
        if (!Input.GetButtonDown("Jump") || !onGround && _jumpsLeft <= 1) return;

        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * (jumpHeight / (totalJumpsPossible - _jumpsLeft + 1)), ForceMode2D.Impulse);

        _jumpsLeft--;

        stamina -= sprinting ? 0.05F : 0;
    }

    private new void FixedUpdate()
    {
        base.FixedUpdate();

        if (stamina < 0) stamina = 0;
        if (stamina > 1) stamina = 1;

        if (onGround) _jumpsLeft = totalJumpsPossible;

        var horizontalAxis = Input.GetAxis("Horizontal");

        var currentVelocity = rb.velocity;
        facing = currentVelocity.x > 0 ? 1 : -1;

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

        // rb.AddForce(Vector2.left * (horizontalAxis * deltaSpeed), ForceMode2D.Force);
        // transform.Translate(Vector2.right * (horizontalAxis * deltaSpeed));
        rb.velocity = new Vector2(horizontalAxis * deltaSpeed, rb.velocity.y);
        // if (transform.position.y <= 0) // krill zone (underworld)
        //     Hurt(0.01F);
    }

    public new void Hurt(float amount)
    {
        base.Hurt(amount);
    }

    public new void Kill()
    {
        base.Kill();
        // transform.position = new Vector2(0,1); // spawn him back in!!!
    }
}