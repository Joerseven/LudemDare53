using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FrogUI : MonoBehaviour
{
    public List<Sprite> items;

    public Sprite frogOpen;

    public Sprite frogClose;

    public GameObject item;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowItem(string name)
    {
        switch (name)
        {
            case "Blowpipe":
                item.GetComponent<SpriteRenderer>().sprite = items[0];
                break; 
            case "Weapon":
                item.GetComponent<SpriteRenderer>().sprite = items[1];
                break;
            case "Unfreeze":
                item.GetComponent<SpriteRenderer>().sprite = items[2];
                break;
        }
        
        item.SetActive(true);
        GetComponent<SpriteRenderer>().sprite = frogOpen;

    }

    public void RemoveItem()
    {
        item.SetActive(false);
        GetComponent<SpriteRenderer>().sprite = frogClose;
    }
}
