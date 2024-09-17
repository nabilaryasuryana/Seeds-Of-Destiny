using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiHider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Nonaktifkan gameObject yang terhubung ke script ini
        this.gameObject.SetActive(false);
    }
}
