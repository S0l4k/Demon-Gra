using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KeyDoorInteraction : MonoBehaviour
{
    public enum KeyColor { Red, Blue, Green, Yellow }

    [Header("Typ obiektu")]
    public bool isPlayer = false;
    public bool isKey = false;

    [Header("Ustawienia koloru")]
    public KeyColor color;

    private HashSet<KeyColor> collectedKeys;

    void Start()
    {
        if (isPlayer)
        {
            collectedKeys = new HashSet<KeyColor>();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        KeyDoorInteraction otherComp = other.GetComponent<KeyDoorInteraction>();
        if (isPlayer && otherComp != null && otherComp.isKey)
        {
            collectedKeys.Add(otherComp.color);
            Debug.Log($"Zebrano klucz: {otherComp.color}");
            Destroy(other.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        KeyDoorInteraction otherComp = collision.collider.GetComponent<KeyDoorInteraction>();
        if (isPlayer && otherComp != null && !otherComp.isKey)
        {
            if (collectedKeys.Contains(otherComp.color))
            {
                Destroy(otherComp.gameObject);
            }
        }
    }
}