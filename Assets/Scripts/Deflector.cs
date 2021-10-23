using UnityEngine;

public class Deflector : MonoBehaviour
{
    public Rigidbody2D rb;

    public float reflectionSpeed = 5;

    // Start is called before the first frame update
    private void Start()
    {
        var velocity = new Vector2(reflectionSpeed, 0);
        rb.AddForce(velocity);
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        ReflectProjectile(rb, collision.contacts[0].normal);
    }

    private void ReflectProjectile(Rigidbody2D rb, Vector3 reflectVector)
    {
        var velocity = Vector2.Reflect(Vector2.left * reflectionSpeed, reflectVector);
        rb.velocity = velocity;
    }
}