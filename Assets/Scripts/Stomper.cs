using UnityEngine;

public class Stomper : MonoBehaviour
{
    public int damage;
    public int score;
    public float bounceForce;

    private Rigidbody2D rb;
    
    private void Start()
    {
        rb = transform.parent.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Hitbox")
        {
            AudioManager.instance.Play("Enemy");
            other.gameObject.GetComponent<EnemyLives>().TakeDamage(damage);
            rb.AddForce(transform.up * bounceForce, ForceMode2D.Impulse);
            ScoreManager.AddScore(score);
        }
    }
}
