using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Image hpImage;
    public Sprite[] hpSprites;

    void Update()
    {
        if (playerHealth == null || hpImage == null || hpSprites == null || hpSprites.Length != 8)
            return;

        float hpPercent = (float)playerHealth.currentHealth / playerHealth.maxHealth;
        int spriteIndex = Mathf.Clamp(Mathf.FloorToInt(hpPercent * 8), 0, 7);

        hpImage.sprite = hpSprites[7 - spriteIndex];
    }
}