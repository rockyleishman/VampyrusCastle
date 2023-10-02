using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIController : DestructableObject
{
    [Header("Movement")]
    [SerializeField] public float MovementSpeed = 4.0f;
    private int _enemyDirection;
    [SerializeField] public float PathPointReachedRadius = 1.0f;
    private PathPoint _currentPathPoint;

    [Header("Player Collision")]
    [SerializeField] public float CollisionDamage = 5.0f;
    [SerializeField] public float CollisionCooldown = 1f;
    [SerializeField] public float CollisionKnockbackDistance = 2.0f;
    [SerializeField] public float CollisionKnockbackVelocity = 20.0f;
    private float _collisionCooldownTimer;
    private bool _isCollisionReady;

    [Header("Events")]
    [SerializeField] public int MinCandyDropped = 1;
    [SerializeField] public int MaxCandyDropped = 3;

    [Header("Events")]
    [SerializeField] public GameEvent CandySpawnEvent;

    private Animator _animator;

    protected override void Start()
    {
        base.Start();

        _collisionCooldownTimer = 0.0f;
        _isCollisionReady = true;

        _animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();

        FollowPath();

        //update collision readiness
        _collisionCooldownTimer -= Time.deltaTime * SpeedMultiplier;

        if (_collisionCooldownTimer <= 0.0f)
        {
            _isCollisionReady = true;
        }
        else
        {
            _isCollisionReady = false;
        }
    }

    private void FollowPath()
    {
        //find closest path point if null
        if (_currentPathPoint == null)
        {
            PathPoint[] allPathPoints = DataManager.Instance.LevelDataObject.PathPoints;
            _currentPathPoint = allPathPoints[0];
            float currentPathPointDistance = Vector3.Distance(transform.position, _currentPathPoint.transform.position);
            foreach (PathPoint pathPoint in allPathPoints)
            {
                float pathPointDistance = Vector3.Distance(transform.position, pathPoint.transform.position);
                if (pathPointDistance < currentPathPointDistance)
                {
                    _currentPathPoint = pathPoint;
                    currentPathPointDistance = pathPointDistance;
                }
            }
        }

        //find next path point if reached path point
        if (Vector3.Distance(transform.position, _currentPathPoint.transform.position) <= PathPointReachedRadius)
        {
            if (_currentPathPoint.IsPathEnd)
            {
                //TODO: attack crystal
            }
            else
            {
                _currentPathPoint = _currentPathPoint.NextPoint;
            }
        }

        //move towards current path point + wander
        Vector3 moveDirection = (_currentPathPoint.transform.position - transform.position).normalized;
        transform.Translate(moveDirection * (MovementSpeed * Time.deltaTime * SpeedMultiplier));

        //change enemy direction
        int lastDirection = _enemyDirection;

        if (moveDirection.y < -Mathf.Cos(Mathf.PI/8))
        {
            //south
            _enemyDirection = 0;
        }
        else if (moveDirection.y < -Mathf.Sin(Mathf.PI / 8))
        {
            if (moveDirection.x < -Mathf.Sin(Mathf.PI / 8))
            {
                //southwest
                _enemyDirection = 1;
            }
            else if (moveDirection.x > Mathf.Sin(Mathf.PI / 8))
            {
                //southeast
                _enemyDirection = 7;
            }            
        }
        else if (moveDirection.y > Mathf.Cos(Mathf.PI / 8))
        {
            //north
            _enemyDirection = 4;
        }
        else if (moveDirection.y > Mathf.Sin(Mathf.PI / 8))
        {
            if (moveDirection.x < -Mathf.Sin(Mathf.PI / 8))
            {
                //northwest
                _enemyDirection = 3;
            }
            else if (moveDirection.x > Mathf.Sin(Mathf.PI / 8))
            {
                //northeast
                _enemyDirection = 5;
            }
        }
        else if (moveDirection.x < -Mathf.Cos(Mathf.PI / 8))
        {
            //west
            _enemyDirection = 2;
        }
        else if (moveDirection.x > Mathf.Cos(Mathf.PI / 8))
        {
            //east
            _enemyDirection = 6;
        }

        //apply animation
        if (lastDirection != _enemyDirection)
        {
            _animator.SetInteger("EnemyDirection", _enemyDirection);
            _animator.SetTrigger("ChangeDirection");
        }
    }

    //collision effects
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (CollisionDamage > 0.0f && _isCollisionReady)
        {
            PlayerController player = collision.collider?.GetComponent<PlayerController>();

            if (player != null)
            {
                player.DamageHP(CollisionDamage);
                player.Knockback((player.transform.position - transform.position).normalized * CollisionKnockbackDistance, CollisionKnockbackDistance / CollisionKnockbackVelocity);
                player.ActivateIFrames();
                _collisionCooldownTimer = CollisionCooldown;
            }
        }
    }

    protected override void DestroyObject()
    {
        //spawn candy
        int candyAmount = Random.Range(MinCandyDropped, MaxCandyDropped + 1);

        for (int i = 0; i < candyAmount; i++)
        {
            CandySpawnEvent.TriggerEvent(transform.position);
        }

        //despawn
        OnDespawn();
    }
}
