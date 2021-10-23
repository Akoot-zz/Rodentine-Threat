using UnityEngine;

public class EntityLiving : MonoBehaviour
{
    public Transform feet;
    public Rigidbody2D rb;
    public BoxCollider2D box;
    public bool onGround;
    public float health = 1;
    public LayerMask groundLayer;
    public float radius;

    public bool walking;

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

        onGround = Physics2D.OverlapCircle(feet.position, box.size.x / 2, groundLayer);

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
}