using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _health = 100;
    [SerializeField] private float _attackRange = 2.5f;

    private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _zombieDie;    
    [SerializeField] private AudioClip[] _zombieGetHit;
    [SerializeField] private AudioClip[] _zombieAttack;
    private enum EnemyState
    {
        Running,
        Ragdoll
    }

    private NavMeshAgent _agent;
    private Camera _player;
    private Rigidbody[] _ragdollRigidbodies;
    private Animator _animator;
    private CharacterController _characterController;

    private EnemyState _currentState = EnemyState.Running;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _player = Camera.main;
        _ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _characterController = GetComponent<CharacterController>();
        DisableRagdoll();
    }

    private void Update()
    {
        if (GameManager._gameState == GameManager.GameState.start)
            Destroy(gameObject);
        if (GameManager._gameState != GameManager.GameState.running)
        {
            _agent.enabled = false;
            _animator.enabled = false;
            return;
        }

        if (GameManager._gameState == GameManager.GameState.running)
        {
            _agent.enabled = true;
            switch (_currentState)
            {
                case EnemyState.Running:
                    RunningBehaviour();
                    break;
                case EnemyState.Ragdoll:
                    RagdollBehaviour();
                    break;
            }
        }
    }

    public void TakeDamage(float amount)
    {
        _health -= amount;
        if (_health == 0)
            Die();
        else if (_health > 0)
        {
            _audioSource.PlayOneShot(GetAudioClip(_zombieGetHit));
            GameManager._score += 50;
        }
        else
            TriggerRagdoll(Gun._force, Gun._hitPoint);
    }

    private void Die()
    {
        _audioSource.PlayOneShot(GetAudioClip(_zombieDie));
        GameManager._score += 500;
        TriggerRagdoll(Gun._force, Gun._hitPoint);
    }

    private void DisableRagdoll()
    {
        foreach (var rigidbody in _ragdollRigidbodies)
        {
            rigidbody.isKinematic = true;
        }
        _animator.enabled = true;
        _characterController.enabled = true;
    }

    private void EnableRagdoll()
    {
        _animator.enabled = false;
        _characterController.enabled = false;
        foreach (var rigidbody in _ragdollRigidbodies)
        {
            rigidbody.isKinematic = false;
        }
    }

    private void TriggerRagdoll(Vector3 force, Vector3 hitPoint)
    {
        EnableRagdoll();

        Rigidbody hitRigidBody = FindHitRigidBody(hitPoint);
        hitRigidBody.AddForceAtPosition(force, hitPoint, ForceMode.Impulse);

        _currentState = EnemyState.Ragdoll;
    }

    private Rigidbody FindHitRigidBody(Vector3 hitPoint)
    {
        Rigidbody closestRigidBody = null;
        float closestDistance = 0;

        foreach (var rigidbody in _ragdollRigidbodies)
        {
            float distance = Vector3.Distance(rigidbody.position, hitPoint);
            if (closestRigidBody == null || distance < closestDistance)
            {
                closestRigidBody = rigidbody;
                closestDistance = distance;
            }
        }
        return closestRigidBody;
    }

    private void RunningBehaviour()
    {
        _animator.enabled = true;

        _agent.destination = _player.transform.position;
        if (_agent.remainingDistance < _agent.stoppingDistance)
        {
            _agent.updateRotation = false;
            //insert your rotation code here
            Vector3 direction = _player.transform.position - transform.position;
            direction.y = 0;
            direction.Normalize();

            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 180 * Time.deltaTime);
        }
        else
        {
            _agent.updateRotation = true;
        }

        //_currentState = EnemyState.Running;
        float distance = Vector3.Distance(_player.transform.position, transform.position);
        if (distance < _attackRange)
            _animator.SetFloat("Distance", 1);
        else if (distance > _attackRange)
            _animator.SetFloat("Distance", 3);
    }

    private void RagdollBehaviour()
    {
        if (Vector3.Distance(_player.transform.position, transform.position) > 20)
        {
            Destroy(gameObject);
            EnemySpawner._currentNo--;
        }
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
