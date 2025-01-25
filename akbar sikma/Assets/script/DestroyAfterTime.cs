using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float lifetime = 20f; // Waktu hidup dalam detik

    void Start()
    {
        Destroy(gameObject, lifetime); // Hancurkan objek setelah waktu yang ditentukan
    }
}