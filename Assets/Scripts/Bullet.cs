using UnityEngine;

public class Bullet : MonoBehaviour
{
    public LayerMask groundLayer;

    public Rigidbody2D rb;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void FixedUpdate()
    {
        if (Physics2D.OverlapCircle(rb.position, .25f, groundLayer)) Kill();
    }

    private void Kill()
    {
        rb.position = new Vector2(0, 0);
    }
}