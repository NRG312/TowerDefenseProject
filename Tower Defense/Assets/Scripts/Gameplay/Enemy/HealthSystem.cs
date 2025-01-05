using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private GameObject objHealth;
    private EnemyController _enemyController;
    private Slider _sliderHealth;

    public void ResetHealth()
    {
        _enemyController = GetComponentInParent<EnemyController>();
        _sliderHealth = GetComponentInChildren<Slider>();
        _sliderHealth.maxValue = _enemyController.hpEnemy;
        _sliderHealth.value = _enemyController.hpEnemy;
        objHealth.SetActive(false);
    }

    public void TakeDamage(float damage)
    {
        if (_sliderHealth.value > 0)
        {
            _sliderHealth.value -= damage;
        }
        if (_sliderHealth.value <= 0)
        {
            DeadBot();
        }
    }

    private void DeadBot()
    {
        _enemyController.GetComponent<IScoreBehaviour>().Score(_enemyController.gainMoney);
        _enemyController.EnemyDead();
    }

    public void FunHealthBar()
    {
        if (_sliderHealth.value < _sliderHealth.maxValue)
        {
            objHealth.SetActive(true);
            transform.DOLookAt(Camera.main.transform.position, 1f);
        }
    }
}
