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
        Debug.Log("Zebrano zas�b. Aktualna ilo��: " + currentResource);
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
            Debug.Log("U�yto specjalnego ataku! Zas�b zresetowany do 0.");
        }
        else
        {
            Debug.Log("Za ma�o zasob�w na specjalny atak!");
        }
    }
}