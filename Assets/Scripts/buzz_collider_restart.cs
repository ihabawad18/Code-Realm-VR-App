using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buzz_collider_restart : MonoBehaviour
{
    public Button myButton;
    public AudioClip buzzSound;

    public AudioSource audioSource;

    void OnTriggerEnter(Collider other)
    {
        if (myButton.interactable)
        {
            Transform buttonParent = myButton.transform.parent;
            if (buttonParent != null)
            {
                Transform buttonGrandParent = buttonParent.parent;
                if (buttonGrandParent != null)
                {
                    if (buttonGrandParent.gameObject.activeInHierarchy)
                    {
                        audioSource.PlayOneShot(buzzSound);
                        StartCoroutine(DelayButtonClick());
                    }
                    else
                    {
                        Debug.Log("Can't press the button because the grandparent is not active.");
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

    IEnumerator DelayButtonClick()
    {
        yield return new WaitForSeconds(1.5f);
        myButton.onClick.Invoke();
        Debug.Log("Pressed button after 1 second.");
    }
}
