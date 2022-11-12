using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Gun : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private Camera _camera;
    [SerializeField] private ParticleSystem _muzzleFlash;
    [SerializeField] private GameObject _impactEffect;
    [SerializeField] private Animator _animator;
    [SerializeField] private Text _ammoText;

    [Header("Variables")]
    [SerializeField] private int _maxAmmo = 10;
    [SerializeField] private float _reloadTime = 1f;
    [SerializeField] private float _fireRate = 15f;
    [SerializeField] private float _damage = 25f;
    [SerializeField] private float _range = 100f;
    [SerializeField] private float _maxForce;

    [Header("Sound")]
    private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _gunShot;
    [SerializeField] private AudioClip _gunReload;

    [HideInInspector] public static Vector3 _force;
    [HideInInspector] public static Vector3 _hitPoint;

    private float _nextTimeToFire = 0f;
    private int _currentAmmo;
    private bool _isReloading = false;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _currentAmmo = _maxAmmo;        
    }

    private void OnEnable()
    {
        _isReloading = false;
        _animator.SetBool("Reloading", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager._gameState != GameManager.GameState.running)
            return;
        if (_isReloading)
            return;
        _ammoText.text = _currentAmmo.ToString();
        if (_currentAmmo <= 0 || (Input.GetKeyDown(KeyCode.R) && _currentAmmo != _maxAmmo))
        {
            StartCoroutine(Reload());
            return;
        }
        if (Input.GetButton("Fire1") && Time.time >= _nextTimeToFire)
        {
            _nextTimeToFire = Time.time + 1f / _fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        _audioSource.PlayOneShot(GetAudioClip(_gunShot));
        _muzzleFlash.Play();
        _currentAmmo--;

        RaycastHit hitInfo;
        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hitInfo, _range))
        {
            Debug.Log(hitInfo.transform.name);

            Enemy enemy = hitInfo.transform.GetComponent<Collider>().GetComponentInParent<Enemy>();
            if (enemy != null)
            {
                Vector3 forceDirection = enemy.transform.position - _camera.transform.position;
                forceDirection.y = 1;
                forceDirection.Normalize();
                _force = _maxForce * forceDirection;
                _hitPoint = hitInfo.point;
                enemy.TakeDamage(_damage);
            }

            //ImpactEffect
            GameObject impactGO = Instantiate(_impactEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
            Destroy(impactGO, 2f);
        }
    }

    IEnumerator Reload()
    {

        _isReloading = true;
        _animator.SetBool("Reloading", true);
        _audioSource.PlayOneShot(_gunReload);
        yield return new WaitForSeconds(_reloadTime - .25f);
        _animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(.25f);
        _currentAmmo = _maxAmmo;
        _isReloading = false;
    }
    private AudioClip GetAudioClip(AudioClip[] clips)
    {
        int n = Random.Range(1, clips.Length);
        AudioClip clip = clips[n];
        clips[n] = clips[0];
        clips[0] = clip;
        return clip;
    }

}
