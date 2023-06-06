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

    //Reset ghost
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
    //Set the ghost state and call the according function
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
                CancelInvoke("TransitionPatrolGhost");
                TransitionScaredGhost(duration);
                break;

        }
    }

    //Transition to dead ghost, we make the ghost disapear, reset his position and behaviour, and then changing is state back to patrol
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

    //Transition to dead ghost, we make the ghost scared so Pacman can eat it, change color for understanding, and then changing is state back to patrol after a duration
    public void TransitionScaredGhost(int duration)
    {
        m_currentState = GhostStates.Scared;

        GetComponent<SpriteRenderer>().color = Color.blue;

        Invoke(nameof(TransitionPatrolGhost), duration);
    }
    //Transition to patrol ghost, checks if ghost is enabled or not, if not then it means the ghost is dead and needs to be reseted back to patrol settings
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

    //Ghost collides with player, eats player if not in scared mode, or dies if in scared mode
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
