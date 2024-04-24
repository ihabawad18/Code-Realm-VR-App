using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buzz_collider_script : MonoBehaviour
{
    public Button myButton;
    void OnTriggerEnter(Collider other)
    {

        myButton.onClick.Invoke(); 
    }
}
