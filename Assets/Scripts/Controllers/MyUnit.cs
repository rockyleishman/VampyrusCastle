using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyUnit : MonoBehaviour
{


    
    public int health = 100;
    //when enemy enter the collider, the tower will attack
    private Collider2D detectEnemyCollider; 
    
    //about attack variable
    [Header("Attack")]
    
    [SerializeField] private float attackSpeed = 5;
    public float attackRange = 15;
    public float coolTime = 1;

    void Start()
    {
        detectEnemyCollider = GetComponent<Collider2D>();
    }

    private void OnCollisionStay(Collision other)
    {
        //if detecting enemy, shoot it
        if (other.gameObject.CompareTag("Enemy"))
        {
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
