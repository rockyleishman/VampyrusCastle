using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EventDataObject", menuName = "Data/EventDataObject", order = 0)]
public class EventData : ScriptableObject
{
    [Header("Attack Events")]
    [SerializeField] public AttackEvent PlayerAttack;
    [SerializeField] public AttackEvent BasicTowerAttack;
    [SerializeField] public AttackEvent BombTowerAttack;

    [Header("Collectable Game Events")]
    [SerializeField] public GameEvent CandySpawn;
    [SerializeField] public GameEvent CandyCollect;

    [Header("Wave Game Events")]
    [SerializeField] public GameEvent WaveStart;
    [SerializeField] public GameEvent WaveNext;

    [Header("Crystal Game Events")]
    [SerializeField] public GameEvent CrystalFinish;
    [SerializeField] public GameEvent CrystalDamage;
    [SerializeField] public GameEvent CrystalDeath;

    [Header("Player Game Events")]
    [SerializeField] public GameEvent PlayerDamage;
    [SerializeField] public GameEvent PlayerHeal;
    [SerializeField] public GameEvent PlayerDeath;

    [Header("Enemy Game Events")]
    [SerializeField] public GameEvent EnemyDamage;
    [SerializeField] public GameEvent EnemyDeath;

    [Header("Enemy Spawn Game Events")]
    [SerializeField] public GameEvent SpawnEnemyBasic;
    [SerializeField] public GameEvent SpawnEnemyBasicAlt1;
    [SerializeField] public GameEvent SpawnEnemyBasicAlt2;
    [SerializeField] public GameEvent SpawnEnemyBrute;
    [SerializeField] public GameEvent SpawnEnemyBruteAlt1;
    [SerializeField] public GameEvent SpawnEnemyBruteAlt2;

    [Header("UI Game Events")]
    [SerializeField] public GameEvent GamePause;
    [SerializeField] public GameEvent GameResume;
    [SerializeField] public GameEvent BuildModeEnter;
    [SerializeField] public GameEvent BuildModeExit;
    [SerializeField] public GameEvent BuildMenuLevel0Show;
    [SerializeField] public GameEvent BuildMenuLevel1Show;
    [SerializeField] public GameEvent BuildMenuLevel2Show;
    [SerializeField] public GameEvent BuildMenuHide;
}
