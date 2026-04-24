using UnityEngine;
using UnityEngine.SceneManagement;

public class ScripSceneTesoro : MonoBehaviour
{
    private bool jugadorCerca = false;

    void Update()
    {
        if (jugadorCerca && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(9); // índice 4 en Build Settings
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false;
        }
    }
}