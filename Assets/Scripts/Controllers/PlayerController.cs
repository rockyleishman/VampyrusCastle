using UnityEngine;
using UnityEngine.UI;

public class PlayerController : DestructableObject
{
    [Header("Player Settings")]
    [SerializeField] [Range(0.0f, 20.0f)] public float MovementSpeed = 5.0f;
    [SerializeField] [Range(0.0f, 0.1f)] public float TimeToChangeDirection = 0.05f;
    [SerializeField] [Range(0.0f, 20.0f)] public float MaxAttackSpeed = 5.0f;
    private float _attackCooldownTimer;
    private int _playerDirection;
    private Animator _animator;

    protected override void Start()
    {
        base.Start();
        //set player reference in player data
        DataManager.Instance.PlayerDataObject.Player = this;

        //init attack cooldown timer
        _attackCooldownTimer = 0.0f;

        //init direction
        _playerDirection = 0;

        //init animator
        _animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();
        Movement();
        Attack();
        BuildMode();
        CallWave();
    }

    private void Movement()
    {
        //find direction
        Vector2 moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0.0f).normalized;

        //apply movement
        transform.Translate(moveDirection * MovementSpeed * Time.deltaTime * SpeedMultiplier);

        //change player direction (for attack direction)
        if (moveDirection.x > 0.0f)
        {
            if (moveDirection.y > 0.0f)
            {
                //northeast
                _playerDirection = 5;
            }
            else if (moveDirection.y < 0.0f)
            {
                //southeast
                _playerDirection = 7;
            }
            else
            {
                //east
                _playerDirection = 6;
            }
        }
        else if (moveDirection.x < 0.0f)
        {
            if (moveDirection.y > 0.0f)
            {
                //northwest
                _playerDirection = 3;
            }
            else if (moveDirection.y < 0.0f)
            {
                //southwest
                _playerDirection = 1;
            }
            else
            {
                //west
                _playerDirection = 2;
            }
        }
        else
        {
            if (moveDirection.y > 0.0f)
            {
                //north
                _playerDirection = 4;
            }
            else if (moveDirection.y < 0.0f)
            {
                //south
                _playerDirection = 0;
            }
        }

        //apply animation
        _animator.SetFloat("HorizontalVelocity", moveDirection.x);
        _animator.SetBool("IsMoving", moveDirection != Vector2.zero);
    }

    private void Attack()
    {
        _attackCooldownTimer -= Time.deltaTime;

        if (Input.GetButtonDown("Jump") && _attackCooldownTimer <= 0.0f)
        {
            //determine attack rotation
            Vector3 attackDirection;
            switch (_playerDirection)
            {
                case 1:
                    //SW
                    attackDirection = (Vector3.down + Vector3.left).normalized;
                    break;

                case 2:
                    //W
                    attackDirection = Vector3.left;
                    break;

                case 3:
                    //NW
                    attackDirection = (Vector3.up + Vector3.left).normalized;
                    break;

                case 4:
                    //N
                    attackDirection = Vector3.up;
                    break;

                case 5:
                    //NE
                    attackDirection = (Vector3.up + Vector3.right).normalized;
                    break;

                case 6:
                    //E
                    attackDirection = Vector3.right;
                    break;

                case 7:
                    //SE
                    attackDirection = (Vector3.down + Vector3.right).normalized;
                    break;

                default:
                    //S
                    attackDirection = Vector3.down;
                    break;
            }

            //trigger attack event
            DataManager.Instance.EventDataObject.PlayerAttack.TriggerEvent(transform.position, attackDirection, null);

            //reset cooldown
            _attackCooldownTimer = 1.0f / MaxAttackSpeed;
        }
    }

    private void BuildMode()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            if (TowerManager.Instance.IsInBuildMode)
            {
                //exit build mode
                DataManager.Instance.EventDataObject.BuildModeExit.TriggerEvent(transform.position);
            }
            else
            {
                //enter build mode
                DataManager.Instance.EventDataObject.BuildModeEnter.TriggerEvent(transform.position);
            }
        }        
    }

    private void CallWave()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (!WaveManager.Instance.IsSpawningStarted)
            {
                DataManager.Instance.EventDataObject.WaveStart.TriggerEvent(DataManager.Instance.LevelDataObject.Crystal.transform.position);
            }
            else if (!WaveManager.Instance.IsSpawningCompleted)
            {
                DataManager.Instance.EventDataObject.WaveNext.TriggerEvent(DataManager.Instance.LevelDataObject.Crystal.transform.position);
            }
        }
    }

    protected override void InitHP()
    {
        base.InitHP();

        DataManager.Instance.PlayerDataObject.MaxHP = MaxHP;
        DataManager.Instance.PlayerDataObject.CurrentHP = _currentHP;
    }

    public override void DamageHP(float hp)
    {
        base.DamageHP(hp);

        DataManager.Instance.PlayerDataObject.CurrentHP = _currentHP;

        if (_iFrameTimeLeft <= 0.0f)
        {
            DataManager.Instance.EventDataObject.PlayerDamage.TriggerEvent(transform.position);
        }
    }

    public override void HealHP(float hp)
    {
        base.HealHP(hp);

        DataManager.Instance.PlayerDataObject.CurrentHP = _currentHP;

        DataManager.Instance.EventDataObject.PlayerHeal.TriggerEvent(transform.position);
    }

    protected override void DestroyObject()
    {
        DataManager.Instance.EventDataObject.PlayerDeath.TriggerEvent(transform.position);

        Debug.Log("GAME OVER (Player Dead)");
    }
}
