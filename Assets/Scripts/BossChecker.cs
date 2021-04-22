using UnityEngine;

public class BossChecker : MonoBehaviour
{
    public GameObject boss;

    void Start()
    {
        
    }

    void Update()
    {
        if(boss == null)
        {
            GameController.NextScene();
        }
    }
}
