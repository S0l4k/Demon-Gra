using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerResourceUI : MonoBehaviour
{
    public PlayerResource playerResource;
    public Image resourceBarImage;
    public List<Sprite> resourceSprites;

    void Update()
    {
        int current = Mathf.Clamp(playerResource.currentResource, 0, resourceSprites.Count - 1);
        resourceBarImage.sprite = resourceSprites[current];
    }
}