using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public UIManager m_UIManager;

    private int m_score;

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
}
