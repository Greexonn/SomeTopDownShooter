using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleEnemyController : MonoBehaviour
{
    public float attackDistance;
    public float loseDistance;

    private NavMeshAgent _agent;
    private Animator _animator;
    private HealthController _health;
    private GameObject _currentPlayer = null;

    private enum EnemyState
    {
        Idle,
        Follow,
        Attack
    }
    private EnemyState _currentState;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _health = GetComponent<HealthController>();
        _currentState = EnemyState.Idle;
    }

    void Update()
    {
        UpdateState();
    }

    private void UpdateState()
    {
        if (_currentPlayer != null)
        {
            UpdateDestination();
            switch (_currentState)
            {
                case EnemyState.Idle:
                {
                    //on player found
                    _currentState = EnemyState.Follow;
                    //behaviour
                    SetAnimationState();
                    break;
                }
                case EnemyState.Follow:
                {
                    if (Vector3.Distance(_currentPlayer.transform.position, transform.position) <= attackDistance)
                    {
                        //on player close enough to attack
                        _currentState = EnemyState.Attack;
                        //behaviour
                        SetAnimationState();
                    }
                    else if (Vector3.Distance(_currentPlayer.transform.position, transform.position) > loseDistance)
                    {
                        _currentPlayer = null;
                    }
                    break;
                }
                case EnemyState.Attack:
                {
                    if (Vector3.Distance(_currentPlayer.transform.position, transform.position) > attackDistance)
                    {
                        //on player got away from attack
                        _currentState = EnemyState.Follow;
                        //behaviour
                        SetAnimationState();
                    }
                    break;
                }
            }
        }
        else
        {
            if (_currentState != EnemyState.Idle)
            {
                //on player lost
                _currentState = EnemyState.Idle;
                //behaviour
                _agent.SetDestination(transform.position);
                SetAnimationState();
            }
        }
    }

    private void SetAnimationState()
    {
        //clear
        _animator.SetBool("Idle", false);
        _animator.SetBool("Follow", false);
         _animator.SetBool("Attack", false);

        //set
        switch (_currentState)
        {
            case EnemyState.Idle:
            {
                _animator.SetBool("Idle", true);
                break;
            }
            case EnemyState.Follow:
            {
                _animator.SetBool("Follow", true);
                break;
            }
            case EnemyState.Attack:
            {
                _animator.SetBool("Attack", true);
                break;
            }
        }
    }

    private void UpdateDestination()
    {
        _agent.SetDestination(_currentPlayer.transform.position);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _currentPlayer = other.gameObject;
        }
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("ParticleGun"))
        {
            //behaviour on got hit
            _health.DecreaseHealth(5.0f);
        }
    }

    public void LookAtPlayer()
    {
        transform.rotation = Quaternion.LookRotation((_currentPlayer.transform.position - transform.position), Vector3.up);
    }

    public void NinjaAttack_AnimEvent()
    {
        _currentPlayer.GetComponent<HealthController>().DecreaseHealth(35);
    }
}
