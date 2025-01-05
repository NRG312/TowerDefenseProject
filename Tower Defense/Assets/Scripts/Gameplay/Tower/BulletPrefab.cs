
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
public class BulletPrefab : MonoBehaviour
{
    [SerializeField] private float speedBullet;
    [SerializeField] private GameObject SFXExplosion;
    //
    private TowerController _towerController;
    private Rigidbody _rg;
    private MeshRenderer _mesh;
    private SphereCollider _explosionColl;
    private bool _exploded;
    //
    [Space(20f)]
    [SerializeField]private List<EnemyController> _hitEnemies;
    private void Start()
    {
        _rg = GetComponent<Rigidbody>();
        _mesh = GetComponent<MeshRenderer>();
        _explosionColl = GetComponent<SphereCollider>();
    }
    private void Update()
    {
        _rg.velocity = transform.TransformDirection(Vector3.forward * speedBullet * Time.unscaledTime);
    }
    
    public void ReplaceData(TowerController towerController)
    {
        _towerController = towerController;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground")
        {
            ExplosionHit();
        }else if (other.tag == "Enemy")
        {
            ExplosionHit();
        }
    }

    private void ExplosionHit()
    {
        if (_exploded == false)
        {
            //Creating Sfx
            GameObject sfx = Instantiate(SFXExplosion, transform.position, transform.rotation);
            Destroy(sfx,2);
            //Stopping bullet & enabling new collider
            _exploded = true;
            _rg.isKinematic = true;
            _mesh.enabled = false;
            _explosionColl.enabled = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (_exploded)
        {
            if (other.tag == "Enemy")
            {
                if (!_hitEnemies.Contains(other.GetComponent<EnemyController>()))
                {
                    _hitEnemies.Add(other.GetComponent<EnemyController>());
                    other.GetComponentInChildren<HealthSystem>().TakeDamage(_towerController.towerData.damage);
                    DestroyBullet();
                }
            }
        }
    }

    private async Task DestroyBullet()
    {
        await Task.Delay(1000);
        Destroy(gameObject);
    }
}
