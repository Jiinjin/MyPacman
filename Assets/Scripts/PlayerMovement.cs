using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float m_speed = 5f;
    private Rigidbody2D m_rb;
    private Vector2 m_direction;
    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_direction = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        GetDirection();
        Move();
    }

    private void GetDirection()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        if (horizontalInput != 0)
        {
            m_direction = new Vector2(horizontalInput, 0f);
        }
        else if (verticalInput != 0)
        {
            m_direction = new Vector2(0f, verticalInput);
        }

    }

    private void Move()
    {
        Vector2 targetPosition = m_rb.position + m_direction * m_speed * Time.fixedDeltaTime;
        m_rb.MovePosition(targetPosition);
    }

}
