using UnityEngine;

public class SignTrigger2D : MonoBehaviour
{
    public GameObject signTextObject;

    private void Start()
    {
        if (signTextObject != null)
            signTextObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (signTextObject != null)
                signTextObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (signTextObject != null)
                signTextObject.SetActive(false);
        }
    }
}