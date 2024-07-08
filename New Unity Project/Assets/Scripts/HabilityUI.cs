using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HabilityUI : MonoBehaviour
{
    CanvasGroup canvasGroup;
    Image habilityImage;
    public bool faded = false;
    public float duration = 2f;
    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        habilityImage = GetComponent<Image>();
    }
    public void Fade()
    {
        habilityImage.color = new Color(255, 0, 0);
        StartCoroutine(DoFade(canvasGroup, 0,1));
    }

    public IEnumerator DoFade(CanvasGroup canvasGroup, float start, float end)
    {
        float counter = 0f;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, counter / duration);

            yield return null;
        }
        faded = !faded;
        habilityImage.color = new Color(0, 255, 0);
    }
}
