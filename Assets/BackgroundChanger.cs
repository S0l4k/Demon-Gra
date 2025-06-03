using UnityEngine;

public class BackgroundChanger : MonoBehaviour
{
    public SpriteRenderer backgroundRenderer;
    public SpriteRenderer groundRenderer;

    public Sprite newBackground;
    public Sprite newGround;

    public GameObject[] enemiesToDestroy; // ← dodaj ręcznie przez Inspector

    public void ChangeBackground()
    {
        if (backgroundRenderer != null && newBackground != null)
            backgroundRenderer.sprite = newBackground;

        if (groundRenderer != null && newGround != null)
            groundRenderer.sprite = newGround;

        // Zniszcz przeciwników
        foreach (GameObject enemy in enemiesToDestroy)
        {
            if (enemy != null)
                Destroy(enemy);
        }
    }
}
