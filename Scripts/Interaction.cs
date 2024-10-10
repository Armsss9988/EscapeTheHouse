using UnityEngine;

public class Interaction : MonoBehaviour
{
    CharacterController characterController;
    private bool isInteracting = false;
    Interacted interacted;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position + Vector3.up, (transform.TransformDirection(Vector3.forward)) * 2f, Color.yellow);
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(transform.position + Vector3.up, transform.TransformDirection(Vector3.forward), out RaycastHit hit, 2f))
            {

                Debug.Log("Did Hit");
                if (hit.collider.TryGetComponent(out interacted))
                {
                    characterController.ChangeState(new Interact(characterController));
                }
            }
        }
    }
    public void Interact()
    {
        interacted.OnInteract();
    }
    public void StartInteraction()
    {
        isInteracting = true;
    }
    public void EndInteraction()
    {
        isInteracting = false;
    }
    public bool IsInteracting()
    {
        return isInteracting;
    }
}
