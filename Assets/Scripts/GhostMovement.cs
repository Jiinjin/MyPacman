using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour
{

    public float m_speed = 5f;
    private Rigidbody2D m_rb;

    private Vector2 m_currentDirection;
    private Vector2 m_nextDirection;
    private Vector2 m_nextPosition;

    public LayerMask obstacleLayer;

    public bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_currentDirection = Vector2.zero;
        m_nextDirection = Vector2.up;
        m_nextPosition = m_rb.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Reset Ghost
    public void ResetGhostMovement()
    {
        m_currentDirection = Vector2.zero;
        m_nextDirection = Vector2.up;
        m_nextPosition = m_rb.position;
    }

    private void FixedUpdate()
    {
        if (isDead == false)
        {
            TryMove();
            Move();
        }
      
    }

    //Check if the ghost can move in the current direction or the next one
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

    //Change the direction of the ghost
    private void MoveInDirection(Vector2 direction)
    {
        m_currentDirection = direction;
        m_nextPosition = m_rb.position + m_currentDirection;
    }

    //Check if the Ghost can move in the given direction
    private bool CanMoveInDirection(Vector2 direction)
    {
        float offset = 0.2f; 

        Collider2D hitCollider = Physics2D.OverlapCircle(m_rb.position + direction * offset, 0.2f, obstacleLayer);
        return (hitCollider == null);
    }

    //Ghosts main movement
    private void Move()
    {

        Vector2 targetPosition = m_rb.position + m_currentDirection * m_speed * Time.fixedDeltaTime;
        m_rb.MovePosition(targetPosition);  
    }

    //Check if ghost collides with a movement node, giving him the available paths where he can go, and select one randomly and set the according direction
    private void OnTriggerEnter2D(Collider2D other)
    {
        NodesMovement node = other.GetComponent<NodesMovement>();

        // Do nothing while the ghost is frightened
        if (node != null)
        {   
           
            // Pick a random available direction
            int index = Random.Range(0, node.m_availableDirections.Count);

            // Prefer not to go back the same direction so increment the index to
            // the next available direction
            if (node.m_availableDirections.Count > 1 && node.m_availableDirections[index] == -m_currentDirection)
            {
                index++;

                // Wrap the index back around if overflowed
                if (index >= node.m_availableDirections.Count)
                {
                    index = 0;
                }
            }

            m_nextDirection = node.m_availableDirections[index];
        }
    }


}
