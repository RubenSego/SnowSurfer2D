using UnityEngine;

public class CrashDetector : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        int layerIndex = LayerMask.NameToLayer("Floor");

        if (other.gameObject.layer == layerIndex)
        {
            Debug.Log("Player has crashed!");
        }
    }
}
