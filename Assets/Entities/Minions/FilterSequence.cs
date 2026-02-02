using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ImageSequence : MonoBehaviour
{
    [Header("Images (drag vos Image UI)")]
    [SerializeField] private GameObject image1;
    [SerializeField] private Image image2;

    [Header("Timing")]
    [SerializeField] private float displayTime = 3f;

    // Méthode publique (si besoin manuel plus tard)
    public void StartSequence()
    {
        StartCoroutine(Sequence());
    }
    
    public IEnumerator StartSequenceCoroutine()
    {
        yield return Sequence();
    }

    private IEnumerator Sequence()
    {
        // Image 1 visible 3s
        if (image1 != null) image1.gameObject.SetActive(true);
        if (image2 != null) image2.gameObject.SetActive(false);
        yield return new WaitForSeconds(displayTime);

        // Image 2 visible 3s
        if (image2 != null) image2.gameObject.SetActive(true);
        if (image1 != null) image1.gameObject.SetActive(false);
        yield return new WaitForSeconds(displayTime);

        // Tout off
        gameObject.SetActive(false);
    }
}
