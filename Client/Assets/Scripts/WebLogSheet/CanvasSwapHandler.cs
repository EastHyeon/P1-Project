using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSwapHandler : MonoBehaviour
{
    public Canvas loginCanvas, registerCanvas;
    void Start()
    {
        loginCanvas.gameObject.SetActive(true);
        registerCanvas.gameObject.SetActive(false);
    }

    public void GoRegister()
    {
        loginCanvas.gameObject.SetActive(false);
        registerCanvas.gameObject.SetActive(true);
    }

    public void GoLogin()
    {
        loginCanvas.gameObject.SetActive(true);
        registerCanvas.gameObject.SetActive(false);
    }
}
