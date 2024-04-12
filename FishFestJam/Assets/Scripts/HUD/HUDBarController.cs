using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDBarController : MonoBehaviour
{
    [SerializeField]
    private GameObject bar;
    [SerializeField]
    private float sideBuffer;
    // Testing variable
    // [SerializeField][Range(0f,1f)]
    // float perc;
    private RectTransform barRect;
    private float width;
    // Start is called before the first frame update
    void Awake()
    {
        barRect = bar.GetComponent<RectTransform>();
        width = GetComponent<RectTransform>().rect.width;
        sideBuffer = barRect.anchorMin.x;
    }

    /// <summary>
    /// Percent of the bar to cover. Ranges from 0.0 to 1.0
    /// </summary>
    /// <param name="percent"></param>
    public void SetBarWidth(float percent)
    {
        // Debug.Log($"Percent: {percent}");
        float barLength = -width * (1 - percent);
        barRect.sizeDelta = new Vector2(barLength, barRect.rect.height);
        barRect.localPosition = new Vector2(sideBuffer + (barLength / 2.0f),0);
    }
    // private void Update() {
    //     Debug.Log($"Percent: {perc}");
    //     float barLength = -width * (1 - perc);
    //     barRect.sizeDelta = new Vector2(barLength, barRect.rect.height);
    //     barRect.localPosition = new Vector2(sideBuffer + (barLength / 2.0f),0);
    // }
}
