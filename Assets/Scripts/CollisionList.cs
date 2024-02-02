using UnityEngine;
using System.Collections;
using System.Collections.Generic;   // Don't forget to add this if using a List.
using UnityEngine.Rendering;
using TMPro;
using UnityEditor.ShaderGraph.Serialization;
using UnityEngine.Networking;
using System.Text;


public class CollisionList : MonoBehaviour
{

    // Declare and initialize a new List of GameObjects called currentCollisions.
    public static List<List<GameObject>> currentCollisions = new List<List<GameObject>>();

    private const string ServerURL = "http://127.0.0.1:5000/execute_python";


    private IEnumerator Start()
    {
        Transform parent1 = transform;

        // Get the number of child GameObjects
        int numberOfChildren = parent1.childCount;

        Debug.Log("Number of child GameObjects: " + numberOfChildren);

        for (int i = 0; i < numberOfChildren; i++)
        {
            currentCollisions.Add(new List<GameObject>());
        }


        for (int i = 0; i < currentCollisions.Count; i++)
        {
            for (int j = 0; j < currentCollisions[i].Count; j++)
            {

                Debug.Log(i + " " + j + " " + currentCollisions[i][j].gameObject.name);

            }
        }
        // Add a delay
        yield return new WaitForSeconds(10);

        //logic of objects to code

        string s = "";
        for (int i = 0; i < currentCollisions.Count; i++)
        {
            SortGameObjectsByZPosition(i);
            int spaces = 0;
            for (int j = 0; j < currentCollisions[i].Count; j++)
            {
                //parent
                Transform parent = currentCollisions[i][j].transform;
                //second child which is the canvas
                GameObject canvas = parent.GetChild(1).gameObject;
                //getting canvas tranform
                Transform canvas_parent = canvas.transform;
                //text child
                GameObject code_text = canvas_parent.GetChild(0).gameObject;
                // Get the TextMeshProUGUI component from the GameObject
                TextMeshProUGUI textMeshProComponent = code_text.GetComponent<TextMeshProUGUI>();

                string textMeshProText = textMeshProComponent.text;
                string firstKeyword = textMeshProText.Split(' ')[0];


                switch (firstKeyword)
                {
                    case "if":
                        for (int space = 0; space < spaces; space++)
                        {
                            s += ' ';
                        }
                        spaces += 4;
                        s += textMeshProText + '\n';
                        break;
                    case "for":
                        for (int space = 0; space < spaces; space++)
                        {
                            s += ' ';
                        }
                        spaces += 4;
                        s += textMeshProText + '\n';
                        break;
                    default:
                        for (int space = 0; space < spaces; space++)
                        {
                            s += ' ';
                        }
                        s += textMeshProText + '\n';
                        break;
                }

            }
            StartCoroutine(SendPythonCodeWithDelay(s, 5f));

    }
     
      

    }
    void SortGameObjectsByZPosition(int index)
    {
        // Use the List.Sort method with a custom comparison function
        currentCollisions[index].Sort((go1, go2) =>
         {
             // Compare the z-positions of the two GameObjects in decreasing order
             float z1 = go1.transform.position.z;
             float z2 = go2.transform.position.z;

             // Sort in decreasing order based on z-position
             return z2.CompareTo(z1);
         });

    }

    IEnumerator SendPythonCodeWithDelay(string code, float delaySeconds)
    {
        // Add a delay
        yield return new WaitForSeconds(delaySeconds);

        // Send Python code
        StartCoroutine(SendPythonCode(code));

    }
    IEnumerator SendPythonCode(string code)
    {
        Debug.Log(code);
        Python py = new Python(code);
        var jsonData = JsonUtility.ToJson(py);
        Debug.Log(jsonData);
        var request = new UnityWebRequest(ServerURL, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        //form.AddField("python_code", code);
        // Create a UnityWebRequest POST request
        //UnityWebRequest www = UnityWebRequest.Post(ServerURL, form);

        // Send the request
        //yield return www.SendWebRequest();

        Debug.Log(request.downloadHandler.text);
        PythonResponse response = JsonUtility.FromJson<PythonResponse>(request.downloadHandler.text);

        if (response != null)
        {
            string result = response.result;
            Debug.Log(result);
        }
        else
        {
            Debug.LogError("Failed to parse JSON response.");
        }

        request.Dispose();
    }
    public class Python
    {
        public string python_code;
        public Python(string code)
        {
            this.python_code = code;
        }
    }

    public class PythonResponse
    {
        public string result;
        public bool success;
    }
}

