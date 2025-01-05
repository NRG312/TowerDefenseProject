using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class EnemyController : MonoBehaviour,IScoreBehaviour,IDamageable
{
    private HealthSystem _healthSystem;
    [Inject] private GameplayController _gameplayController;
    [Inject] private WaypointsController _waypoints;
    
    //How much money will gain after death
    public float gainMoney;
    
    [Header("Movement")]
    private int _indexWaypoint = 0;
    private bool _isMoving = false;
    private Vector3 _dest;
    [SerializeField] private float speed;

    [FormerlySerializedAs("hp")] [Header("EnemyParameters")]
    public float hpEnemy;
    
    public void ResetEnemy()
    {
        _healthSystem = GetComponentInChildren<HealthSystem>();
        _healthSystem.ResetHealth();
        _indexWaypoint = 0;
        _isMoving = false;
    }

    public void Score(float amount)
    {
        _gameplayController.AddMoney(amount);
    }

    public void TakeDamage(float dmg)
    {
        _healthSystem.TakeDamage(dmg);
    }
    //tutaj bedzie stworzenie zniszczony prefab
    public void EnemyDead()
    {
        ObjectsTickController.onDeathEnemy.Invoke(this);
        gameObject.SetActive(false);
    }
    //Moving Enemy
    public void Tick()
    {
        MoveEnemyForward();
        if (_healthSystem != null)
        {
            _healthSystem.FunHealthBar();
        }
    }
    private void MoveEnemyForward()
    {
        if (_isMoving == false)
        {
            _dest = _waypoints.GetTheWayPoint(_indexWaypoint);
            _isMoving = true;
        }
        else
        {
            //Moving Pos
            transform.position = Vector3.MoveTowards(transform.position, _dest, speed * Time.deltaTime);
            //Moving Rot
            Vector3 relativePos = _dest - transform.position;
            Quaternion rot = Quaternion.LookRotation(relativePos,Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation,rot, 1.5f * Time.deltaTime);
            //CheckingDist
            if (Vector3.Distance(transform.position,_dest) < 0.1f)
            {
                _indexWaypoint++;
                _isMoving = false;
            }
        }

    }

    public class Factory : PlaceholderFactory<EnemyController>
    {
        
    }
}