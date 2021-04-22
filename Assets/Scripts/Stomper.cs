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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Hitbox")
        {
            AudioManager.instance.Play("Enemy");
            AudioManager.instance.StopPlaying("Bee");
            other.gameObject.GetComponent<EnemyLives>().TakeDamage(damage);
            rb.AddForce(transform.up * bounceForce, ForceMode2D.Impulse);
            ScoreManager.AddScore(score);
        }else if(other.gameObject.tag == "BossHitbox")
        {
            AudioManager.instance.Play("Enemy");
            Boss.lives--;
            other.gameObject.GetComponent<EnemyLives>().TakeDamage(damage);
            rb.AddForce(transform.up * bounceForce, ForceMode2D.Impulse);
        }
    }
}
