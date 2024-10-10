using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AIController : MonoBehaviour
{
    [Serializable]
    public class StateData
    {
        public StateEnum stateKey;
        public AIState stateSO;
    }
    [SerializeField] private List<StateData> stateListData;
    protected StateMachine stateMachine;
    [SerializeField] protected Transform target;
    public CacheComponent cacheComponent;
    [SerializeField] AIState curState;
    [SerializeField] Dictionary<StateEnum, AIState> stateDictionary;
    [SerializeField] StateEnum defaulState;
    public Animator animator;
    public enum StateEnum
    {
        Patrol,
        Tracking,
        Chase,

    }
    public Transform Target
    {
        get => target;
        set => target = value;
    }

    public void Awake()
    {
        cacheComponent = new CacheComponent(this.gameObject);
        stateMachine = new StateMachine(this);
        animator = GetComponent<Animator>();
        stateDictionary = new Dictionary<StateEnum, AIState>();
        animator.applyRootMotion = true;

        foreach (var stateData in stateListData)
        {
            stateDictionary[stateData.stateKey] = stateData.stateSO;
        }
        DefaulState();
    }
    public T GetCachedComponent<T>() where T : Component
    {
        return cacheComponent.GetComponent<T>();
    }
    public Y GetCacheValue<Y>(string name)
    {
        return cacheComponent.GetValue<Y>(name);
    }
    public void SetCacheValue<Y>(string name, Y value)
    {
        cacheComponent.SetValue(name, value);
    }
    public void DefaulState()
    {
        ChangeState(defaulState);
    }

    public void Update()
    {
        stateMachine.Update();
        curState = stateMachine.GetState();
    }
    public void OnAnimatorMove()
    {
        stateMachine.OnAnimatorMove();
    }

    public void ChangeState(StateEnum newState)
    {
        stateMachine.ChangeState(stateDictionary[newState]);
    }

    internal NavMeshAgent GetNavMeshAgent()
    {
        throw new NotImplementedException();
    }
}
