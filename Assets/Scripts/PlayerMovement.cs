using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float m_speed = 5f;
    private Rigidbody2D m_rb;

    private Vector2 m_currentDirection;
    private Vector2 m_nextDirection;
    private Vector2 m_nextPosition;

    public GameManager m_gameManager;

    private Vector3 m_initialPosition;
    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_currentDirection = Vector2.zero;
        m_nextDirection = Vector2.zero;
        m_nextPosition = m_rb.position;
        m_initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        GetNextDirection();
        TryMove();
        Move();
    }

    //Check if we can move in the current direction or the next one
    private void TryMove()
    {
        if (CanMoveInDirection(m_nextDirection))
        {
            MoveInDirection(m_nextDirection);
        }
        else if (CanMoveInDirection(m_currentDirection))
        {
            MoveInDirection(m_currentDirection);
        }
    }

    //Reset Pacman 
    public void ResetPacman()
    {
        m_currentDirection = Vector2.zero;
        m_nextDirection = Vector2.zero;
        transform.position = m_initialPosition;
    }

    //Change the direction of Pacman
    private void MoveInDirection(Vector2 direction)
    {
        m_currentDirection = direction;
        m_nextPosition = m_rb.position + m_currentDirection;
    }

    //Get Input in order to apply Pacman's next direction 
    private void GetNextDirection()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        if (horizontalInput != 0)
        {
            m_nextDirection = new Vector2(horizontalInput, 0f);
        }
        else if (verticalInput != 0)
        {
            m_nextDirection = new Vector2(0f, verticalInput);
        }

    }

    //Pacman main movement
    private void Move()
    {
        Vector2 targetPosition = m_rb.position + m_currentDirection * m_speed * Time.fixedDeltaTime;
        m_rb.MovePosition(targetPosition);
    }

    //Check if Pacman can move in the given direction
    private bool CanMoveInDirection(Vector2 direction)
    {
        float offset = 0.2f; // Adjust this value as needed

        Collider2D hitCollider = Physics2D.OverlapCircle(m_rb.position + direction * offset, 0.2f, LayerMask.GetMask("Walls"));
        return (hitCollider == null);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //When Pacman collides with a score pellet, destroy the pellet, add score and check if it was the last one in  order to end the game
        if (collision.gameObject.tag == "ScorePellet")
        {
            Destroy(collision.gameObject);
            m_gameManager.AddScore(100);
            m_gameManager.CheckIfRemainingScorePellets();
        }

        //When Pacman collides with a power pellet, destroy the pellet and call the according function for the game manager
        if (collision.gameObject.tag == "PowerPellet")
        {
            Destroy(collision.gameObject);
            m_gameManager.PowerPelletEaten();
            
        }

        //When Pacman collides with the TPZone, Teleports PAcman to the other side of the map
        if (collision.gameObject.tag == "TPZone")
        {
            transform.position = Vector3.Scale(transform.position, new Vector3(-1f, 1f, 1f));
        }
    }

}
