using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public Text timerText;
    public Text speedText;
    public Rigidbody playerRigidbody; // ตัวละครที่มี Rigidbody

    private float elapsedTime = 0f;
    private bool isRunning = true;

    void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
            timerText.text = "Time: " + elapsedTime.ToString("F2") + "s";
        }

        if (playerRigidbody != null)
        {
            float speed = playerRigidbody.velocity.magnitude;
            speedText.text = "Speed: " + speed.ToString("F2") + " m/s";
        }
    }

    public void StartTimer()
    {
        isRunning = true;
        elapsedTime = 0f;
    }

    public void StopTimer()
    {
        isRunning = false;
    }
}