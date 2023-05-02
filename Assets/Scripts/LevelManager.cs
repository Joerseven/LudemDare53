using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update]
    public List<AllyComponent> allies;
    public List<EnemyComponent> enemies;
    public ItemSpawner itemSpawner;
    public Canvas gameOverScreen;
    [SerializeField] private int levelNumber;
    private GameManager gameManager;
    void Start()
    {
        var items = GetComponentsInChildren<ItemComponent>();
        print(items.Count());
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RemoveAlly(GameObject ally)
    {
        allies.Remove(ally.GetComponent<AllyComponent>());
        Destroy(ally);
        
        if (allies.Count <= 0)
        {
            GameOver();
            
        }

        var frozenNumber = 0;
        var alive = 0;

        var items = GetComponentsInChildren<ItemComponent>(true);

        foreach (var a in allies)
        {
            if (a.frozen)
            {
                frozenNumber++;
            } 
            else if (a.TryGetComponent<RangedComponent>(out var ranged))
            {
                if (!ranged.CheckHasTargets())
                {
                    var isMelee = false;
                    foreach (var i in items)
                    {
                        if (i.itemName == "weapon")
                        {
                            isMelee = true;
                        }
                    }

                    if (isMelee)
                    {
                        alive++;
                    }
                }
                else
                {
                    alive++;
                }

            }
            else
            {
                alive++;
            }
        }
        
        print(items.Length);
        print(alive);

        if (items.Length == 0 && alive == 0)
        {
            GameOver();
        }
        
        

    }

    public void RemoveEnemy(GameObject enemy)
    {
        enemies.Remove(enemy.GetComponent<EnemyComponent>());
        Destroy(enemy);
        
        if (enemies.Count <= 0)
        {
            gameManager.NextLevel();
        }
    }
    
    public void GameOver()
    {
        gameManager.GameOver();
    }

    void WinLevel()
    {
        

    }

    void SpawnAlly()
    {
        
    }
}
