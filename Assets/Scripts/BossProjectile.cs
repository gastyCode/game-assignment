using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public float speed = 4.5f;

    private void Start()
    {
        if (Boss.isFacingLeft)
        {
            speed = -speed;
        }
    }

    void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Player")
        {
            Destroy(other.gameObject);
            AudioManager.instance.Play("Hurt");
            AudioManager.instance.StopPlaying("Eye");
            Boss.lives = 3;
            GameController.RestartLevel();
        }else if (other.transform.tag != "Boss")
        {
            Destroy(gameObject);
        }
    }
}
