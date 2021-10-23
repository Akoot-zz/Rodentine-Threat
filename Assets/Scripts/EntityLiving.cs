using UnityEngine;

public class EntityLiving : MonoBehaviour
{
    public Transform feet;
    public Rigidbody2D rb;
    public BoxCollider2D box;
    public bool onGround;
    public float health = 1;
    public LayerMask groundLayer;

    public bool walking;
    public float jumpHeight = 8;
    public float runSpeed = 5;

    protected void Start()
    {
    }

    // Update is called once per frame
    protected void Update()
    {
    }

    protected void FixedUpdate()
    {
        if (health < 0) health = 0;
        if (health > 1) health = 1;

        onGround = Physics2D.OverlapCircle(feet.position, 0.5F, groundLayer);

        walking = rb.velocity.x != 0;
    }

    public void Hurt(float amount)
    {
        health -= amount;
        if (health <= 0) Kill();
    }

    public void Kill()
    {
        // transform.position = new Vector2(0,1); // spawn him back in!!!
        health = 1;
    }

    protected void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
    }
}