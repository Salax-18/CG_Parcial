using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Música por escena")]
    public AudioClip musicaMenu;
    public AudioClip musicaTavern;
    public AudioClip musicaBosque;
    public AudioClip musicaCombate;
    public AudioClip musicaBoss;
    public AudioClip musicaTesoro;

    private AudioSource audioSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneCargada;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneCargada;
    }

    void OnSceneCargada(Scene escena, LoadSceneMode mode)
    {
        switch (escena.name)
        {
            case "TransicionMenu":
            case "Menu":
            case "TransicionMenuSalida":
                TocarMusica(musicaMenu);
                break;
            case "Tavern":
                TocarMusica(musicaTavern);
                break;
            case "Bosque":
                TocarMusica(musicaBosque);
                break;
            case "Combate":
            case "CCombateBosque":
                TocarMusica(musicaCombate);
                break;
            case "Cueva":
            case "PruebaFinalCombate 1":
                TocarMusica(musicaBoss);
                break;
            case "Tesoro":
                TocarMusica(musicaTesoro);
                break;
            case "Final_Game":
                TocarMusica(null);
                break;
        }
    }

    void TocarMusica(AudioClip clip)
    {
        if (clip == null)
        {
            audioSource.Stop();
            return;
        }
        if (audioSource.clip == clip) return;
        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.Play();
    }
}