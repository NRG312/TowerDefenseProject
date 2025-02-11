using UnityEngine;
using DG.Tweening;
public class DeathAnimationHovercopter : MonoBehaviour
{
    [SerializeField] private ParticleSystem fireSFX;
    [SerializeField] private ParticleSystem explosionSFX;
    [SerializeField] private GameObject helicopterModel;
    private Rigidbody _rg;
    private GameObject _createdFireSFX;

    private bool _touchGround;
    private bool _isGrounded;
    private AudioSource _audio;
    //
    private float _timer = 10f;
    private void Start()
    {
        _createdFireSFX = Instantiate(fireSFX,transform).gameObject;
        _rg = GetComponent<Rigidbody>();
        if (GetComponent<AudioSource>() != null)
        {
            _audio = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        _createdFireSFX.transform.position = helicopterModel.transform.position;
        
        if (_touchGround == false && _isGrounded == false)
        {
            transform.DORotate(Vector3.right * 3.5f * Time.deltaTime, 8f);
            _rg.AddForce(-Vector3.forward * 9f * Time.deltaTime,ForceMode.Impulse);
        }
        else if(_touchGround && _isGrounded == false)
        {
            Instantiate(explosionSFX,new Vector3(helicopterModel.transform.position.x - 1,helicopterModel.transform.position.y + 3,helicopterModel.transform.position.z),helicopterModel.transform.rotation,transform);
            _rg.isKinematic = true;
            DOTween.Clear();
            _isGrounded = true;
            PlaySound();
        }
        
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground")
        {
            _touchGround = true;
        }
    }

    private void PlaySound()
    {
        _audio.Play();
    }
}