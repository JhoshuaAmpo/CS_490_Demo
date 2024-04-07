using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDBarController : MonoBehaviour
{
    [SerializeField]
    private GameObject bar;
    [SerializeField][Range(-800,-21)]
    private float newWidth;
    [SerializeField]
    private float sideBuffer;
    private RectTransform barRect;
    private float width;
    // amount pixels cover per percent
    private float totalBarLength;
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
        Debug.Log($"Percent: {percent}");
        float barLength = -width * (1 - percent);
        barRect.sizeDelta = new Vector2(barLength, barRect.rect.height);
        barRect.localPosition = new Vector2(sideBuffer + (barLength / 2.0f),0);
    }
    // private void Update() {
    //     barRect.sizeDelta = new Vector2(newWidth, barRect.rect.height);
    //     barRect.localPosition = new Vector2(sideBuffer + (newWidth / 2.0f),0);
    // }
}
