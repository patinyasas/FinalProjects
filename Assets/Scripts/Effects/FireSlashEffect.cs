using UnityEngine;

public class FireSlashEffect : MonoBehaviour
{
    public float duration = 1f; // Duration before the effect disappears

    void Start()
    {
        // Automatically destroy the effect after the specified duration
        Destroy(gameObject, duration);
    }
}