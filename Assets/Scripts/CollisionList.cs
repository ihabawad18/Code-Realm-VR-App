using UnityEngine;
using System.Collections;
using System.Collections.Generic;   // Don't forget to add this if using a List.
using UnityEngine.Rendering;
using TMPro;

public class CollisionList : MonoBehaviour
{

    // Declare and initialize a new List of GameObjects called currentCollisions.
    public static List<List<GameObject>> currentCollisions = new List<List<GameObject>>();


    private void Start()
    {
        Transform parent = transform;

        // Get the number of child GameObjects
        int numberOfChildren = parent.childCount;

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


    }

    private void Update()
    {
        string s = "";
        for (int i = 0; i < currentCollisions.Count; i++)
        {
            SortGameObjectsByZPosition(i);
            int spaces = 4;
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
                        spaces+=4;
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
        }
        Debug.Log(s);

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
}