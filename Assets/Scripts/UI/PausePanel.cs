using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PausePanel : MonoBehaviour
{
    [Header("Configuration")]
    public InputAction pauseButton;
    public GameObject pausePanelScreen;
    private bool isPlaying;

    void Awake()
    {
        isPlaying = true;
    }

    private void OnEnable()
    {
        pauseButton.Enable();
        pauseButton.performed += OnPauseButtonPressed;
    }

    private void OnDisable()
    {
        pauseButton.performed -= OnPauseButtonPressed;
        pauseButton.Disable();
    }

    private void OnPauseButtonPressed(InputAction.CallbackContext context)
    {
        isPlaying = !isPlaying;
    }

    public void ResumeGame()
    {
        isPlaying = true;
    }

    void Update()
    {
        if(isPlaying)
        {
            PlayAction();
        } else
        {
            PauseAction();
        }
    }

    void PlayAction()
    {
        pausePanelScreen.SetActive(false);
    }

    void PauseAction()
    {
        pausePanelScreen.SetActive(true);
    }
}
