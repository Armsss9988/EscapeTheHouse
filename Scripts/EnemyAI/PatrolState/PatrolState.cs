using UnityEngine;
using UnityEngine.AI;
[CreateAssetMenu(fileName = "PatrolState", menuName = "PatrolState")]
public class PatrolState : AIState
{
    [SerializeField] protected float speed = 1f;
    public override void EnterState(AIController aiController)
    {
        NavMeshAgent agent = aiController.GetCachedComponent<NavMeshAgent>();
        agent.isStopped = false;
        agent.ResetPath();
        aiController.SetCacheValue("currentWaypointIndex", 0);
        MoveToNextWaypoint(aiController);
    }
    public override void ExitState(AIController aiController)
    {
        NavMeshAgent agent = aiController.GetCachedComponent<NavMeshAgent>();
        agent.isStopped = true;
        agent.ResetPath();

    }

    public override void UpdateState(AIController aiController)
    {
        NavMeshAgent agent = aiController.GetCachedComponent<NavMeshAgent>();
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance + 0.1f)
        {
            MoveToNextWaypoint(aiController);
        }
    }
    private void MoveToNextWaypoint(AIController aiController)
    {
        WaypointsPatrol randomPatrol = aiController.GetCachedComponent<WaypointsPatrol>();
        if (randomPatrol != null)
        {
            Transform[] waypoints = randomPatrol.GetWaypoints();
            /*int currentWaypointIndex = randomPatrol.GetCurrentWayPointIndex();*/
            int currentWaypointIndex = aiController.GetCacheValue<int>("currentWaypointIndex");
            aiController.GetCachedComponent<NavMeshAgent>().destination = waypoints[currentWaypointIndex].position;
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            /*randomPatrol.SetCurrentWayPointIndex(currentWaypointIndex);*/
            aiController.SetCacheValue("currentWaypointIndex", currentWaypointIndex);
        }
        else
        {
            Debug.LogWarning($"{aiController.name} does not implement IRandomPatrol");
        }
    }

}
