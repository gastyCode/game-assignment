using UnityEngine;
using System.Collections;

public class CreditsScene : MonoBehaviour
{
    public float animationTime = 20f;
    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        AudioManager.instance.Play("Song");
        StartCoroutine(Wait(animationTime));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            AudioManager.instance.StopPlaying("Song");
            GameController.GoToMenu();
        }
    }

    IEnumerator Wait(float animationTime)
    {
        yield return new WaitForSeconds(animationTime);
        AudioManager.instance.StopPlaying("Song");
        GameController.GoToMenu();
    }
}
