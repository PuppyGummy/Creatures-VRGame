using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject food;
    public Transform foodPos;
    private GameObject foodInstance;
    public void GrabFood()
    {
        Debug.Log("Grabbed");
        PetController.Instance.foodInstance = Instantiate(food, new Vector3(foodPos.position.x, foodPos.position.y + 1, foodPos.position.z), Quaternion.identity);
    }
}
