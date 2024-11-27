using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private GameObject healthBar;
    [SerializeField] private float health = 1f;
    public void TakeDamage(float damage)
    {
        if (health > 0)
        {
            health -= damage;
            healthBar.transform.localScale = new Vector3(1,health,1);
        }
        if (health <= 0)
        {
            healthBar.transform.localScale = new Vector3(1, 0, 1);
            DeadBot();
        }
    }

    private void DeadBot()
    {
        EnemyController enemy = GetComponentInParent<EnemyController>();
        enemy.GetComponent<IScoreBehaviour>().Score(enemy.gainMoney);
        enemy.EnemyDead();
    }
}
