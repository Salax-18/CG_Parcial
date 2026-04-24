using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BatallaFinalControl : CombateControl
{
    private const int IDX_LUPUS = 0;
    private const int IDX_HELENA = 1;

    private bool HelenaViva()
    {
        if (cantidadEnemigos <= 1) return false;
        if (enemigos.transform.childCount <= 1) return false;  // ← este es el fix
        Transform t = enemigos.transform.GetChild(IDX_HELENA);
        return t != null && t.gameObject != null;
    }

    // Sobreescribe el turno de enemigos — ahora Lupus Y Helena actúan
    protected override IEnumerator AtaqueEnemigo(int idx)
    {
        // 1. Turno de Lupus (con su lógica especial)
        yield return StartCoroutine(TurnoLupus());

        // 2. Turno propio de Helena (igual que enemigo normal, si vive)
        if (HelenaViva())
            yield return StartCoroutine(TurnoHelena());
    }

    // ── Lógica de Lupus ──────────────────────────────────────────────────────
    IEnumerator TurnoLupus()
    {
        if (cantidadEnemigos <= 0) yield break;
        Transform tLupus = enemigos.transform.GetChild(IDX_LUPUS);
        if (tLupus == null) yield break;
        Character lupus = tLupus.GetComponent<Character>();
        if (lupus == null) yield break;

        int tirada = Random.Range(1, 11);
        Debug.Log($"Lupus — tirada de éxito: {tirada}");
        yield return new WaitForSecondsRealtime(0.5f);

        bool lupusPuedeAtacar;
        bool helenaAtacaExtra;

        if (tirada >= 8 && tirada <= 9)
        {
            if (HelenaViva())
            {
                Debug.Log($"Lupus tirada {tirada} (8-9) — sigue orden, Helena no ataca extra");
                lupusPuedeAtacar = true;
                helenaAtacaExtra = false;
            }
            else
            {
                Debug.Log($"Lupus tirada {tirada} (8-9) — Helena muerta, Lupus bloqueado");
                lupusPuedeAtacar = false;
                helenaAtacaExtra = false;
            }
        }
        else // 1-7 o 10
        {
            Debug.Log($"Lupus tirada {tirada} — éxito{(HelenaViva() ? " + Helena ataca extra" : "")}");
            lupusPuedeAtacar = true;
            helenaAtacaExtra = HelenaViva();
        }

        // Ataque de Lupus
        if (lupusPuedeAtacar)
        {
            int d1 = Random.Range(1, 10);
            int d2 = Random.Range(1, 10);
            int combinado = d1 * 10 + d2;
            Debug.Log($"Lupus — dados combinados: {d1} y {d2} → {combinado}");
            yield return new WaitForSecondsRealtime(0.5f);

            if (combinado > 50 && combinado < 90)
            {
                int ataqueAleatorio = Random.Range(0, lupus.data.ataques.Length);
                Debug.Log($"Lupus — ataque exitoso ({combinado}), usando {lupus.data.ataques[ataqueAleatorio].nombreAtaque}");
                yield return StartCoroutine(AplicarDanoEnemigo(lupus, ataqueAleatorio, PlayerSelect));
            }
            else
            {
                Debug.Log($"Lupus — fuera de rango ({combinado})");
            }
        }

        // Ataque extra de Helena solicitado por Lupus
        if (helenaAtacaExtra)
        {
            Transform tHelena = enemigos.transform.GetChild(IDX_HELENA);
            if (tHelena != null)
            {
                Character helena = tHelena.GetComponent<Character>();
                if (helena != null)
                {
                    yield return new WaitForSecondsRealtime(0.5f);
                    int playerAleatorio = Random.Range(0, cantidadPlayers);
                    Debug.Log($"Helena — ataque extra (solicitado por Lupus) al Player {playerAleatorio}");
                    yield return StartCoroutine(AplicarDanoEnemigo(helena, 0, playerAleatorio));
                }
            }
        }
    }

    // ── Turno propio de Helena (igual que enemigo normal) ────────────────────
    IEnumerator TurnoHelena()
    {
        Transform tHelena = enemigos.transform.GetChild(IDX_HELENA);
        if (tHelena == null) yield break;
        Character helena = tHelena.GetComponent<Character>();
        if (helena == null) yield break;

        int dado = Random.Range(1, 11);
        Debug.Log($"Helena — dado de éxito propio: {dado}");
        yield return new WaitForSecondsRealtime(0.5f);

        if (dado < 4)
        {
            Debug.Log($"Helena falló (dado {dado} < 4)");
            yield break;
        }

        int d1 = Random.Range(1, 10);
        int d2 = Random.Range(1, 10);
        int combinado = d1 * 10 + d2;
        Debug.Log($"Helena — dados combinados: {d1} y {d2} → {combinado}");
        yield return new WaitForSecondsRealtime(0.5f);

        if (combinado > 50 && combinado < 90)
        {
            int playerAleatorio = Random.Range(0, cantidadPlayers);
            Debug.Log($"Helena — ataque propio exitoso ({combinado}) al Player {playerAleatorio}");
            yield return StartCoroutine(AplicarDanoEnemigo(helena, 0, playerAleatorio));
        }
        else
        {
            Debug.Log($"Helena — fuera de rango ({combinado})");
        }
    }

    // ── Calcular y aplicar daño de enemigo a player ──────────────────────────

    // ── Fin de batalla final ─────────────────────────────────────────────────
    protected override void VerificarYAvanzar()
    {
        if (cantidadEnemigos <= 0)
        {
            Debug.Log("=== BATALLA FINAL GANADA ===");
            SceneManager.LoadScene("Tesoro");
            return;
        }

        base.VerificarYAvanzar();
    }
}