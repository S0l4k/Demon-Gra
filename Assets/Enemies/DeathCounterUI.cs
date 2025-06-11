using UnityEngine;
using UnityEngine.UI;

public class DeathCounterUI : MonoBehaviour
{
    [SerializeField] private Image deathCounterImage; // Podpiêty Image z UI
    [SerializeField] private Sprite[] counterSprites; // Sprite'y od 0 do 10

    private void Start()
    {
        UpdateDeathCounterUI();
    }

    private void Update()
    {
        UpdateDeathCounterUI();
    }

    private void UpdateDeathCounterUI()
    {
        if (DeathCount.Instance != null && counterSprites.Length > 0)
        {
            int count = Mathf.Clamp(DeathCount.Instance.GetDefeatedEnemies(), 0, 10);
            deathCounterImage.sprite = counterSprites[count];
        }
    }
}