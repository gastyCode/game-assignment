using UnityEngine;

public class RunningEnemy : MonoBehaviour
{
    public float enemySpeed = 10f;
    public float distanceBetween = 5f;
    public float raycastDistance;
    public Transform groundDetection;
    public Transform target;

    private Rigidbody2D rb;
    private bool isMovingRight = true;
    private bool moving = false;
    private bool once = true;

    private void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        gameObject.GetComponent<Renderer>().enabled = false;
    }

    private void Update()
    {
        if(rb.position.x - target.position.x <= distanceBetween && once == true)
        {
            once = false;
            gameObject.GetComponent<Renderer>().enabled = true;
            moving = true;
        }

        if(moving == true)
        {
            transform.Translate(Vector2.left * enemySpeed * Time.deltaTime);
        }

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
        if (other.transform.tag == "Player")
        {
            GameController.RestartLevel();
        }
    }
}
