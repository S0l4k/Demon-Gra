using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePickup : MonoBehaviour
{
    public int value = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerResource playerResource = collision.collider.GetComponent<PlayerResource>();
            if (playerResource != null && playerResource.CanPickup(value))
            {
                playerResource.AddResource(value);
                Destroy(gameObject);
            }
        }
    }
}