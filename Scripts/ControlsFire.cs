using UnityEngine;

using System.Collections;


using UnityEngine.UI;

public class ControlsFire : MonoBehaviour {

	

    public Rigidbody m_Shell;                  
    public Transform m_FireTransform;          
    public Slider m_AimSlider;   
   public AudioSource m_ShootingAudio;    

     private bool loads;
     public GameObject player;
     private TankShooting gun;
     private float current = 0f;
     public float m_MinLaunchForce = 50f;       
     public float m_MaxLaunchForce = 90f;        
     public float m_MaxChargeTime = 0.75f; 
     public float m_ChargeSpeed = 0f;  
     private WaitForSeconds m_EndWait;  

     private int counter = 0;
     

     void Start() {
         m_AimSlider.value = m_MinLaunchForce;
         loads = false; 
         m_ChargeSpeed = (m_MaxLaunchForce - m_MinLaunchForce) / m_MaxChargeTime; 
         gun = player.GetComponent<TankShooting>();
         current = m_MinLaunchForce;
         m_EndWait = new WaitForSeconds (2f);
     }

     void Update() {
         m_AimSlider.value = m_MinLaunchForce;
         if(loads == true ){
               m_AimSlider.value = current;  
               current += m_ChargeSpeed * Time.deltaTime;
               if(current >= m_MaxLaunchForce){
                  current = m_MaxLaunchForce;
                }               
         }
     }

     public void load() {
         loads = true;
     }
     public void hit() {
         loads = false;
         m_AimSlider.value = m_MinLaunchForce;
         StartCoroutine (Fire());
         current = m_MinLaunchForce;
     }

      private IEnumerator Fire ()
     {
        Quaternion target = Quaternion.Euler(0f, m_FireTransform.rotation.y + 330f,0f);
        Rigidbody shellInstance =
            Instantiate (m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;
        shellInstance.velocity = current  * m_FireTransform.forward; ;

        m_ShootingAudio.Play ();
        yield return m_EndWait;
    }

}