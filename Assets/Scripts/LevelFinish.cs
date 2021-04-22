using System.Collections;
using UnityEngine;

public class LevelFinish : MonoBehaviour
{
    public Animator animator;
    public float animationTime = 1f;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Player")
        {
            animator.SetBool("isTouched", true);
            AudioManager.instance.Play("Win");
            StartCoroutine(Wait(animationTime));
        }
    }

    IEnumerator Wait(float animationTime)
    {
        yield return new WaitForSeconds(animationTime);
        GameController.NextScene();
    }
}
