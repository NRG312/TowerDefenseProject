using System;
using UnityEngine;

public class DeathAnimationVehicle : MonoBehaviour
{
    [SerializeField] private ParticleSystem fireSFX;
    [SerializeField] private ParticleSystem explosionSFX;
    [SerializeField] private GameObject turretObject;
    private Rigidbody _rg;
    private bool _addedForce;
    private float _timer = 10;
    private void Start()
    {
        Instantiate(fireSFX, transform.position, transform.rotation,transform);
        Instantiate(explosionSFX, turretObject.transform.position, turretObject.transform.rotation,transform);
        _rg = turretObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_addedForce == false)
        {
            _rg.AddForce(0,4,-2.5f,ForceMode.Impulse);
            _addedForce = true;
        }

        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            Destroy(gameObject);
        }
    }
}