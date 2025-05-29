using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public Transform cam;
    public float parallaxEffectX = 0.3f;
    public float parallaxEffectY = 0.1f; // mniejszy, by nie odci¹ga³ uwagi
    private Vector3 lastCamPosition;
    private float lengthX;

    void Start()
    {
        if (cam == null)
            cam = Camera.main.transform;

        lastCamPosition = cam.position;

        // Szerokoœæ t³a w osi X
        lengthX = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void LateUpdate()
    {
        if (cam == null) return;

        Vector3 delta = cam.position - lastCamPosition;

        // Przesuwamy t³o proporcjonalnie do ruchu kamery
        transform.position += new Vector3(delta.x * parallaxEffectX, delta.y * parallaxEffectY, 0f);

        lastCamPosition = cam.position;

        // Przeskakiwanie tylko w poziomie
        float distanceToCamX = cam.position.x - transform.position.x;

        if (Mathf.Abs(distanceToCamX) >= lengthX)
        {
            float offsetX = (distanceToCamX > 0) ? lengthX : -lengthX;
            transform.position += new Vector3(offsetX, 0f, 0f);
            lastCamPosition = cam.position;
        }
    }
}
