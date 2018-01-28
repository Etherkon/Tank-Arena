using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    Transform player;                 
    NavMeshAgent nav;   
    
    private float distance = 0f;           

    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player").transform;
        nav = GetComponent <NavMeshAgent> ();
    }

    void Update ()
    {
          distance = Vector3.Distance (player.transform.position, transform.position);
          if(distance > 35f){
            nav.enabled = true;
            nav.SetDestination (player.position);
          }
          else if(distance > 25f) {nav.enabled = false;}
          else { transform.Translate(0f,0f,-1f/7); }
    } 
}