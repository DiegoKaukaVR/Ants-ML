using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Sensors.Reflection;

public class AntMLAgent : Agent
{

    [Header("Agent Configuration")]
    [SerializeField] bool training;

 

    public Vector3 AgentPosition
    {
        get
        {
            return transform.position;
        }
    }

    public Transform target;

    public Vector3 TargetPos
    {
        get
        {
            return target.position;
        }
        set
        {
            target.position = value;
        }
    }

    [Header("Ant Configuration")]
    public stats _stats = new stats();

    [System.Serializable]
    public struct stats
    {
        public int hp;
        public int energy;
        public float strength;
    }

    Vector3 originalPos;
    stats originalStats;

    protected NavMeshAgent navMeshAgent;
 

    #region Monobehavior

    protected virtual void Awake()
    {
       
    }
    protected virtual void Start()
    {
       
    }
    protected override void OnEnable()
    {
        base.OnEnable();
    }
    protected override void OnDisable()
    {
        base.OnDisable();

    }

    public override void Initialize()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        originalPos = transform.position;
        originalStats = _stats;

        if (!training) { MaxStep = 0;} //Infinite Gameplay
    }

    #endregion

    #region MLAgent Implementation
    public override void OnEpisodeBegin()
    {
        ResetAgent();
        TargetPos = FindRandomValidNavmeshPoint();
        GoToTarget(TargetPos);
    }



    void ResetAgent()
    {
        transform.position = originalPos;
        _stats = originalStats;
        navMeshAgent.velocity = Vector3.zero;
    }


    /// <summary>
    /// 
    /// </summary>
    public override void OnActionReceived(ActionBuffers actions)
    {
        //High Reward
        if (Vector3.Distance(transform.position, target.position)<0.2f)
        {
            AddReward(5f);
        }

        //Negative Reward
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position);
        sensor.AddObservation(TargetPos);
        sensor.AddObservation(originalPos);

    }

    #region Extensions

    

    /// <summary>
    /// Called in Heuristic Mode, when player take controll of the agent or create a new Decision Policy
    /// </summary>
    public override void Heuristic(in ActionBuffers actionsOut)
    {

    }

    /// <summary>
    /// Collect custom observations
    /// </summary>
     #endregion

    #endregion

    #region Movement

    [SerializeField] bool debugPath = false;
    public Vector3 FindRandomValidNavmeshPoint()
    {
        Vector3 finalPoint = new Vector3();
      
        Vector3 randomPoint = new Vector3(Random.Range(0, 5), 0, Random.Range(0,5));
        NavMeshHit navMeshHit;

        if (NavMesh.SamplePosition(randomPoint, out navMeshHit, 0.2f, NavMesh.AllAreas))
        {
            finalPoint = navMeshHit.position;
        }

        return finalPoint;
    }
    public void GoToTarget(Vector3 pos)
    {
        if (debugPath)
        {
            Debug.DrawLine(transform.position, pos, Color.yellow);
        }

        //Comprobar si el punto está en la navmesh

        navMeshAgent.SetDestination(pos);
    }

    #endregion


}
