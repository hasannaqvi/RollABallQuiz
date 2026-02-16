//using UnityEngine;
//using UnityEngine.SceneManagement;

//public class WinController : MonoBehaviour
//{
//    public GameObject winPanel;
//    private int totalDanger;
//    private int totalTarget;
//    private int collectedPickups = 0;


//    public int CollectedPickups() => collectedPickups;
//    public int TotalTarget() => totalTarget;
//    public void InitializeCounts(int danger, int target)
//    {
//        totalDanger = danger;
//        totalTarget = target;

//        Debug.Log("Total danger found: " + totalDanger);
//        Debug.Log("Total targets found: " + totalTarget);
//    }

//    public void CheckWinCondition(int collectedPickups)
//    {
//        //collectedPickups++;
//        //Debug.Log("Collected: " + collectedPickups + " / " + totalPickups);

//        if (collectedPickups == totalTarget)
//        {
//            Debug.Log("WIN CONDITION REACHED");
//            winPanel.SetActive(true);   // ← SHOW THE PANEL
//            //ShowWinScreen();
//            Time.timeScale = 0f;        // pause game
//        }
//    }
//    private void ShowWinScreen()
//    {
//        CanvasGroup cg = winPanel.GetComponent<CanvasGroup>();
//        cg.alpha = 1f;
//        cg.interactable = true;
//        cg.blocksRaycasts = true;

//        Time.timeScale = 0f;
//    }


//    public void PlayAgain()
//    {
//        Time.timeScale = 1f;
//        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
//    }

//    public void BackToMenu()
//    {
//        Time.timeScale = 1f;
//        SceneManager.LoadScene("Menu");
//    }
//}
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinController : MonoBehaviour
{
    public GameObject winPanel;

    private int totalQuestions;      // Total number of questions / cubes
    private int collectedAnswers = 0;

    // Initialize total number of questions (called from PlayerController if needed)
    public void InitializeTotalQuestions(int total)
    {
        totalQuestions = total;
        collectedAnswers = 0;

        Debug.Log("Total questions: " + totalQuestions);
    }

    // Called from PlayerController when a correct answer is collected
    public void CheckWinCondition(int collected)
    {
        collectedAnswers = collected;
        Debug.Log(collectedAnswers + " collectedAnswers");
        if (collectedAnswers >= totalQuestions)
        {
            Debug.Log("WIN CONDITION REACHED");
            ShowWinScreen();
        }
    }

    private void ShowWinScreen()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(true);

            // Optional: fade in if using CanvasGroup
            CanvasGroup cg = winPanel.GetComponent<CanvasGroup>();
            if (cg != null)
            {
                cg.alpha = 1f;
                cg.interactable = true;
                cg.blocksRaycasts = true;
            }
        }

        Time.timeScale = 0f; // pause game
    }

    public void PlayAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
    public int CollectedPickups()
    {
        return collectedAnswers;
    }

    public int TotalTarget()
    {
        return totalQuestions;
    }

}
