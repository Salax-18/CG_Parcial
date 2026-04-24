using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cambiar de escena

public class EnemigoInteraccion : MonoBehaviour, IInteractable
{
    [Header("Configuración de Escena")]
    public string nombreEscenaCombate = "PruebaCombate";

    public void Interact()
    {
        IniciarCombate();
    }

    private void IniciarCombate()
    {
        Debug.Log("Iniciando transición a la escena: " + nombreEscenaCombate);

        // Aquí podrías guardar la posición del jugador antes de ir al combate
        // pero por ahora, solo cargamos la escena:
        SceneManager.LoadScene(nombreEscenaCombate);
    }
}