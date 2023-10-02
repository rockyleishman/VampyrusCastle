using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : DestructableObject
{
    [Header("Movement")]
    [SerializeField] public float MovementSpeed = 5.0f;
    [SerializeField][Range(0.0f, 0.1f)] public float TimeToChangeDirection = 0.05f;
    private int _inputDirection;
    private float _inputDirectionTime;
    private int _playerDirection;

    [Header("Events")]
    [SerializeField] public AttackEvent PlayerAttackEvent;

    private Animator _animator;

    protected override void Start()
    {
        base.Start();

        _inputDirection = 0;
        _inputDirectionTime = 0.0f;
        _playerDirection = 0;

        _animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();

        Movement();
        Attack();
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
        if (Input.GetButtonDown("Fire1"))
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
            PlayerAttackEvent.TriggerEvent(transform.position, attackDirection, null);
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
    }

    public override void HealHP(float hp)
    {
        base.HealHP(hp);

        DataManager.Instance.PlayerDataObject.CurrentHP = _currentHP;
    }

    protected override void DestroyObject()
    {
        //TODO
        Debug.Log("Player Dead");
    }
}
