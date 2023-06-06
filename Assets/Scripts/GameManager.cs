using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public UIManager m_UIManager;

    private int m_score;

    public GhostState[] ghosts;
    public PlayerMovement m_Pacman;

    public int m_ghostScaredDuration = 7;
    public int m_ghostDeadDuration = 4;

    public int m_lifePoints = 3;

    // Start is called before the first frame update
    void Start()
    {
        SetScore(0);
        m_UIManager.UpdateScore(m_score);
        m_UIManager.CreateLifePointsUI(m_lifePoints);
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
        AddScore(300);

        ghost.TransitionDeadGhost(m_ghostDeadDuration);


    }

    public void PacmanDied()
    {
        m_lifePoints -= 1;
        if (m_lifePoints != 0)
        {
            for (int i = 0; i < ghosts.Length; i++)
            {
                ghosts[i].ResetGhost(); ;
            }

            m_Pacman.ResetPacman();

            
            m_UIManager.RemoveOnePacmanLife();
        }
        else
        {
            SceneManager.LoadScene(2);
        }


    }
}
