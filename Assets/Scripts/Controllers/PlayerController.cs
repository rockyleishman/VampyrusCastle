using UnityEngine;

public class PlayerController : DestructableObject
{
    [Header("Player Settings")]
    [SerializeField] [Range(0.0f, 20.0f)] public float MovementSpeed = 5.0f;
    [SerializeField] [Range(0.0f, 0.1f)] public float TimeToChangeDirection = 0.05f;
    [SerializeField] [Range(0.0f, 20.0f)] public float MaxAttackSpeed = 5.0f;
    private float _attackCooldownTimer;
    private int _inputDirection;
    private float _inputDirectionTime;
    private int _playerDirection;

    private Animator _animator;

    protected override void Start()
    {
        base.Start();

        //set player reference in player data
        DataManager.Instance.PlayerDataObject.Player = this;

        //init attack cooldown timer
        _attackCooldownTimer = 0.0f;

        //init direction variables
        _inputDirection = 0;
        _inputDirectionTime = 0.0f;
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

        //change player direction
        int lastDirection = _playerDirection;

        if (moveDirection.x > 0.0f)
        {
            if (moveDirection.y > 0.0f)
            {
                //northeast
                _inputDirection = 5;
                _inputDirectionTime += Time.deltaTime * SpeedMultiplier;
            }
            else if (moveDirection.y < 0.0f)
            {
                //southeast
                _inputDirection = 7;
                _inputDirectionTime += Time.deltaTime * SpeedMultiplier;
            }
            else
            {
                //east
                _inputDirection = 6;
                _inputDirectionTime += Time.deltaTime * SpeedMultiplier;
            }
        }
        else if (moveDirection.x < 0.0f)
        {
            if (moveDirection.y > 0.0f)
            {
                //northwest
                _inputDirection = 3;
                _inputDirectionTime += Time.deltaTime * SpeedMultiplier;
            }
            else if (moveDirection.y < 0.0f)
            {
                //southwest
                _inputDirection = 1;
                _inputDirectionTime += Time.deltaTime * SpeedMultiplier;
            }
            else
            {
                //west
                _inputDirection = 2;
                _inputDirectionTime += Time.deltaTime * SpeedMultiplier;
            }
        }
        else
        {
            if (moveDirection.y > 0.0f)
            {
                //north
                _inputDirection = 4;
                _inputDirectionTime += Time.deltaTime * SpeedMultiplier;
            }
            else if (moveDirection.y < 0.0f)
            {
                //south
                _inputDirection = 0;
                _inputDirectionTime += Time.deltaTime * SpeedMultiplier;
            }
            else
            {
                //no directional input
                _inputDirectionTime = 0.0f;
            }
        }

        if(_inputDirection == _playerDirection)
        {
            _inputDirectionTime = 0.0f;
        }

        if (_inputDirectionTime > TimeToChangeDirection)
        {
            _playerDirection = _inputDirection;
        }

        //apply animation
        if (lastDirection != _playerDirection)
        {
            _animator.SetInteger("PlayerDirection", _playerDirection);
            _animator.SetTrigger("ChangeDirection");
        }
    }

    private void Attack()
    {
        _attackCooldownTimer -= Time.deltaTime;

        if (Input.GetButtonDown("Fire1") && _attackCooldownTimer <= 0.0f)
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

        DataManager.Instance.EventDataObject.PlayerDamage.TriggerEvent(transform.position);
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
