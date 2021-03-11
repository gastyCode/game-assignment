using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float enemySpeed;
    public float raycastDistance;
    public Transform groundDetection;

    private bool isMovingRight = true;

    void Update()
    {
        transform.Translate(Vector2.right * enemySpeed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, raycastDistance);
        if (!groundInfo.collider)
        {
            if (isMovingRight)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                isMovingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                isMovingRight = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.transform.tag == "Player")
        {
            AudioManager.instance.Play("Hurt");
            GameController.RestartLevel();
        }
    }
}