using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI wavesText;
    // Start is called before the first frame update
    private void OnEnable()
    {
        wavesText.text = LevelManager.instance.waves.ToString();
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        EnemySpawner.instance.harder();
    }
}
