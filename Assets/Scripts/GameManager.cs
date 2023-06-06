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

    public GameObject m_scorePelletsTileMap;

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

    //Called when Pacman eats a power pellet
    public void PowerPelletEaten()
    {
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].SetGhostState(GhostState.GhostStates.Scared, m_ghostScaredDuration);
        }
    }

    //Called when Pacman ate a ghost in scared mode
    public void GhostDied(GhostState ghost)
    {
        AddScore(300);

        ghost.TransitionDeadGhost(m_ghostDeadDuration);


    }

    //Called when ghost eats pacman in normal mode
    public void PacmanDied()
    {
        //Remove a life point
        m_lifePoints -= 1;

        //Check if any remaining lives, if so, reset ghost and pacman behaviour, else end game
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
            EndGame();
        }
    }

    public void EndGame()
    {
        SceneManager.LoadScene(2);
    }

    //Called when a score pellet is eaten, checks how many scorepellets left in order to end the game if needed
    public void CheckIfRemainingScorePellets()
    {
        if (m_scorePelletsTileMap.transform.childCount == 1)
        {
            EndGame();
        }
    }
}
