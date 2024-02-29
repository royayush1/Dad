using UnityEngine;
using System.Linq;
using System.Collections;

public class DadPathFollow : MonoBehaviour
{

    // Array of waypoints to walk from one to the next one
    [SerializeField]
    private Transform[] waypoints;

    // Walk speed that can be set in Inspector
    [SerializeField]
    private float moveSpeed = 2f;

    // Index of current waypoint from which Enemy walks
    // to the next one
    private int waypointIndex = 0;

    private int dadPathNum = 0;

    int[] stopIndices = new int[2];
    public static System.Random rnd = new System.Random();
    public bool stop = false;
    public float dadWalkOutRate = 120f;
    private float nextWalkTime;




    // Use this for initialization
    private void Start()
    {

        // Set position of Enemy as position of the first waypoint
        transform.position = waypoints[waypointIndex].transform.position;
        stopIndices[0] = rnd.Next(5, 7);
        stopIndices[1] = rnd.Next(7, waypoints.Length);
        nextWalkTime = Time.time + dadWalkOutRate;


    }

    // Update is called once per frame
    private void Update()
    {
        if (Time.time >= nextWalkTime)
        {
            if (stop)
            {
                int chance = rnd.Next(0, 350);
                if (chance == 0)
                {
                    stop = false;
                }
                else
                {
                    stop = true;
                }
            }
            else
            {
                Move();
            }
        }
        // Move Enemy
        

    }

    // Method that actually make Enemy walk
    private void Move()
    {
        // If Enemy didn't reach last waypoint it can move
        // If enemy reached last waypoint then it stops
        if (waypointIndex <= waypoints.Length - 1)
        {

            // Move Enemy from current waypoint to the next one
            // using MoveTowards method
            transform.position = Vector2.MoveTowards(transform.position,
               waypoints[waypointIndex].transform.position,
               moveSpeed * Time.deltaTime);


            // If Enemy reaches position of waypoint he walked towards
            // then waypointIndex is increased by 1
            // and Enemy starts to walk to the next waypoint
            if (transform.position == waypoints[waypointIndex].transform.position)
            {
                if (stopIndices.Contains(waypointIndex))
                {
                    stop = true;
                }
               
                waypointIndex += 1;
            }
        }

        else
        {
            waypointIndex = 0;
            transform.position = Vector2.MoveTowards(transform.position,
               waypoints[waypointIndex].transform.position,
               moveSpeed * Time.deltaTime);
            nextWalkTime = Time.time + dadWalkOutRate;




        }

        
    }
}