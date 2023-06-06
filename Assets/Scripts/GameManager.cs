using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public UIManager m_UIManager;

    private int m_score;

    public GhostState[] ghosts;

    public int m_ghostScaredDuration = 7;
    public int m_ghostDeadDuration = 4;

    // Start is called before the first frame update
    void Start()
    {
        SetScore(0);
        m_UIManager.UpdateScore(m_score);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetScore()
    {
        return m_score;
    }
    public void AddScore(int scoreToAdd)
    {
        m_score += scoreToAdd;
        m_UIManager.UpdateScore(m_score);
    }

    public void SetScore(int newScore)
    {
        m_score = newScore;
    }

    public void PowerPelletEaten()
    {
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].SetGhostState(GhostState.GhostStates.Scared, m_ghostScaredDuration);
        }
    }

    public void GhostDied(GhostState ghost)
    {
        //score

        ghost.TransitionDeadGhost(m_ghostDeadDuration);


    }
}
