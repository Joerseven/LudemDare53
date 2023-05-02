using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int currentLevel = 0;
    [SerializeField] private List<LevelManager> levels;
    [SerializeField] private Canvas gameOverScreen;
    [SerializeField] private Canvas winScreen;
    [SerializeField] private List<GameObject> levelPrefabs;
    public void StartGame()
    {
        levels[currentLevel].gameObject.SetActive(true);
    }

    public void NextLevel()
    {
        levels[currentLevel].gameObject.SetActive(false);
        ++currentLevel;
        if (currentLevel >= levels.Count)
        {
            winScreen.GetComponent<Canvas>().enabled = true;
            return;
        }
        levels[currentLevel].gameObject.SetActive(true);
    }

    public void ResetLevel()
    {
        //PrefabUtility.RevertPrefabInstance(levels[currentLevel].gameObject, InteractionMode.AutomatedAction);
        
        var newLevel = Instantiate(levelPrefabs[currentLevel]);
        var oldLevel = levels[currentLevel].gameObject;

        levels[currentLevel] = newLevel.GetComponent<LevelManager>();
        Destroy(oldLevel);
        
        gameOverScreen.gameObject.SetActive(false);
        levels[currentLevel].gameObject.SetActive(true);
    }

    public void GameOver()
    {
        levels[currentLevel].gameObject.SetActive(false);
        gameOverScreen.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
