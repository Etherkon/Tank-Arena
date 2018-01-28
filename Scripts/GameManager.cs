using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int m_NumRoundsToWin = 5;           
    public float m_StartDelay = 4f;            
    public float m_EndDelay = 8f;          
    public Text m_MessageText;     
    public GameObject tank1; 
    public GameObject tank2;       
    public Transform m_SpawnPoint1;
    public Transform m_SpawnPoint2;  
    public Canvas canvas;         
    public Camera cam1;
    public Camera cam2;  
    public AudioSource begin;
    public Light light;
            
    private int m_RoundNumber;                 
    private WaitForSeconds m_StartWait;         
    private WaitForSeconds m_EndWait;           
    private int points1 = 0;
    private int points2 = 0;
    private int m_RoundWinner = 0;        
    private int m_GameWinner;          

    private void Start()
    {
        m_StartWait = new WaitForSeconds (m_StartDelay);
        m_EndWait = new WaitForSeconds (m_EndDelay);

        SpawnAllTanks();
 
        canvas.enabled = false;
        canvas.enabled = true;
        StartCoroutine (GameLoop ());
        cam1.enabled = true;
        cam2.enabled = false;
    }

    private void SpawnAllTanks()
    {
        tank1.SetActive(true);
        tank2.SetActive(true);
    }

    private IEnumerator GameLoop ()
    {
        yield return StartCoroutine (RoundStarting ());
        yield return StartCoroutine (RoundPlaying());
        yield return StartCoroutine (RoundEnding());

        if (m_GameWinner != 0)
        {
            Application.LoadLevel (Application.loadedLevel);
        }
        else
        {
            StartCoroutine (GameLoop ());
        }
    }

    private IEnumerator RoundStarting ()
    {   
       begin.Play();
      
        ResetAllTanks ();
     //   DisableTankControl ();
        EnableTankControl ();

        m_RoundNumber++;
        m_MessageText.text = "ROUND " + m_RoundNumber;

        yield return m_StartWait;
    }

    private IEnumerator RoundPlaying ()
    {
        // EnableTankControl ();

        m_MessageText.text = string.Empty;

        while (!OneTankLeft())
        {
            yield return null;
        }
    }

    private IEnumerator RoundEnding ()
    {
        DisableTankControl ();

        m_RoundWinner = 0;
        m_RoundWinner = GetRoundWinner ();

        if (m_RoundWinner == 1) {
            light.color = Color.blue;
            ++points1; }
        else if (m_RoundWinner == 2) {
            light.color = Color.red;
            ++points2;}

        m_GameWinner = GetGameWinner ();
        string message = EndMessage ();
        m_MessageText.text = message;

        yield return m_EndWait;
    }

    private bool OneTankLeft()
    {
        int numTanksLeft = 0;

         if (tank1.activeSelf) 
                numTanksLeft++;
          if (tank2.activeSelf) 
                numTanksLeft++;
        
        return numTanksLeft <= 1;
    }
    
    private int GetRoundWinner()
    {
        for (int i = 0; i < 2; i++)
        {
            if (!tank1.activeSelf)
                return 2;  
            else if (!tank2.activeSelf)
                return 1;
        }

        return 0;
    }

    private int GetGameWinner()
    {
        for (int i = 0; i < 2; i++)
        {
            if (points1 == m_NumRoundsToWin)
                return 1;
            else if (points2 == m_NumRoundsToWin)
                return 2;
        }

        return 0;
    }

    private string EndMessage()
    {
        string message = "DRAW!";
        string player1 = "<color=#" + ColorUtility.ToHtmlStringRGB(Color.blue) + ">BLUE" + "</color>";
        string player2 = "<color=#" + ColorUtility.ToHtmlStringRGB(Color.red) + ">RED" + "</color>";

        if (m_RoundWinner == 1)
            message = player1 + " WINS THE ROUND !";
        else if (m_RoundWinner == 2)
            message = player2 + " WINS THE ROUND !";

        message += "\n\n\n\n";

        message += player1 + ": " + points1 + " WINS\n";
        message += player2 + ": " + points2 + " WINS\n";    

        if (m_GameWinner == 1)
            message = player1 + " WINS THE GAME !";
        else if (m_GameWinner == 2)
            message = player2 + "WINS THE GAME !";

        return message;
    }

    private void ResetAllTanks()
    {

        tank1.SetActive(false);
        tank2.SetActive(false); 

        tank1.SetActive(true);
        tank2.SetActive(true);
        
        tank1.transform.position = m_SpawnPoint1.position;
        tank2.transform.position = m_SpawnPoint2.position;

        tank1.transform.rotation = Quaternion.Euler(0f,0f,0f);
        tank2.transform.rotation = Quaternion.Euler(0f,180f,0f);
    }

    private void EnableTankControl()
    {
        canvas.enabled = true;
        cam1.enabled = true;
        cam2.enabled = false;
    }

    private void DisableTankControl()
    {
        canvas.enabled = false;
        cam1.enabled = false;
        cam2.enabled = true;
    }
}