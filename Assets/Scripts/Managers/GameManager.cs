using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : Singleton<GameManager>
{
    [SerializeField] [Range(0.001f, 10.0f)] public float GameOverGameTimeHalfLife = 1.0f;

    private void Awake()
    {
        //set game speed to full
        Time.timeScale = 1.0f;

        //set candy to 0
        DataManager.Instance.PlayerDataObject.Candy = 0;
    }

    public void AddCandy(int candy)
    {
        DataManager.Instance.PlayerDataObject.Candy += candy;
    }

    public void RemoveCandy(int candy)
    {
        DataManager.Instance.PlayerDataObject.Candy -= candy;
    }

    public void GameOver()
    {
        StartCoroutine(GameOverSlowDown());
    }

    private IEnumerator GameOverSlowDown()
    {
        while (Time.timeScale > 0.001f)
        {
            Time.timeScale *= 0.5f;
            yield return new WaitForSecondsRealtime(GameOverGameTimeHalfLife);
        }
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(DataManager.Instance.LevelDataObject.NextLevelBuildIndex);
    }
}