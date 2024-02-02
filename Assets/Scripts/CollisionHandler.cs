using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class CollisionHandler : MonoBehaviour
{
    //private CollisionList parentScript = new();
    public int index;
    private void Start()
    {

    }
    
    
    void OnCollisionEnter(Collision col)
    {

        // Add the GameObject collided with to the list.
        if (col.gameObject.name != "Cube (1)" && !CollisionList.currentCollisions[index].Contains(col.gameObject))
        {
            CollisionList.currentCollisions[index].Add(col.gameObject);
            Debug.Log("entered");
            
            for (int i = 0; i < CollisionList.currentCollisions.Count; i++)
            {
                for (int j = 0; j < CollisionList.currentCollisions[i].Count; j++)
                {
                    Debug.Log(i + " " + j + " " + CollisionList.currentCollisions[i][j].gameObject.name);

                    //CollisionList.currentCollisions[i][j].gameObject.GetComponents<GameObject>();

                }
            }
        }
        

    }

    void OnCollisionExit(Collision col)
    {

        // Remove the GameObject collided with from the list.
        if (col.gameObject.name != "Cube (1)")
        {
            CollisionList.currentCollisions[index].Remove(col.gameObject);
            for (int i = 0; i < CollisionList.currentCollisions.Count; i++)
            {
                for (int j = 0; j < CollisionList.currentCollisions[i].Count; j++)
                {

                    Debug.Log(i + " " + j + " " + CollisionList.currentCollisions[i][j].gameObject.name);

                }
            }
        }
    }
}
