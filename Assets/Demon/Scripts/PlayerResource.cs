using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResource : MonoBehaviour
{
    public int currentResource = 0;
    public int maxResource = 8;

    public void AddResource(int amount)
    {
        currentResource = Mathf.Min(currentResource + amount, maxResource);
        Debug.Log("Zebrano zasób. Aktualna iloœæ: " + currentResource);
    }

    public bool CanPickup(int amount)
    {
        return currentResource + amount <= maxResource;
    }

    public bool CanUseSpecialAttack()
    {
        return currentResource >= maxResource;
    }

    public void UseSpecialAttack()
    {
        if (CanUseSpecialAttack())
        {
            currentResource = 0;
            Debug.Log("U¿yto specjalnego ataku! Zasób zresetowany do 0.");
        }
        else
        {
            Debug.Log("Za ma³o zasobów na specjalny atak!");
        }
    }
}