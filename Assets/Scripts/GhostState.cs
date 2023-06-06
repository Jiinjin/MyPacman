using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostState : MonoBehaviour
{

    public enum GhostStates { Patrol, Scared, Dead };

    private GhostStates m_currentState;

    private Vector3 m_initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        m_currentState = GhostStates.Patrol;
        m_initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetGhost()
    {
        CancelInvoke();
        m_currentState = GhostStates.Patrol;
        transform.position = m_initialPosition;
        if (GetComponent<GhostMovement>() != null)
        {
            GetComponent<GhostMovement>().ResetGhostMovement();
        }

    }

    public void SetGhostState(GhostStates state, int duration = 0)
    {
        switch(state)
        {
            case GhostStates.Dead:
                TransitionDeadGhost(duration);
                break;
            case GhostStates.Patrol:
                TransitionPatrolGhost();
                break;
            case GhostStates.Scared:
                TransitionScaredGhost(duration);
                break;

        }
    }


    public void TransitionDeadGhost(int duration)
    {
        m_currentState = GhostStates.Dead;

        GetComponent<SpriteRenderer>().enabled = false;
        transform.position = m_initialPosition;

        if (GetComponent<GhostMovement>() != null)
        {
            GetComponent<GhostMovement>().isDead = true;
            GetComponent<GhostMovement>().ResetGhostMovement();
        }
       

        Invoke(nameof(TransitionPatrolGhost), duration);

    }

    public void TransitionScaredGhost(int duration)
    {
        m_currentState = GhostStates.Scared;

        GetComponent<SpriteRenderer>().color = Color.blue;

        Invoke(nameof(TransitionPatrolGhost), duration);
    }

    public void TransitionPatrolGhost()
    {
        m_currentState = GhostStates.Patrol;

        if (GetComponent<SpriteRenderer>().enabled == false)
        {
            GetComponent<SpriteRenderer>().enabled = true;
        }
        GetComponent<SpriteRenderer>().color = Color.red;

        if (GetComponent<GhostMovement>() != null)
        {
            GetComponent<GhostMovement>().isDead = false;
        }
    }

    public GhostStates GetGhostState()
    {
        return m_currentState;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (m_currentState == GhostStates.Scared)
            {
                FindObjectOfType<GameManager>().GhostDied(this);
            }
            else
            {
                FindObjectOfType<GameManager>().PacmanDied();
            }
        }
    }
}
