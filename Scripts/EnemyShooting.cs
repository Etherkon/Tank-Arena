using UnityEngine;
using UnityEngine.UI;

public class EnemyShooting : MonoBehaviour
{
    public int m_PlayerNumber = 1;              
    public Rigidbody m_Shell;                  
    public Transform m_FireTransform;              
    public AudioSource m_ShootingAudio;            
    public float m_MinLaunchForce;       
    public float m_MaxLaunchForce;        
    public float m_MaxChargeTime = 0.75f;      

    private string m_FireButton;                
    private float m_CurrentLaunchForce = 100f;        
    private float m_ChargeSpeed;  
    private int shoots = 0;              

    private void OnEnable()
    {
    }

    private void Start ()
    {
        m_FireButton = "Fire" + m_PlayerNumber;
        m_ChargeSpeed = (m_MaxLaunchForce - m_MinLaunchForce) / m_MaxChargeTime;
    }

    private void Update ()
    {
        shoots = Random.Range(0,160);

        if (shoots == 0)
        {
            Fire ();
        }
    }

    private void Fire ()
    {
        Rigidbody shellInstance =
            Instantiate (m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;
        shellInstance.velocity = m_CurrentLaunchForce * m_FireTransform.forward; ;

        m_ShootingAudio.Play ();
    }
}