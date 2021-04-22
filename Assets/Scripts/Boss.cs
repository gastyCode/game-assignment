using System.Collections;
using UnityEngine;
using Pathfinding;

public class Boss : MonoBehaviour
{
    public Transform target;
    public BossProjectile ProjectilePrefab;
    public Transform LaunchOffset;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    public float distanceBetween = 30f;
    public float time = 2f;
    public float fireTime = 1f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    bool once = true;
    bool attack = false;
    public static bool isFacingLeft = true;

    Seeker seeker;
    private Rigidbody2D rb;
    public static int lives = 3;

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
            if (once == true)
            {
                once = false;
                Vector2 move = new Vector2(0, rb.position.y) * -speed * Time.deltaTime;
                rb.AddForce(move);
                AudioManager.instance.Play("Appear");
                StartCoroutine(Wait(time));
            }
        }

        if(attack == true && lives == 3)
        {
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
        }else if(lives == 2)
        {  
            if (fireTime > 0)
            {
                fireTime -= Time.deltaTime;
            }
            else
            {
                Instantiate(ProjectilePrefab, LaunchOffset.position, transform.rotation);
                AudioManager.instance.Play("Magic");
                fireTime = 2f;
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
                ProjectilePrefab.transform.localScale = new Vector3(1f, 1f, 1f);
                isFacingLeft = false;
            }
            else if (rb.velocity.x <= -0.01f)
            {
                rb.transform.localScale = new Vector3(1f, 1f, 1f);
                ProjectilePrefab.transform.localScale = new Vector3(-1f, 1f, 1f);
                isFacingLeft = true;
            }
        }else if(lives == 1)
        {
            if (fireTime > 0)
            {
                fireTime -= Time.deltaTime;
            }
            else
            {
                Instantiate(ProjectilePrefab, LaunchOffset.position, transform.rotation);
                AudioManager.instance.Play("Magic");
                fireTime = 1f;
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
                ProjectilePrefab.transform.localScale = new Vector3(1f, 1f, 1f);
                isFacingLeft = false;
            }
            else if (rb.velocity.x <= -0.01f)
            {
                rb.transform.localScale = new Vector3(1f, 1f, 1f);
                ProjectilePrefab.transform.localScale = new Vector3(-1f, 1f, 1f);
                isFacingLeft = true;
            }
        }
    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        attack = true;
        AudioManager.instance.Play("Eye");
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Player")
        {
            Destroy(other.gameObject);
            AudioManager.instance.Play("Hurt");
            AudioManager.instance.StopPlaying("Eye");
            lives = 3;
            GameController.RestartLevel();
        }
    }
}
