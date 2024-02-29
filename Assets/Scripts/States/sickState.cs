using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sickState : State
{
    public GameObject snotPrefab;
    private Transform npcTransform; // Store NPC's transform
    private npc npcController; // Store reference to NPC controller
    public float incubationPeriod = 60;
    public bool timerIsRunning = false;
    public float coughInterval = 45f;
    private float nextCoughTime;
    private State returnState;
    public Vector3 Direction { get; set; }
    public LayerMask npcLayer;
   
    public float coughRange = 1f;

    // Start is called before the first frame update

    public sickState(State _returnState, Transform _npcTransform, npc _npcController, GameObject _snotPrefab) : base("Sick")
    {
        returnState = _returnState;
        npcTransform = _npcTransform;
        npcController = _npcController;
        snotPrefab = _snotPrefab;
    }

   

    // Update is called once per frame
    public override State Update(npc agent)
    {
        if (timerIsRunning)
        {

            if (incubationPeriod > 0)
            {

                incubationPeriod -= Time.deltaTime;
                Debug.Log("Still Incubating");
               

            }
            else
            {
                incubationPeriod = 0;
                Debug.Log("Done Incubating");
                timerIsRunning = false;
            }

            

        }
        if (!timerIsRunning)
        {
            if (Time.time >= nextCoughTime)
            {
                PerformCough();
                Debug.Log("NPC Coughing");
                nextCoughTime = Time.time + coughInterval;
            }


        }

        return this;

    }

    void PerformCough()
    {
        Collider[] hitColliders = Physics.OverlapSphere(npcTransform.position, 4f); // Adjust radius as needed
        foreach (Collider collider in hitColliders)
        {
            npc nearbyNPC = collider.GetComponent<npc>();
            if (nearbyNPC != null && nearbyNPC != npcController && nearbyNPC.isSick == false)
            {

                nearbyNPC.MakeSick();
            }
        }
        


        SpawnSnot();
        Debug.Log("Splooge released");
    }

    public override void EnterState(npc agent)
    {
        timerIsRunning = true;

        nextCoughTime = 15f;
        //agent.npcStateSick();
        //agent.SetSpeedModifierHalf();
    }

    //Upon exiting state, set speed and animation state to normal
    public override void ExitState(npc agent)
    {
        agent.npcStateNormal();
        agent.SetSpeedModifierNormal();
        base.ExitState(agent);
    }
    void SpawnSnot()
    {
        GameObject snot = GameObject.Instantiate(snotPrefab, npcTransform.position, Quaternion.identity);
    }
}
