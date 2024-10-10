using UnityEngine;
using UnityEngine.AI;
[CreateAssetMenu(fileName = "TrackingState", menuName = "TrackingState")]
public class TrackingState : AIState
{
    [SerializeField] private float rotationSpeed = 3f;
    [SerializeField] private float pauseDuration = 1f;
    [SerializeField] private float duration = 4f;

    public override void EnterState(AIController aiController)
    {
        NavMeshAgent agent = aiController.GetCachedComponent<NavMeshAgent>();
        agent.isStopped = false;
        agent.ResetPath();
        aiController.SetCacheValue<int>("indexDirection", 0);
        aiController.SetCacheValue<float>("timePause", pauseDuration);
        aiController.SetCacheValue<float>("remainingTime", duration);
    }

    private Vector3[] directions = new Vector3[]
   {
        Vector3.forward,
        Vector3.right,
        Vector3.back,
        Vector3.left
   };

    public override void ExitState(AIController aiController)
    {
        NavMeshAgent agent = aiController.GetCachedComponent<NavMeshAgent>();
        agent.isStopped = false;
        agent.ResetPath();

    }

    public override void UpdateState(AIController aiController)
    {
        Transform aiTransform = aiController.transform;
        float timePause = aiController.GetCacheValue<float>("timePause");
        int indexDir = aiController.GetCacheValue<int>("indexDirection");
        float remainingTime = aiController.GetCacheValue<float>("remainingTime");

        Quaternion targetRotation = Quaternion.LookRotation(directions[indexDir]);

        if (timePause <= 0)
        {
            timePause = pauseDuration;
            indexDir = (indexDir + 1) % directions.Length;
            aiController.SetCacheValue("indexDirection", indexDir);
        }

        aiTransform.rotation = Quaternion.RotateTowards(
                   aiTransform.rotation,
                   targetRotation,
                   rotationSpeed * Time.deltaTime
               );

        if (Quaternion.Angle(aiTransform.rotation, targetRotation) < 0.1f)
        {
            aiController.SetCacheValue("timePause", timePause - Time.deltaTime);
        }

        remainingTime -= Time.deltaTime;
        aiController.SetCacheValue("remainingTime", remainingTime);
        if (remainingTime <= 0f)
        {


            aiController.DefaulState();
        }
    }

}
