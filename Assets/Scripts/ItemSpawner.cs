using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> itemPrefab;
    [SerializeField] private List<Transform> itemSpawnLocations;
    private List<GameObject> items;
    // Start is called before the first frame update
    void Start()
    {
        //items.Add(Instantiate(itemPrefab[0], itemSpawnLocations[0].position, Quaternion.identity));
        //Instantiate(itemPrefab[0], itemSpawnLocations[0].position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
