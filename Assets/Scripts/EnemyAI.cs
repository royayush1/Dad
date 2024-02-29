using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Animator anim;
    public bool stopped = false;
    public Vector3 dir;
    

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        stopped = GetComponent<FollowThePath>().stop;
        anim.SetBool("isRunning", !stopped);
        dir = transform.position;
        dir.Normalize();
        anim.SetFloat("X", dir.x);
        anim.SetFloat("Y", dir.y);
    }
}
