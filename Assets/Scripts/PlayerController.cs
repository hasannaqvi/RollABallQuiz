//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.UIElements;

//public class PlayerController : MonoBehaviour
//{
//    Dictionary<string, string> surgeryQuestions = new Dictionary<string, string>()
//{
//    {"Which procedure removes the appendix?", "Appendectomy"},
//    {"Which surgery replaces a damaged hip joint?", "Hip Replacement"},
//    {"Which surgery opens blocked coronary arteries?", "Coronary Angioplasty"},
//    {"Which operation removes a gallbladder?", "Cholecystectomy"},
//    {"Which surgery repairs a hernia?", "Herniorrhaphy"},
//    {"Which procedure removes part of the colon?", "Colectomy"},
//    {"Which surgery replaces a knee joint?", "Knee Arthroplasty"},
//    {"Which surgery corrects vision using lasers?", "LASIK"},
//    {"Which surgery removes the tonsils?", "Tonsillectomy"},
//    {"Which operation repairs a torn rotator cuff?", "Rotator Cuff Repair"},
//    {"Which surgery removes a cataract from the eye?", "Cataract Surgery"},
//    {"Which procedure bypasses blocked coronary arteries?", "Coronary Artery Bypass Grafting (CABG)"}
//};

//    public float speed = 10f;
//    public Text scoreText;
//    private Rigidbody rb;
//    private int score = 0;
//    public WinController winController;
//    public GameObject lossPanel;
//    public Text QuestionText;


//    public Transform ground;
//    public float padding = 0.5f;

//    float minX, maxX, minZ, maxZ;

//    void Start()
//    {
//        rb = GetComponent<Rigidbody>();
//        Debug.Log("PlayerController is running");
//        float width = 10f * ground.localScale.x;
//        float length = 10f * ground.localScale.z;

//        Vector3 center = ground.position;

//        minX = center.x - width / 2f + padding;
//        maxX = center.x + width / 2f - padding;

//        minZ = center.z - length / 2f + padding;
//        maxZ = center.z + length / 2f - padding;

//    }

//    void FixedUpdate()
//    {
//        //float moveHorizontal = Input.GetAxis("Horizontal");
//        //float moveVertical = Input.GetAxis("Vertical");
//        //Debug.Log("Movement: " + moveHorizontal + ", " + moveVertical);

//        //Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
//        //rb.AddForce(movement * speed);
//        float moveHorizontal = Input.GetAxis("Horizontal");
//        float moveVertical = Input.GetAxis("Vertical");

//        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
//        rb.AddForce(movement * speed);

//        // Clamp inside plane
//        Vector3 clampedPosition = rb.position;
//        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
//        clampedPosition.z = Mathf.Clamp(clampedPosition.z, minZ, maxZ);

//        rb.position = clampedPosition;

//    }

//    void OnTriggerEnter(Collider other)
//    {
//        Debug.Log("Triggered with: " + other.name);

//        if (other.CompareTag("danger"))
//        {
//            lossPanel.SetActive(true);

//        }
//        if (other.CompareTag("target"))
//        {
//            Debug.Log("About to call CollectPickup");

//            other.gameObject.SetActive(false);
//            score++;
//            scoreText.text = "Score: " + score;
//            winController.CheckWinCondition(score);

//        }
//    }

//}
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public Text scoreText;
    public Text questionText;
    public GameObject winPanel;
    public GameObject lossPanel;

    public Transform ground;
    public float padding = 0.5f;

    private Rigidbody rb;
    private int score = 0;
    public WinController winController;

    // Questions dictionary
    Dictionary<string, string> surgeryQuestions = new Dictionary<string, string>()
    {
        {"Which procedure removes the appendix?", "Appendectomy"},
        {"Which surgery replaces a damaged hip joint?", "Hip Replacement"},
        {"Which surgery opens blocked coronary arteries?", "Coronary Angioplasty"},
        {"Which operation removes a gallbladder?", "Cholecystectomy"},
        {"Which surgery repairs a hernia?", "Herniorrhaphy"},
        {"Which procedure removes part of the colon?", "Colectomy"},
        {"Which surgery replaces a knee joint?", "Knee Arthroplasty"},
        {"Which surgery corrects vision using lasers?", "LASIK"},
        {"Which surgery removes the tonsils?", "Tonsillectomy"},
        {"Which operation repairs a torn rotator cuff?", "Rotator Cuff Repair"},
        {"Which surgery removes a cataract from the eye?", "Cataract Surgery"},
        {"Which procedure bypasses blocked coronary arteries?", "Coronary Artery Bypass Grafting (CABG)"}
    };

    private List<KeyValuePair<string, string>> questionList;
    private int currentQuestionIndex = 0;

    float minX, maxX, minZ, maxZ;

    void Start()
    {
        if(winController !=  null)
            winController.InitializeTotalQuestions(questionList.Count);
        
        rb = GetComponent<Rigidbody>();

        // Ground boundaries
        float width = 10f * ground.localScale.x;
        float length = 10f * ground.localScale.z;
        Vector3 center = ground.position;
        minX = center.x - width / 2f + padding;
        maxX = center.x + width / 2f - padding;
        minZ = center.z - length / 2f + padding;
        maxZ = center.z + length / 2f - padding;

        // Convert dictionary to list for ordered questions
        questionList = new List<KeyValuePair<string, string>>(surgeryQuestions);
        ShuffleList(questionList);
        // Show first question
        if (questionList.Count > 0)
        {
            currentQuestionIndex = 0;
            questionText.text = questionList[currentQuestionIndex].Key;
        }

        // Initialize score UI
        scoreText.text = "Score: 0 / " + questionList.Count;
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
        rb.AddForce(movement * speed);

        // Clamp inside ground
        Vector3 clampedPosition = rb.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        clampedPosition.z = Mathf.Clamp(clampedPosition.z, minZ, maxZ);
        rb.position = clampedPosition;
    }

    void OnTriggerEnter(Collider other)
    {
        // Ignore objects without tags
        if (other.gameObject.tag == "Untagged")
            return;

        string currentAnswer = questionList[currentQuestionIndex].Value;

        if (other.CompareTag(currentAnswer))
        {
            // Correct collision
            other.gameObject.SetActive(false);
            score++;
            scoreText.text = "Score: " + score + " / " + questionList.Count;

            currentQuestionIndex++;

            if (currentQuestionIndex < questionList.Count)
            {
                questionText.text = questionList[currentQuestionIndex].Key;
            }
            if (winController != null)
            {
                winController.CheckWinCondition(score);
            }
        }
        else
        {
            // Wrong answer
            lossPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }
    void ShuffleList<T>(List<T> list)
    {
        System.Random rng = new System.Random(); // seeded with current time by default
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1); // 0 to n
            T temp = list[k];
            list[k] = list[n];
            list[n] = temp;
        }
    }
}
