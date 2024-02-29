using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npc : MonoBehaviour
{
    // REPLACE THIS WITH NPC ANIMATIONS THEN DELETE NPCANIMATIONHANDLER SCRIPT AND PLAYER CONSTANT
    //public npcAnimationHandler normal, sick;
    public State currState;
    private float speedModifer = 0.5f;
    public bool isSick = false;
    public GameObject snotPrefab;


    public void Start()
    {
        currState = new NPCState(currState, transform, this, snotPrefab);
        currState.EnterState(this);
    }

    public void MakeSick()
    {
        isSick = true;
    }

    //public void CureSickness()
    //{
        //currState = new healthyState(currState);
        //isSick = false;
    //}

    

    // Update is called once per frame
    void Update()
    {
        
        State nextState = currState.Update(this);
        if (!nextState.Name.Equals(currState.Name))
        {
            currState.ExitState(this);
            nextState.EnterState(this);

            currState = nextState;
        }
        
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    //Check if this agent is close enough to the target position to interact with it
    public bool CloseEnough(Vector3 position)
    {
        float dist = (GetPosition() - position).sqrMagnitude;
        return dist < 0.01f;
    }

    public void npcStateNormal()
    {
        //normal.enabled = true;
        //sick.enabled = isSick;
    }
    public void npcStateSick()
    {
        //normal.enabled = false;
        //sick.enabled = isSick;
    }


    public void SetSpeedModifierHalf()
    {
        speedModifer = 0.25f;
    }
    public void SetSpeedModifierNormal()
    {
        speedModifer = 0.5f;
    }

}
