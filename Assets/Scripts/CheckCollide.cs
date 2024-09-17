using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollide : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Food")
        {
            Debug.Log("Collide with " + other.gameObject.tag);
            PetController.Instance.onReachFood();
        }
        else if (other.gameObject.tag == "Bed")
        {
            Debug.Log("Collide with " + other.gameObject.tag);
            PetController.Instance.ChangeAnimationState("Sleep");
        }
    }
}
