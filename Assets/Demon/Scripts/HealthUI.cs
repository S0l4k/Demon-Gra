using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Image hpFillImage;
    public Animator hpAnimator;

    void Update()
    {
        if (playerHealth != null && hpFillImage != null)
        {
            float fill = (float)playerHealth.currentHealth / playerHealth.maxHealth;
            hpFillImage.fillAmount = fill;

            //Debug.Log("HP fill: " + fill);

            if (hpAnimator != null)
            {
                hpAnimator.SetFloat("HP", fill);
                //Debug.Log("Animator HP parameter: " + fill);
            }
            else
            {
                Debug.LogWarning("HP animacja jest pusta");
            }
        }
    }
}