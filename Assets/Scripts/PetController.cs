using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class PetController : MonoBehaviour
{
    public static PetController Instance { get; private set; }

    public Animator animator;
    private string currentState;
    private Camera cam;
    private NavMeshAgent agent;
    public Collider planeCollider;
    public GameObject food;
    public Rigidbody rb;
    private Ray ray;
    private RaycastHit hit;
    private bool isEating, isSleeping;
    public GameObject foodInstance;
    public GameObject effect;
    public Transform effectPos;
    public XRRayInteractor leftRay, rightRay;
    public Transform bedPoint;

    void Start()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        GameObject.DontDestroyOnLoad(this.gameObject);

        cam = Camera.main;
        agent = GetComponent<NavMeshAgent>();

        foodInstance = null;
    }
    void Update()
    {
        Eat();
        if (agent.remainingDistance > agent.stoppingDistance)
        {
            ChangeAnimationState("Walk");
        }
        else if (isEating)
        {
            ChangeAnimationState("Eating");
        }
        else if (!isSleeping)
        {
            ChangeAnimationState("Idle");
        }
    }

    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        animator.Play(newState);
        currentState = newState;
    }

    public void OnClick()
    {
        Debug.Log("Clicked");
        ChangeAnimationState("Happy");
        Instantiate(effect, effectPos.position, Quaternion.identity);
    }
    public void GoSleep()
    {
        Debug.Log("GoSleep");
        isSleeping = true;
        agent.SetDestination(bedPoint.position);
    }

    public void TurnAround()
    {
        ChangeAnimationState("Circle");
    }

    public void Walk()
    {
        isSleeping = false;
        Vector3 pos = new Vector3();
        Vector3 norm = new Vector3();
        int index = 0;
        bool validTarget = false;
        leftRay.TryGetHitInfo(out pos, out norm, out index, out validTarget);
        rightRay.TryGetHitInfo(out pos, out norm, out index, out validTarget);
        agent.SetDestination(pos);
    }


    public void Eat()
    {
        if (foodInstance != null)
        {
            agent.SetDestination(foodInstance.transform.position);
        }
    }
    public void onReachFood()
    {
        isEating = true;
        StartCoroutine(Eating());
    }

    IEnumerator Eating()
    {
        yield return new WaitForSeconds(2);
        isEating = false;
        Instantiate(effect, effectPos.position, Quaternion.identity);
        Destroy(foodInstance);
    }
}
