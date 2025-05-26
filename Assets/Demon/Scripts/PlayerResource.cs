using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResource : MonoBehaviour
{
    public int currentResource = 0;
    public int maxResource = 10;

    public void AddResource(int amount)
    {
        currentResource = Mathf.Min(currentResource + amount, maxResource);
        Debug.Log("Zebrano zasób. Aktualna iloœæ: " + currentResource);
    }

    public bool CanPickup(int amount)
    {
        return currentResource + amount <= maxResource;
    }
}