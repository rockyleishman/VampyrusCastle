using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    FollowPath,
    AttackTarget
}

public class EnemyAIController : DestructableObject
{
    [Header("Movement")]
    [SerializeField] public float MovementSpeed = 4.0f;
    //private int _enemyDirection;
    [SerializeField] public float PathPointReachedRadius = 1.0f;
    private PathPoint _currentPathPoint;
    private DestructableObject _currentTarget;

    [Header("Targeting")]
    [SerializeField] public float PlayerDetectionRange = 6.0f;
    [SerializeField] public float PlayerChaseRange = 9.0f;
    [SerializeField] public float AverageTimeBeforeScanningForTarget = 15.0f;

    [Header("Player Collision")]
    [SerializeField] public float CollisionDamage = 5.0f;
    [SerializeField] public float CollisionCooldown = 1f;
    [SerializeField] public float CollisionKnockbackDistance = 2.0f;
    [SerializeField] public float CollisionKnockbackVelocity = 20.0f;
    private float _collisionCooldownTimer;
    private bool _isCollisionReady;

    [Header("Candy")]
    [SerializeField] public int MinCandyDropped = 1;
    [SerializeField] public int MaxCandyDropped = 3;

    [Header("Events")]
    [SerializeField] public GameEvent CandySpawnEvent;

    private EnemyState _state;
    private Animator _animator;

    protected override void Start()
    {
        base.Start();

        _collisionCooldownTimer = 0.0f;
        _isCollisionReady = true;

        _state = EnemyState.FollowPath;

        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        Init();

        _collisionCooldownTimer = 0.0f;
        _isCollisionReady = true;

        _state = EnemyState.FollowPath;

        _currentPathPoint = null;
        _currentTarget = null;
    }

    protected override void Update()
    {
        base.Update();

        switch (_state)
        {
            case EnemyState.AttackTarget:
                AttackTarget();
                break;

            case EnemyState.FollowPath:
            default:
                FollowPath();
                break;
        }

        //chance to scan for player
        if (Random.Range(0.0f, AverageTimeBeforeScanningForTarget / Time.deltaTime) <= 1.0f)
        {
            DetectPlayer();
        }

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
                _currentTarget = DataManager.Instance.LevelDataObject.Crystal;
                _state = EnemyState.AttackTarget;
            }
            else
            {
                _currentPathPoint = _currentPathPoint.NextPoint;
            }
        }

        //move towards current path point + wander
        Vector3 moveDirection = (_currentPathPoint.transform.position - transform.position).normalized;
        transform.Translate(moveDirection * (MovementSpeed * Time.deltaTime * SpeedMultiplier));

        /*//change enemy direction
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
        }*/

        //apply animation
        _animator.SetFloat("HorizontalVelocity", moveDirection.x);
        _animator.SetBool("IsMoving", moveDirection != Vector3.zero);
    }

    private void DetectPlayer()
    {
        if (Vector3.Distance(transform.position, DataManager.Instance.PlayerDataObject.Player.transform.position) <= PlayerDetectionRange)
        {
            _currentTarget = DataManager.Instance.PlayerDataObject.Player;
            _state = EnemyState.AttackTarget;
        }
    }

    private void AttackTarget()
    {
        //check if player is in range
        if (_currentTarget is PlayerController && Vector3.Distance(transform.position, DataManager.Instance.PlayerDataObject.Player.transform.position) > PlayerChaseRange)
        {
            _currentPathPoint = null;
            _state = EnemyState.FollowPath;
        }

        //move towards target
        Vector3 moveDirection = (_currentTarget.transform.position - transform.position).normalized;
        transform.Translate(moveDirection * (MovementSpeed * Time.deltaTime * SpeedMultiplier));
    }

    //collision effects
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (CollisionDamage > 0.0f && _isCollisionReady)
        {
            PlayerController player = collision.collider?.GetComponent<PlayerController>();
            CrystalController crystal = collision.collider?.GetComponent<CrystalController>();

            if (player != null)
            {
                player.DamageHP(CollisionDamage);
                player.Knockback((player.transform.position - transform.position).normalized * CollisionKnockbackDistance, CollisionKnockbackDistance / CollisionKnockbackVelocity);
                SoundManager.Instance.PlayHitSound();
                player.ActivateIFrames();
                _collisionCooldownTimer = CollisionCooldown;
            }
            else if (crystal != null)
            {
                crystal.DamageHP(CollisionDamage);
                crystal.ActivateIFrames();
                _collisionCooldownTimer = CollisionCooldown;
            }
        }
    }

    public override void DamageHP(float hp)
    {
        base.DamageHP(hp);

        if (_iFrameTimeLeft <= 0.0f)
        {
            DataManager.Instance.EventDataObject.EnemyDamage.TriggerEvent(transform.position);
        }
    }

    protected override void DestroyObject()
    {
        //spawn candy
        int candyAmount = Random.Range(MinCandyDropped, MaxCandyDropped + 1);

        for (int i = 0; i < candyAmount; i++)
        {
            DataManager.Instance.EventDataObject.CandySpawn.TriggerEvent(transform.position);
        }

        //death event
        DataManager.Instance.EventDataObject.EnemyDeath.TriggerEvent(transform.position);

        //despawn
        OnDespawn();
    }
}
