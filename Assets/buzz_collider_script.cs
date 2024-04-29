using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buzz_collider_script : MonoBehaviour
{
    public Button myButton;
    void OnTriggerEnter(Collider other)
    {
        if (transform.parent != null)
        {
            Transform grandParent = transform.parent.parent;
            if (grandParent != null)
            {
                if (grandParent.gameObject.activeInHierarchy)
                {
                    myButton.onClick.Invoke();

                    Debug.Log("pressed button.");
                }
                else
                {
                    Debug.Log("Cant press the button.");
                }
            }
            else
            {
                Debug.Log("No grandparent found.");
            }
        }
        else
        {
            Debug.Log("No parent found.");
        }

    }
}
