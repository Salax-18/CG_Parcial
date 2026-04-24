using UnityEngine;
using TMPro;

public class MochilaSlot : MonoBehaviour
{
    [Header("Configuración")]
    public string nombreDelHongo; // Ej: "hongo verde"
    public TextMeshProUGUI textoCantidad;

    public void ActualizarSlot()
    {
        if (Inventario.Instance != null && textoCantidad != null)
        {
            // Le pide al inventario la cantidad de este hongo específico
            int c = Inventario.Instance.ObtenerCantidad(nombreDelHongo);
            textoCantidad.text = c.ToString();
        }
    }
}