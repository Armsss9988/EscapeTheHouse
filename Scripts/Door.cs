using UnityEngine;
using UnityEngine.AI;

public class Door : MonoBehaviour, Interacted
{
    Animator animator;
    [SerializeField] private NavMeshObstacle navObstacle;
    bool isOpen = false;
    void Start()
    {
        animator = GetComponent<Animator>();
        navObstacle.carving = true;
    }
    public void OnInteract()
    {
        Debug.Log("Door!!");
        if (isOpen)
        {
            animator.Play("DoorClose");
            isOpen = false;
            navObstacle.enabled = true;
        }
        else
        {
            animator.Play("DoorOpen");
            isOpen = true;
            navObstacle.enabled = false;
        }
    }


}
