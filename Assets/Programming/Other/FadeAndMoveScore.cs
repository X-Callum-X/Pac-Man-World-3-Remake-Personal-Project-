using UnityEngine;
using TMPro;

public class FadeAndMoveScore : MonoBehaviour
{
    public float fadeTime;
    private TextMeshProUGUI textToFade;
    public float alphaValue;
    public float fadePerSecond;

    GameObject cam;

    private void Start()
    {
        cam = GameObject.Find("Main Camera");

        textToFade = GetComponentInChildren<TextMeshProUGUI>();
        fadePerSecond = 1 / fadeTime;
        alphaValue = textToFade.color.a;
    }

    private void Update()
    {
        transform.LookAt(cam.transform.position);

        transform.Translate(Vector3.up * Time.deltaTime / 2);

        if (fadeTime > 0)
        {
            alphaValue -= fadePerSecond * Time.deltaTime;
            textToFade.color = new Color(textToFade.color.r, textToFade.color.g, textToFade.color.b, alphaValue);
            fadeTime -= Time.deltaTime;
        }

        else
        {
            Destroy(gameObject);
        }
    }
}
