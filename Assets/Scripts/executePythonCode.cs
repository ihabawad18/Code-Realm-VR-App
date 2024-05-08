using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class executePythonCode : MonoBehaviour
{

    private const string ServerURL = "https://code-realm-vr-app.onrender.com/execute_python";
    public GameObject codeText;
    public GameObject executedText;
    List<List<GameObject>> currentCollisions = CollisionList.currentCollisions;
    string code = "";
    public void Update()
    {
        string s = "";
        //logic of objects to code
        int spaces = 0;

        for (int i = 0; i < currentCollisions.Count; i++)
        {

            SortGameObjectsByXPosition(i);

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

                Debug.Log(textMeshProText);

                /*switch (firstKeyword)
                {
                    case "if":
                        for (int space = 0; space < spaces; space++)
                        {
                            s += ' ';
                        }
                        spaces += 4;
                        s += textMeshProText + '\n';
                        break;
                    case "elif":
                        for (int space = 0; space < spaces; space++)
                        {
                            s += ' ';
                        }
                        spaces += 4;
                        s += textMeshProText + '\n';
                        break;
                    case "else":
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
                    case "while":
                        for (int space = 0; space < spaces; space++)
                        {
                            s += ' ';
                        }
                        spaces += 4;
                        s += textMeshProText + '\n';
                        break;
                    case "def":
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
                }*/

                switch (firstKeyword)
                {
                    case "+space":
                        spaces += 4;
                        break;
                    case "-space":
                        spaces -= 4;
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
            
        }
        code = s;
        codeText.GetComponent<TextMeshProUGUI>().text = code;
    }

    public void executeCode()
    {
        
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
            if (!string.IsNullOrEmpty(response.result))
            {

                string result = response.result;
                executedText.GetComponent<TextMeshProUGUI>().text = result;
                Debug.Log(result);
            }
            else
            {
                string pattern = @"line\s+(\d+)";
                Match match = Regex.Match(response.error, pattern);
                if (match.Success)
                {
                    string lineNumber = match.Groups[1].Value;
                    string result = "Error on Line number: " + lineNumber;
                    executedText.GetComponent<TextMeshProUGUI>().text = result;

                    Debug.Log(result);
                }
                else
                {
                    Debug.Log("Unknown error");
                }
                Debug.LogError("No error message found in the JSON response.");
            }
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
        public string error;
        public string result;
        public bool success;
    }
    void SortGameObjectsByXPosition(int index)
        {
            // Use the List.Sort method with a custom comparison function
            currentCollisions[index].Sort((go1, go2) =>
            {
                // Compare the z-positions of the two GameObjects in decreasing order
                float x1 = -go1.transform.position.x;
                float x2 = -go2.transform.position.x;

                // Sort in decreasing order based on x-position
                return x2.CompareTo(x1);
            });

        }
 }
