using System.Collections;
using UnityEngine;

public class ScriptActivator : MonoBehaviour
{
    // Referensi ke script lain yang ingin diaktifkan atau dinonaktifkan
    [Header("Script yang ingin diaktifkan/dinonaktifkan")]
    [SerializeField] private MonoBehaviour targetScript;

    // Fungsi untuk mengaktifkan script
    public void ActivateScript()
    {
        if (targetScript != null)
        {
            targetScript.enabled = true;  // Aktifkan script
            Debug.Log(targetScript.GetType().Name + " has been activated.");
        }
        else
        {
            Debug.LogWarning("No target script assigned.");
        }
    }

    // Fungsi untuk menonaktifkan script
    public void DeactivateScript()
    {
        if (targetScript != null)
        {
            targetScript.enabled = false; // Nonaktifkan script
            Debug.Log(targetScript.GetType().Name + " has been deactivated.");
        }
        else
        {
            Debug.LogWarning("No target script assigned.");
        }
    }
}
