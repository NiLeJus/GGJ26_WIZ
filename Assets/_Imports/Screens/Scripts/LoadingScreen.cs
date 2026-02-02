using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            gameObject.SetActive(false);  // ← Disparait au keypress
        }
    }
}