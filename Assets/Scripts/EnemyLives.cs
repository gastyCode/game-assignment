using UnityEngine;

public class EnemyLives : MonoBehaviour
{
    public int enemyHP;

    private int currentHP;
    
    private void Start()
    {
        currentHP = enemyHP;
    }

    private void Update()
    {
        if(currentHP <= 0)
        {
            Destroy(transform.parent.gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
    }
}
