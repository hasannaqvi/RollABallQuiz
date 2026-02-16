//using System.Collections.Generic;
//using UnityEngine;

//public class LoadScreenObjects : MonoBehaviour
//{
//    public static int totalDanger;
//    public static int totalTarget;
//    public WinController winController;
//    // Start is called once before the first execution of Update after the MonoBehaviour is created
//    void Start()
//    {
//        GameObject[] objects = GameObject.FindGameObjectsWithTag("PickUp");

//        int dangerCount = Random.Range(1, 9);
//        dangerCount = Mathf.Min(dangerCount, objects.Length);

//        List<GameObject> available = new List<GameObject>(objects);

//        // Assign Danger
//        for (int i = 0; i < dangerCount; i++)
//        {
//            int index = Random.Range(0, available.Count);
//            GameObject selected = available[index];

//            selected.tag = "danger";
//            selected.GetComponent<Renderer>().material.color = Color.red;

//            available.RemoveAt(index);
//        }

//        // Assign Target
//        foreach (GameObject obj in available)
//        {
//            obj.tag = "target";
//            obj.GetComponent<Renderer>().material.color = Color.blue;
//        }

//        totalDanger = dangerCount;
//        totalTarget = available.Count;

//        Debug.Log(totalDanger + " dangers, " + totalTarget + " targets.");
//        winController.InitializeCounts(dangerCount, available.Count);
//    }


//    // Update is called once per frame
//    void Update()
//    {

//    }
//}
using System.Collections.Generic;
using UnityEngine;

public class LoadScreenObjects : MonoBehaviour
{
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

    void Start()
    {
        List<string> answersList = new List<string>(surgeryQuestions.Values);

        // Find all objects with PickUp tag
        GameObject[] pickUpObjects = GameObject.FindGameObjectsWithTag("PickUp");

        // Shuffle answers list
        ShuffleList(answersList);

        // Assign tags and add text
        for (int i = 0; i < pickUpObjects.Length; i++)
        {
            GameObject obj = pickUpObjects[i];

            // Pick an answer
            string answerTag = (i < answersList.Count) ? answersList[i] : answersList[Random.Range(0, answersList.Count)];

            // Assign the tag
            obj.tag = answerTag;

            // Create a child GameObject for the text
            GameObject textObj = new GameObject("Text");
            textObj.transform.SetParent(obj.transform);
            textObj.transform.localPosition = new Vector3(0f, 0.6f, 0f);
            textObj.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);

            // Add TextMesh to child
            TextMesh tm = textObj.AddComponent<TextMesh>();
            tm.text = answerTag;
            tm.anchor = TextAnchor.MiddleCenter;
            tm.alignment = TextAlignment.Center;
            tm.characterSize = 0.1f;
            tm.fontSize = 40;
            tm.color = Color.black;
        }
    }
    // Simple Fisher-Yates shuffle
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
