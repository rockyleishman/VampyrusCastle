using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AttackSpell : MonoBehaviour
{
    public enum Spell
    {
        normal,
        SpecailA,
        SpecailB
    }
    public float baseAttackDamage = 5;
    public float finalAttackDamage=1;
    private float attackRandomDamage;
    public Spell thisSpell= Spell.normal;
    
    // Start is called before the first frame update
    void Start()
    {
        //different spell will affect by different effect

        attackRandomDamage = Random.Range(0.8f, 1.2f);
        // get final damage value at random
        finalAttackDamage = baseAttackDamage * attackRandomDamage;
    }


}
