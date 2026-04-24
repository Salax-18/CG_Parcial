using UnityEngine;

public class FInal_Game_Salida : MonoBehaviour
{
    public void SalirJuego()
    {
        Debug.Log("CLICK DETECTADO");

        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}