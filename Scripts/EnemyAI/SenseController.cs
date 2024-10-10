using UnityEngine;

public abstract class SenseController : MonoBehaviour, ISenseController
{
    protected ISense tracker;
    protected AIController aiController;

    public void Awake()
    {
        aiController = GetComponent<AIController>();
    }
    public void SetAIController(AIController aiController)
    {
        this.aiController = aiController;
    }
    public virtual void SetTracker(ISense tracker)
    {
        this.tracker = tracker;
    }

    public abstract void OnDetectedTarget(Transform target);
    public abstract void OnUndetectedTarget();

    public void OnDisableSense()
    {
        tracker.DisableSense();
    }

    public void OnEnableSense()
    {
        tracker.EnableSense();
    }

}
