using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buzz_collider_script : MonoBehaviour
{
    public Button myButton;

    void OnTriggerEnter(Collider other)
    {
        Transform buttonParent = myButton.transform.parent;
        if (buttonParent != null)
        {
            Transform buttonGrandParent = buttonParent.parent;
            if (buttonGrandParent != null)
            {
                if (buttonGrandParent.gameObject.activeInHierarchy)
                {
                    myButton.onClick.Invoke();

                    Debug.Log("pressed button.");
                }
                else
                {
                    Debug.Log("Cant press the button because the grandparent is not active.");
                }
            }
            else
            {
                Debug.Log("Button has no grandparent.");
            }
        }
        else
        {
            Debug.Log("Button has no parent.");
        }
    }
}
