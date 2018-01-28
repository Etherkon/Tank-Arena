using UnityEngine;

using System.Collections;




public class ControlsForward : MonoBehaviour {

	

     private bool moves;
     public GameObject player;
     private TankMovement moving;
     public float speed = 0f;

     void Start() {
         moves = false; 
         moving = player.GetComponent<TankMovement>();
     }

     void Update() {
         if(moves == true){
		moving.Move(speed);
         }
     }

     public void go() {
         moves = true;
     }
     public void stop() {
         moves = false;
     }

}