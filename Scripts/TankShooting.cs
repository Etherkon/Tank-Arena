using UnityEngine;
using UnityEngine.UI;

public class TankShooting : MonoBehaviour
{       
 public int m_PlayerNumber = 1;    
    public Rigidbody m_Shell;                  
    public Transform m_FireTransform;          
    public Slider m_AimSlider;                 
  //  public AudioSource m_ShootingAudio;       
                                        

    private void OnEnable()
    {
    //    m_AimSlider.value = m_MinLaunchForce;
    }

    public void Charge(float value) {
         m_AimSlider.value = value;
    }

    public void EndCharge(float value){
         m_AimSlider.value = value;
    }

    public void Fire (float value)
    {
        Quaternion target = Quaternion.Euler(0f, m_FireTransform.rotation.y + 330f,0f);
        Rigidbody shellInstance =
            Instantiate (m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;
        shellInstance.velocity = value * m_FireTransform.forward; ;

    //    m_ShootingAudio.Play ();
    }
}