using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    /*private static GameManager instance;

    public static GameManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }*/

    private void Awake()
    {
        //set candy to 0
        DataManager.Instance.LevelDataObject.Candy = 0;
    }

    public void AddCandy(int candy)
    {
        DataManager.Instance.LevelDataObject.Candy += candy;
        Debug.Log("Candy: " + DataManager.Instance.LevelDataObject.Candy);
    }

    public void RemoveCandy(int candy)
    {
        DataManager.Instance.LevelDataObject.Candy -= candy;
    }





















    /*//when the game initialized, we will create three kinds of bullets pool of tower
    public GameObject bulletA;
    public GameObject bulletB;
    public GameObject bulletC;
    //public int poolSize = 20;
    private Stack<GameObject> objectPoolA;
    private Stack<GameObject> objectPoolB;
    private Stack<GameObject> objectPoolC;

    private void Start()
    {
        //create there bullets group to contain bullets
        GameObject bulletParent = new GameObject();
        bulletParent.name = "BulletParent_G";
        GameObject bulletA = new GameObject();
        bulletA.name = "BulletA_G";
        bulletA.transform.SetParent(bulletParent.transform);
        GameObject bulletB = new GameObject();
        bulletB.name = "BulletB_G";
        bulletB.transform.SetParent(bulletParent.transform);
        GameObject bulletC = new GameObject();
        bulletC.name = "BulletC_G";
        bulletC.transform.SetParent(bulletParent.transform);
        
    }

    /// <summary>
    /// get bullet from corresponding pool
    /// </summary>
    /// <param name="pool">pool you want to take bullet</param>
    /// <returns></returns>
    public GameObject GetPooledObject(Stack<GameObject> pool)
    {
        GameObject obj;
        if (pool.Count == 1)
        {
            obj = Instantiate(pool.Peek());
        }
        else
        {
            obj = objectPoolA.Pop();
        }

        obj.SetActive(true);
        return obj;
    }

    /// <summary>
    /// return back bullet
    /// </summary>
    /// <param name="obj">current bullet</param>
    /// <param name="pool">pool needed to return</param>
    public void RetrunPooledObject(GameObject obj, Stack<GameObject> pool)
    {
        pool.Push(obj);
        obj.SetActive(false);
    }*/
}