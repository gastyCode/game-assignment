using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 4.5f;
    public int score = 5;

    void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.transform.tag == "Enemy")
        {
            ScoreManager.AddScore(score);
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }
}