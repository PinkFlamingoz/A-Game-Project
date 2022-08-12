using UnityEngine;
using UnityEngine.AI;

public class EnemyFollowScript : MonoBehaviour
{
    public GameObject[] myTargets;
    public GameObject currentTarget;
    public NavMeshAgent myAgent;
    public int range;
    public int tetherRange;
    public Vector3 startPosition;
    public Enemy enemyStartSpeed;
    // Start is called before the first frame update
    public void Start()
    {
        myTargets = GameObject.FindGameObjectsWithTag("Player");
        InvokeRepeating("DistanceCheck", 0, 0.5f);
        //startPosition = this.transform.position;
    }

    // Update is called once per frame
    public void Update()
    {
        if (currentTarget != null)
        {
            myAgent.destination = currentTarget.transform.position;
        }
        else if (myAgent.destination != startPosition)
        {
            myAgent.destination = startPosition;
        }
    }
    public void DistanceCheck()
    {
        foreach (GameObject target in myTargets)
        {
            float distance = Vector3.Distance(this.transform.position, target.transform.position);
            if (distance < range)
            {
                currentTarget = target;
            }
            else if (distance > tetherRange)
            {
                currentTarget = null;
            }
        }
        myAgent.speed = enemyStartSpeed.startSpeed;
    }
}
