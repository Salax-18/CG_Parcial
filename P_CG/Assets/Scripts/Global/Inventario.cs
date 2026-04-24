using UnityEngine;
using System.Collections.Generic;

public class Inventario : MonoBehaviour
{
    public static Inventario Instance;

    [Header("Configuración UI")]
    public GameObject panelInventario;
    private bool estaAbierto = false;

    private Dictionary<string, int> items = new Dictionary<string, int>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            // Cambiamos 'gameObject' por 'transform.root.gameObject' 
            // para que se lleve a todo el PADRE (SISTEMA_GLOBAL) y sus hijos (Canvas).
            DontDestroyOnLoad(transform.root.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (panelInventario != null)
            {
                estaAbierto = !estaAbierto;
                panelInventario.SetActive(estaAbierto);
                // Si se abre, actualizamos los números
                if (estaAbierto) RefrescarUI();
            }
        }
    }

    public void AgregarItem(string nombreItem)
    {
        var (nombre, cantidad) = ParsearItem(nombreItem);

        // Limpiamos el nombre para evitar errores de mayúsculas
        string clave = nombre.ToLower().Trim();

        if (items.ContainsKey(clave))
            items[clave] += cantidad;
        else
            items[clave] = cantidad;

        Debug.Log($"Inventario: +{cantidad} {clave} (total: {items[clave]})");

        // Si el panel está abierto mientras recoges, actualiza el número al instante
        if (estaAbierto) RefrescarUI();
    }

    public void RefrescarUI()
    {
        if (panelInventario == null) return;
        // Busca todos los scripts MochilaSlot en los hijos del panel y los actualiza
        MochilaSlot[] slots = panelInventario.GetComponentsInChildren<MochilaSlot>(true);
        foreach (MochilaSlot slot in slots) slot.ActualizarSlot();
    }

    public int ObtenerCantidad(string nombre)
    {
        string clave = nombre.ToLower().Trim();
        return items.ContainsKey(clave) ? items[clave] : 0;
    }

    private (string nombre, int cantidad) ParsearItem(string input)
    {
        input = input.Trim();
        string[] partes = input.Split(' ');

        if (partes.Length >= 2 && int.TryParse(partes[0], out int cantidad))
        {
            string nombre = string.Join(" ", partes, 1, partes.Length - 1);
            return (nombre, cantidad);
        }
        return (input, 1);
    }
}