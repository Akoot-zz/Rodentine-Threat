using UnityEngine;

public class Enemy : EntityLiving
{
    public Transform front;
    public Transform back;
    public Transform ledge;

    public float bulletSpeed;

    public LayerMask playerLayer;

    // Start is called before the first frame update
    private new void Start()
    {
    }

    // Update is called once per frame
    protected new void Update()
    {
        base.Update();

        if (onGround && (Physics2D.OverlapCircle(front.position, .25f, groundLayer) ||
                         !Physics2D.OverlapCircle(ledge.position, 0.9f, groundLayer))) Jump();

        // if (Physics2D.OverlapCircle(front.position, .25f, playerLayer)) Shoot();
        // if (Physics2D.OverlapBox(front.position, new Vector2(playerBox.size.x, playerBox.size.y), 0)) Shoot();
    }

    protected new void FixedUpdate()
    {
        base.FixedUpdate();

        rb.velocity = new Vector2(runSpeed * Time.fixedDeltaTime, rb.velocity.y); // keep moving right forever
    }
}