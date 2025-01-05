using UnityEngine;

public class ParticleCollision : MonoBehaviour
{
    public TowerController towerController;
    private void OnParticleCollision(GameObject other)
    {
        if (other.GetComponent<IDamageable>() != null)
        {
            other.GetComponent<IDamageable>().TakeDamage(towerController.towerData.damage);
        }
    }
}