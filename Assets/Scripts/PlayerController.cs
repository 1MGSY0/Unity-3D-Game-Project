using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using System;
using UnityEngine.Windows;
using System.Diagnostics;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    //EventHandling
    public static PlayerController Instance { get; private set; }
    public event EventHandler<OnInteractNPCEventArgs> OnInteractNPC;

    public class OnInteractNPCEventArgs : EventArgs
    {
        public bool interactNPC;
    }
    public event EventHandler<MissionUpdateEventArgs> MissionUpdate;

    public class MissionUpdateEventArgs : EventArgs
    {
        public int missionComplete;
    }

    //Default
    private bool interactNPC = false;
    private bool exit = false;
    private bool canClean = false;
    private GameObject livingroom;
    private GameObject bedroom;
    private GameObject cushions;
    private GameObject result;

    //Mission
    public int totalMission;
    public int missionComplete = 0;
    private GameObject cleanObj;


    //Packages
    private CustomActions input;
    private NavMeshAgent agent;
    RaycastHit hit;

    //Animator
    private Animator animator;
    private const string IDLE = "Idle";
    private const string WALK = "Walk";
    private const string RAISE = "RaiseHand";

    [Header("Movement")]
    [SerializeField] private LayerMask clickableLayers;
    [SerializeField] private float lookRotationSpeed;

    private void Awake()
    {
        Instance = this;

        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        input = new CustomActions();
        if(SceneManager.GetActiveScene().name == "GameScene") 
        {
            livingroom = GameObject.Find("Living Room");
            bedroom = GameObject.Find("Bedroom");
            cushions = GameObject.Find("Cushions (Organised)");
            result = GameObject.Find("Result");
        }
        AssignInputs();
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            livingroom.SetActive(true);
            bedroom.SetActive(false);
            cushions.SetActive(false);
            result.SetActive(false);
        }
    }

    private void AssignInputs()
    {
        input.Player.Move.performed += ctx => ClickToMove();
        input.Player.Interact.performed += Interact_performed;
    }
    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    private void ClickToMove()
    {

        if(Physics.Raycast(Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition), out hit, 200, clickableLayers))
        {  
            agent.destination = hit.point;
        }
            
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            interactNPC = true;
        }
        if (other.gameObject.CompareTag("Exit"))
        {
            exit = true;
        }

        if (other.gameObject.CompareTag("Dirty") && other.gameObject.activeInHierarchy)
        {
            UnityEngine.Debug.Log(missionComplete);
            cleanObj = other.gameObject;
            canClean = true;
        }
        

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            interactNPC = false;
        }
        if (other.gameObject.CompareTag("Exit"))
        {
            exit = false;
        }

        if (other.gameObject.CompareTag("Livingroom"))
        {
            livingroom.SetActive(false);
            bedroom.SetActive(true);
        }
        if (other.gameObject.CompareTag("Bedroom"))
        {
            bedroom.SetActive(false);
            livingroom.SetActive(true);
        }
        if (other.gameObject.CompareTag("Dirty"))
        {
            canClean = false;
        }
    }

    private void Interact_performed(InputAction.CallbackContext obj)
    {
        animator.Play(RAISE);
        if (interactNPC) 
        {
            OnInteractNPC?.Invoke(this, new OnInteractNPCEventArgs
            {
                interactNPC = interactNPC
            }); 
        }
        if (exit)
        {
            UnityEngine.Debug.Log("Quit");
            Application.Quit();
        }
        if (canClean)
        {
            if (missionComplete < totalMission)
            {
                if (cleanObj.name == "Cushions (Disorganised)")
                {
                    cushions.SetActive(true);
                }
                missionComplete++;
                MissionUpdate?.Invoke(this, new MissionUpdateEventArgs
                {
                    missionComplete = missionComplete
                });
                cleanObj.SetActive(false);
            }
            else
            {
                cleanObj.SetActive(false);
                EndGame();
            }
        }

    }
    private void Update()
    {
        FaceTarget();
        SetAnimations();
    }
    private void FaceTarget()
    {

        if (agent.velocity != Vector3.zero)
        {
            Vector3 direction = (agent.destination - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * lookRotationSpeed);
        }

    }

    private void SetAnimations()
    {
        if(agent.velocity == Vector3.zero) 
        {
            animator.Play(IDLE);
        }
        else
        {
            animator.Play(WALK);
        }
    }
    
    private void EndGame()
    {
        result.SetActive(true);
    }
}
