using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [Header("ScoreUI")]
    public TextMeshProUGUI textPoints;
    
    [Header("MagnetUI")]
    public Image magnetImage;
    public TextMeshProUGUI magnetTimeText;
    public float magnetTime = 10;
    public int magnetPrice = 20;

    private float magnetTimeRemaining;

    [Header("FiringUI")]
    public Image firingImage;
    public TextMeshProUGUI firingTimeText;
    public float firingTime = 15;
    public int firingPrice = 30;

    private float firingTimeRemaining;

    public static int score = 0;

    private void Start()
    {
        magnetImage.enabled = false;
        firingImage.enabled = false;
        magnetTimeRemaining = magnetTime;
        firingTimeRemaining = firingTime;
    }

    public void Update()
    {
        textPoints.text = "<sprite index= 0>x" + score.ToString();

        if (score >= magnetPrice && PlayerController.isMagnetEnabled == false)
        {
            magnetImage.enabled = true;
        }
        else if (PlayerController.isMagnetEnabled == true)
        {
            magnetTimeRemaining -= Time.deltaTime;
            magnetTimeText.text = (magnetTimeRemaining).ToString("0");
            if (magnetTimeRemaining < 0)
            {
                PlayerController.isMagnetEnabled = false;
                magnetTimeText.text = "";
                magnetImage.enabled = false;
                magnetTimeRemaining = magnetTime;
            }
        }
        else
        {
            magnetImage.enabled = false;
        }

        if (score >= firingPrice && PlayerController.isFiringEnabled == false)
        {
            firingImage.enabled = true;
        }
        else if (PlayerController.isFiringEnabled == true)
        {
            firingTimeRemaining -= Time.deltaTime;
            firingTimeText.text = (firingTimeRemaining).ToString("0");
            if (firingTimeRemaining < 0)
            {
                PlayerController.isFiringEnabled = false;
                firingTimeText.text = "";
                firingImage.enabled = false;
                firingTimeRemaining = firingTime;
            }
        }
        else
        {
            firingImage.enabled = false;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.tag == "Points")
        {
            AudioManager.instance.Play("Coin");
            AddScore(1);
            Destroy(other.gameObject);
        }
    }

    public static void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
    }

    public static void resetScore()
    {
        score = 0;
    }
}
