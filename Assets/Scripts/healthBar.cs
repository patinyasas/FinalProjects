using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
    public Slider healthSlider;
    private EnemyStats enemyStats;

    void Start()
    {
        enemyStats = GetComponentInParent<EnemyStats>();
        healthSlider.maxValue = enemyStats.maxHP;
        healthSlider.value = enemyStats.currentHP;
    }

    void Update()
    {
        if (enemyStats != null && healthSlider.value != enemyStats.currentHP)
        {
            healthSlider.value = enemyStats.currentHP;
        }
    }
}