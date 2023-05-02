using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlashComponent : MonoBehaviour
{
    // Start is called before the first frame update
    private SpriteRenderer sprite;
    private Material material;
    [SerializeField] private float flashTime = 0.5f;
    private static readonly int FlashValue = Shader.PropertyToID("_FlashValue");

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        material = sprite.material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamageFlash()
    {
        StartCoroutine(DamageFlasher());
    }

    private IEnumerator DamageFlasher()
    {
        float amount = 0f;
        float elapsed = 0f;
        while (elapsed < flashTime)
        {
            elapsed += Time.deltaTime;
            amount = Mathf.Lerp(1f,0f,Mathf.Sin(elapsed / flashTime * Mathf.PI / 2));
            material.SetFloat(FlashValue, amount);
            yield return null;
        }
    }
}
