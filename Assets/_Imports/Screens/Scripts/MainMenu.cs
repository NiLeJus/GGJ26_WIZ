using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuCanvas;  // Ce Canvas

    void Start()
    {
        menuCanvas = gameObject;  // Auto-référence si vide
    }

    public void StartGame()
    {
        menuCanvas.SetActive(false);  // Cache menu
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
