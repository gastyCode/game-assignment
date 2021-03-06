using UnityEngine;
using Pathfinding;

public class FlyingEnemyAI : MonoBehaviour
{
    public Transform target;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    public float distanceBetween = 30f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    bool sound = true;

    Seeker seeker;
    private Rigidbody2D rb;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = transform.GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void FixedUpdate()
    {
        if (Mathf.Abs(rb.position.x - target.position.x) <= distanceBetween)
        {
            if(sound == true)
            {
                sound = false;
                AudioManager.instance.Play("Bee");
            }
            
            if (path == null)
            {
                return;
            }

            if (currentWaypoint >= path.vectorPath.Count)
            {
                reachedEndOfPath = true;
                return;
            }
            else
            {
                reachedEndOfPath = false;
            }
            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
            Vector2 force = direction * speed * Time.deltaTime;

            rb.AddForce(force);

            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

            if (distance < nextWaypointDistance)
            {
                currentWaypoint++;
            }
            
            if (rb.velocity.x >= 0.01f)
            {
                rb.transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (rb.velocity.x <= -0.01f)
            {
                rb.transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
        else
        {
            if (sound == false)
            {
                sound = true;
                AudioManager.instance.StopPlaying("Bee");
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Player")
        {
            AudioManager.instance.Play("Hurt");
            GameController.RestartLevel();
        }
    }
}
