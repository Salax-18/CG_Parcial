using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class FinJDelJuego : MonoBehaviour
{
    public GameObject pantallaInicial;
    public RawImage videoScreen;
    public VideoPlayer videoPlayer;

    void Start()
    {
        videoScreen.gameObject.SetActive(false);
        videoPlayer.loopPointReached += OnVideoTerminado;
    }

    public void MostrarVideo()
    {
        pantallaInicial.SetActive(false);
        videoScreen.gameObject.SetActive(true);
        videoPlayer.Play();
    }

    void OnVideoTerminado(VideoPlayer vp)
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}