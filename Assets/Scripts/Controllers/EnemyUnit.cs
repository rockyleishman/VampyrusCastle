using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : MonoBehaviour
{
    public int health = 100;
    [Header("Attack")]
    public float attackDamage = 5;
    [SerializeField] private float attackSpeed = 5;
    public float attackRange = 15;
    [Range(0.5f,1.5f)]public float attackRandomDamage=1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
