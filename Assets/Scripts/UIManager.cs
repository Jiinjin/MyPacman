using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Text scoreText;
    public Canvas m_lifeCanvas;

    public GameObject m_PacmanLifePointImage;

    public List<GameObject> m_PacmanLifeList;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Update the score, called when pacman eats a score pellet or eat a ghost in scared mode
    public void UpdateScore(int newScore)
    {
        scoreText.text = "Score: " + newScore;
    }

    //Create the life points of Pacman on the UI
    public void CreateLifePointsUI(int lifePoints)
    {
        for (int i = 0; i < lifePoints; i++)
        {
            GameObject pacmanLife = Instantiate(m_PacmanLifePointImage, m_lifeCanvas.transform);
            pacmanLife.transform.localScale = new Vector3(1, 1, 1);
            m_PacmanLifeList.Add(pacmanLife);
        }
    }

    //Removes one life on the UI 
    public void RemoveOnePacmanLife()
    {
        if (m_PacmanLifeList.Count != 0)
        {
            Destroy(m_PacmanLifeList[m_PacmanLifeList.Count - 1]);
            m_PacmanLifeList.RemoveAt(m_PacmanLifeList.Count - 1);
        }
       
    }


}
