using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCState : State
{
    public GameObject snotPrefab;
    private Transform npcTransform; // Store NPC's transform
    private npc npcController; // Store reference to NPC controller
    public float incubationPeriod = 60;
    public bool timerIsRunning = false;
    public float coughInterval = 10f;
    private float nextCoughTime;
    private State returnState;
    public Vector3 Direction { get; set; }
    public LayerMask npcLayer;

    public NPCState(State _returnState, Transform _npcTransform, npc _npcController, GameObject _snotPrefab) : base("NPC") {

        returnState = _returnState;
        npcTransform = _npcTransform;
        npcController = _npcController;
        snotPrefab = _snotPrefab;
    }




    public override void EnterState(npc npc)
    {
        
    }

    public override void ExitState(npc npc)
    {
        base.ExitState(npc);
    }

    public override State Update(npc npc)
    {
        if (npc.isSick)
        {
            return new sickState(this, npcTransform, npcController, snotPrefab);
        }
        
        return this;
    }

    
}
