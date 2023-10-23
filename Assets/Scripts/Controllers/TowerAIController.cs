using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAIController : MonoBehaviour
{
    [Header("Tower Settings")]
    [SerializeField] public int CandyCost = 10;
    [SerializeField] public float AttackSpeed = 1f; //if <= 0, targeting and attacks are disabled
    private float _attackTimer;
    [SerializeField] public float TargetingRange = 6.5f;
    private DestructableObject _target;
    private bool _isSearchingForTarget;

    [Header("Events")]
    [SerializeField] public AttackEvent TowerAttackEvent;

    private void Start()
    {
        _attackTimer = 0.0f;
        _isSearchingForTarget = true;
    }

    private void Update()
    {
        //decrease attack timer
        _attackTimer -= Time.deltaTime;

        if (AttackSpeed > 0.0f)
        {
            CheckTarget();
            AttemptAttack();
        }
    }

    private void CheckTarget()
    {
        //search for new target if out of range or dead
        if (_target == null || Vector3.Distance(transform.position, _target.transform.position) > TargetingRange || !_target.IsAlive)
        {
            _target = null;
            _isSearchingForTarget = true;
        }
        else
        {
            _isSearchingForTarget = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (_isSearchingForTarget && AttackSpeed > 0.0f)
        {
            EnemyAIController enemy = other?.GetComponent<EnemyAIController>();

            if (enemy != null)
            {
                _target = enemy;
                //TODO add target priority
            }
        }
    }

    private void AttemptAttack()
    {
        if (_target != null && _attackTimer <= 0.0f)
        {
            //attack
            TowerAttackEvent.TriggerEvent(transform.position, (_target.transform.position - transform.position).normalized, _target);
            _attackTimer = 1.0f / AttackSpeed;
        }
        //else do nothing
    }
}
