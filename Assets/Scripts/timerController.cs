//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.UIElements;

//public class timerController : MonoBehaviour
//{
//    public float timeRemaining = 60f;   // 1 minute
//    public bool timerIsRunning = true;

//    public Text timerText;               // Optional: assign in Inspector
//    public GameObject lossPanel;         // Assign your loss panel
//    public WinController winController;  // Reference to check if player won

//    void Update()
//    {
//        if (!timerIsRunning)
//            return;

//        if (timeRemaining > 0)
//        {
//            timeRemaining -= Time.deltaTime;

//            // Update UI text if assigned
//            if (timerText != null)
//            {
//                int minutes = Mathf.FloorToInt(timeRemaining / 60f);
//                int seconds = Mathf.FloorToInt(timeRemaining % 60f);
//                timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
//            }
//        }
//        else
//        {
//            timeRemaining = 0;
//            timerIsRunning = false;

//            // If the player hasn't won yet, show loss
//            if (winController != null && winController.TotalTarget() != winController.CollectedPickups())
//            {
//                if (lossPanel != null)
//                    lossPanel.SetActive(true);

//                Debug.Log("Time's up! Player loses.");
//            }
//        }
//    }

//}
using UnityEngine;
using UnityEngine.UI;

public class timerController : MonoBehaviour
{
    public float timeRemaining = 60f;   // 1 minute
    public bool timerIsRunning = true;

    public Text timerText;               // UI Text for timer
    public GameObject lossPanel;         // Assign your loss panel
    public WinController winController;  // Reference to check win

    void Update()
    {
        if (!timerIsRunning)
            return;

        if (timeRemaining > 0f)
        {
            timeRemaining -= Time.deltaTime;

            // Update UI timer text
            if (timerText != null)
            {
                int minutes = Mathf.FloorToInt(timeRemaining / 60f);
                int seconds = Mathf.FloorToInt(timeRemaining % 60f);
                timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }
        }
        else
        {
            // Time has run out
            timeRemaining = 0f;
            timerIsRunning = false;
            timerText.text = string.Format("{0:00}:{1:00}", 0f, 0f);
            // Check if player hasn't answered all questions
            if (winController != null)
            {
                int collected = winController.CollectedPickups();
                int total = winController.TotalTarget();
                Debug.Log($"collected: {collected}");
                Debug.Log($"total {total}");
                if (collected < total || collected == 0)
                {
                    Debug.Log("Time's up! Player loses.");
                    if (lossPanel != null)
                        lossPanel.SetActive(true);

                    Time.timeScale = 0f; // pause the game
                }
            }
        }
    }

    // Optional: reset and start timer
    public void StartTimer(float duration)
    {
        timeRemaining = duration;
        timerIsRunning = true;
    }

    public void StopTimer()
    {
        timerIsRunning = false;
    }
}
