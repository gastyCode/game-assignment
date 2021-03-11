using UnityEngine;

public class Magnet : MonoBehaviour
{
    public float coinSpeed = 20f;
    public float magnetDistance = 5f;

    private Transform player;
    private float distanceBetweenObjects;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        distanceBetweenObjects = Mathf.Abs(Vector2.Distance(transform.position, player.position));

        if (PlayerController.isMagnetEnabled == true && distanceBetweenObjects <= magnetDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, coinSpeed * Time.deltaTime);
        }
    }
    
}
