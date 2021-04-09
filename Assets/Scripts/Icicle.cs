using System.Collections;
using UnityEngine;

public class Icicle : MonoBehaviour
{

    public float wait = 1f;
    public float distanceBetween = 2f;

    public Rigidbody2D rb;
    public Transform target;

    private bool once = true;

    void Update()
    {
        if (Mathf.Abs(rb.position.x - target.position.x) <= distanceBetween && once)
        {
            once = false;
            AudioManager.instance.Play("Ice Cracking");
            StartCoroutine(Wait(wait));
            
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AudioManager.instance.Play("Icicle");
            Destroy(gameObject);
            AudioManager.instance.Play("Hurt");
            GameController.RestartLevel();
        }else if (other.gameObject.CompareTag("Platform"))
        {
            Destroy(gameObject);
            AudioManager.instance.Play("Icicle");
        }
    }

    IEnumerator Wait(float wait)
    {
        yield return new WaitForSeconds(wait);
        rb.gravityScale = 1;
    }
}
