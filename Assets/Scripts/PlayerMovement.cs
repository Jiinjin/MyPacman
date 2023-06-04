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
    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_currentDirection = Vector2.zero;
        m_nextDirection = Vector2.zero;
        m_nextPosition = m_rb.position;
    }

    // Update is called once per frame
    void Update()
    {
        GetNextDirection();
        TryMove();
        Move();
    }

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

    private void MoveInDirection(Vector2 direction)
    {
        m_currentDirection = direction;
        m_nextPosition = m_rb.position + m_currentDirection;
    }

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

    private void Move()
    {
        Vector2 targetPosition = m_rb.position + m_currentDirection * m_speed * Time.fixedDeltaTime;
        m_rb.MovePosition(targetPosition);
    }

    private bool CanMoveInDirection(Vector2 direction)
    {
        float offset = 0.2f; // Adjust this value as needed

        Collider2D hitCollider = Physics2D.OverlapCircle(m_rb.position + direction * offset, 0.2f, LayerMask.GetMask("Walls"));
        return (hitCollider == null);
    }

}
