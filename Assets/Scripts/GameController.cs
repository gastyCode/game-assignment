using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    
    public Animator transition;

    public float transitionTime = 1f;

    private void Awake()
    {
        instance = this;
    }

    public static void RestartLevel()
    {
        ResetStats();
        AudioManager.instance.StopPlaying("Bee");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void NextScene()
    {
        ResetStats();
        instance.StartCoroutine(instance.LoadNextScene(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public static void QuitGame()
    {
        Application.Quit();
    }

    public static void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public static void ResetStats()
    {
        PlayerController.isMagnetEnabled = false;
        PlayerController.isFiringEnabled = false;
        ScoreManager.resetScore();
    }

    IEnumerator LoadNextScene(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }
}
