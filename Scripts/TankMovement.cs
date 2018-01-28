using UnityEngine;

public class TankMovement : MonoBehaviour
{
    public int m_PlayerNumber = 1;              
    public float m_Speed = 12f;               
    public float m_TurnSpeed = 180f;           
    public AudioSource m_MovementAudio;         
    public float m_PitchRange = 0.2f;         

    private string m_MovementAxisName;          
    private string m_TurnAxisName;             
    private Rigidbody m_Rigidbody;            
    private float m_MovementInputValue;      
    private float m_TurnInputValue;        
    private float m_OriginalPitch;            

    private void Awake ()
    {
        m_Rigidbody = GetComponent<Rigidbody> ();
    }

    private void OnEnable ()
    {
        m_Rigidbody.isKinematic = false;
        m_MovementInputValue = 0f;
        m_TurnInputValue = 0f;
    }

    private void OnDisable ()
    {
        m_Rigidbody.isKinematic = true;
    }

    private void Start ()
    {
        m_MovementAxisName = "Vertical";// + m_PlayerNumber;
        m_TurnAxisName = "Horizontal";// + m_PlayerNumber;
        m_OriginalPitch = m_MovementAudio.pitch;
    }

    private void Update ()
    {
        m_MovementInputValue = Input.GetAxis (m_MovementAxisName);
        m_TurnInputValue = Input.GetAxis (m_TurnAxisName);

        EngineAudio ();
    }

    private void EngineAudio ()
    {
        m_MovementAudio.Play ();
    }

    private void FixedUpdate ()
    {
       // Move ();
      //  Turn ();
    }

    public void Move (float value)
    {
        Vector3 movement = transform.forward * value * m_Speed * Time.deltaTime;
        m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
    }

    public void Turn (float value)
    {
        float turn = value * m_TurnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler (0f, turn, 0f);
        m_Rigidbody.MoveRotation (m_Rigidbody.rotation * turnRotation);
    }
}