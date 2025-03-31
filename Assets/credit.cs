using UnityEngine;
using TMPro;
using System.Collections;

public class P : MonoBehaviour
{
    [SerializeField]public GameObject endCreditPanel;
    public TMP_Text creditText;
    public float scrollSpeed = 50f; // ความเร็วเลื่อนเครดิต

    private bool isPlaying = false;
    private Vector3 startPos;
    private Vector3 endPos;
    private float creditHeight;

    public P()
    {
    }

    void Start()
    {
        endCreditPanel.SetActive(false); // ซ่อนเครดิตตอนเริ่มเกม
        startPos = creditText.rectTransform.localPosition;
        creditHeight = creditText.preferredHeight;
        endPos = startPos + new Vector3(0, creditHeight + 500, 0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !isPlaying)
        {
            StartCoroutine(ShowCredits());
        }
    }

    IEnumerator ShowCredits()
    {
        isPlaying = true;
        endCreditPanel.SetActive(true);
        creditText.rectTransform.localPosition = startPos;
        Time.timeScale = 0f; // หยุดเกมชั่วคราว

        float elapsedTime = 0f;
        while (creditText.rectTransform.localPosition.y < endPos.y)
        {
            elapsedTime += Time.unscaledDeltaTime;
            creditText.rectTransform.localPosition += new Vector3(0, scrollSpeed * Time.unscaledDeltaTime, 0);
            yield return null;
        }

        yield return new WaitForSecondsRealtime(2f); // รอ 2 วิหลังเครดิตจบ

        endCreditPanel.SetActive(false);
        Time.timeScale = 1f; // ให้เกมเล่นต่อ
        isPlaying = false;
    }
}
