using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Observer : MonoBehaviour, ISense
{
    [SerializeField] private float rayOffset;
    [SerializeField] private SenseController controller;
    SphereCollider colliderTracking;
    [SerializeField] float viewAngle;
    [SerializeField] float viewRadius;
    [SerializeField] LayerMask layerMask;
    [SerializeField] Collider target;
    [SerializeField] bool isTargetInZone = false;
    [SerializeField] bool isTargetDetected = false;
    void Awake()
    {
        colliderTracking = GetComponent<SphereCollider>();
        viewRadius = colliderTracking.radius;
        controller.SetTracker(this);
    }
    void OnTriggerEnter(Collider other)
    {
        if ((layerMask.value & (1 << other.gameObject.layer)) > 0)
        {
            Debug.Log("SomeThing entered the trigger!");
            isTargetInZone = true;
            target = other;
        }
    }
    public void Update()
    {
        if (isTargetInZone)
        {
            Vector3 direction = target.transform.position - transform.position;
            Vector3 raycastOrigin = transform.position + transform.forward * rayOffset;
            Debug.DrawRay(raycastOrigin + Vector3.up, direction, Color.red, 0.1f);
            if (Vector3.Angle(transform.forward, direction) < viewAngle / 2)
            {


                if (Physics.Raycast(raycastOrigin + Vector3.up, direction, out RaycastHit ray))
                {
                    Debug.Log("I see something!");
                    Debug.Log(ray.collider.gameObject.name);
                    if (ray.collider == target)
                    {
                        if (!isTargetDetected)
                        {
                            Debug.Log("I see player!");
                            controller.OnDetectedTarget(target.transform);
                            isTargetDetected = true;
                        }
                    }
                    else if (isTargetDetected)
                    {
                        isTargetDetected = false;
                        controller.OnUndetectedTarget();
                    }
                }
            }
            else
            {
                if (isTargetDetected)
                {
                    isTargetDetected = false;
                    controller.OnUndetectedTarget();
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewRadius);

        Vector3 leftBoundary = DirFromAngle(-viewAngle / 2);
        Vector3 rightBoundary = DirFromAngle(viewAngle / 2);

        // Draw field of view as a cone
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + leftBoundary * viewRadius);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary * viewRadius);

        // Draw full circle for clarity
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, viewRadius);
    }
    private Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal = false)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    void OnTriggerExit(Collider other)
    {
        if (other == target)
        {
            if (isTargetDetected)
            {
                controller.OnUndetectedTarget();
            }
            isTargetInZone = false;

        }
    }

    public void EnableSense()
    {
        colliderTracking.enabled = true;
    }

    public void DisableSense()
    {
        colliderTracking.enabled = false;
    }

    public void SetResponder(SenseController responder)
    {
        this.controller = responder;
    }
}
