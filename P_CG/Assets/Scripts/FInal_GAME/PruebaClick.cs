using UnityEngine;
using UnityEngine.UI;

public class PruebaClick : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => {
            Debug.Log("CLICK DETECTADO!");
        });
    }
}
