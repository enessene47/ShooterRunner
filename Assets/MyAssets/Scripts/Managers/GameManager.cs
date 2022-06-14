using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    #endregion

    public void RertyLevel() => SceneManager.LoadScene(ActiveScene);

    public void NextLevel()
    {
        int index = PlayerPrefs.GetInt("Level", 0);

        if(index == 0)
        {
            index++;

            PlayerPrefs.SetInt("Level", index);
        }

        PlayerPrefs.SetInt("FakeLevel", 1 + PlayerPrefs.GetInt("FakeLevel"));

        SceneManager.LoadScene(index);
    }

    public int ActiveScene => SceneManager.GetActiveScene().buildIndex;
}
