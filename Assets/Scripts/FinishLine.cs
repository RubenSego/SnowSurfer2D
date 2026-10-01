using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField] private float reloadDelay = 1f; //Delay before reloading the scene.
    [SerializeField] private ParticleSystem finishEffect; //Particle effect to play when the player crosses the finish line.
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has crossed the finish line!");
            finishEffect.Play();
            Invoke(nameof(ReloadScene), reloadDelay); // Reload the scene after 1 second delay
            //TO DO: You can add additional logic here, such as triggering a win condition or loading a new scene.
        }
    }
    void ReloadScene()
    {
        // Reload the current scene.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    } 
}