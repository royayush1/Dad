using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCough : MonoBehaviour
{

    
    public float coughRange = 1f;
    public float coughCoolDown = 5f;
    private float coughTimer = 0f;
    private bool canCough = true;
    private bool infectedNPC = false;
    public Vector3 Direction { get; set; }
    public LayerMask npcLayer;
    public GameObject snotPrefab;

 


    // Update is called once per frame
    void Update()
    {
        if (!canCough)
        {
            
            coughTimer += Time.deltaTime;
            if (coughTimer >= coughCoolDown)
            {
                canCough = true;
                coughTimer = 0f;
            }
        }


        
        if (canCough && Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Cough");
            PerformCough();

            if (!infectedNPC)
            {
                SpawnSnot();
            }
            
            canCough = false;
        }

        infectedNPC = false;
    }

    void PerformCough()
    {
        
        
        GameObject[] npcs = GameObject.FindGameObjectsWithTag("inContact");
        foreach (GameObject npcObject in npcs)
        {
            Vector3 directionToNPC = npcObject.transform.position - transform.position;
            float dotProduct = Vector3.Dot(transform.forward, directionToNPC.normalized);
            
            Debug.Log(dotProduct);
            Debug.Log(directionToNPC.magnitude);
            
            if (directionToNPC.magnitude < coughRange)
            {
                
                npc npcController = npcObject.GetComponent<npc>();
                if (npcController != null && !npcController.isSick)
                {
                    Debug.Log("infected");
                    npcController.MakeSick();
                    infectedNPC = true;
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, coughRange);
    }

    void SpawnSnot()
    {
        Instantiate(snotPrefab, transform.position, Quaternion.identity);
    }

}
