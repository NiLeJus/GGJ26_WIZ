using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarShrink : MonoBehaviour {

    private const float DAMAGED_HEALTH_SHRINK_TIMER_MAX = .6f;

    private Image barImage;
    private Image damagedBarImage;
    private float damagedHealthShrinkTimer;

    private void Awake() {
        // Look for "bar" inside "Canvas"
        Transform canvasTransform = transform.Find("Canvas");
        if (canvasTransform != null) {
            barImage = canvasTransform.Find("bar").GetComponent<Image>();
            damagedBarImage = canvasTransform.Find("damagedBar").GetComponent<Image>();
        } else {
            // Fallback if Canvas is not found (maybe direct children?)
            barImage = transform.Find("bar")?.GetComponent<Image>();
            damagedBarImage = transform.Find("damagedBar")?.GetComponent<Image>();
        }
    }

    private void Update() {
        damagedHealthShrinkTimer -= Time.deltaTime;
        if (damagedHealthShrinkTimer < 0) {
            if (barImage != null && damagedBarImage != null && barImage.fillAmount < damagedBarImage.fillAmount) {
                float shrinkSpeed = 1f;
                damagedBarImage.fillAmount -= shrinkSpeed * Time.deltaTime;
            }
        }
    }

    public void OnHealthChanged(float healthNormalized) {
        if (barImage == null || damagedBarImage == null) return;

        if (healthNormalized < barImage.fillAmount) {
            damagedHealthShrinkTimer = DAMAGED_HEALTH_SHRINK_TIMER_MAX;
        } 
        else if (healthNormalized > barImage.fillAmount) {
             damagedBarImage.fillAmount = healthNormalized;
        }

        barImage.fillAmount = healthNormalized;
    }
}
