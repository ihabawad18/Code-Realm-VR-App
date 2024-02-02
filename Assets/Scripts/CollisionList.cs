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

    
    private void Start()
    {
        Transform parent1 = transform;

        // Get the number of child GameObjects
        int numberOfChildren = parent1.childCount;

        for (int i = 0; i < numberOfChildren; i++)
        {
            currentCollisions.Add(new List<GameObject>());
        }


        /*for (int i = 0; i < currentCollisions.Count; i++)
        {
            for (int j = 0; j < currentCollisions[i].Count; j++)
            {

                Debug.Log(i + " " + j + " " + currentCollisions[i][j].gameObject.name);

            }
        }*/

        // Add a delay
        //yield return new WaitForSeconds(10);


        

    }
     
      

    }
    

