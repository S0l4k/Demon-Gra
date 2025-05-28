using UnityEngine;

public class Platform : MonoBehaviour
{
    private Vector3 originalPosition;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    void Awake()
    {
        originalPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void Hide()
    {
        if (spriteRenderer != null) spriteRenderer.enabled = false;
        if (boxCollider != null) boxCollider.enabled = false;
    }

    public void Show()
    {
        transform.position = originalPosition;
        if (spriteRenderer != null) spriteRenderer.enabled = true;
        if (boxCollider != null) boxCollider.enabled = true;
    }
}