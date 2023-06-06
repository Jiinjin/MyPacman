using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodesMovement : MonoBehaviour
{
    public LayerMask obstacleLayer;
    public List<Vector2> m_availableDirections = new List<Vector2>();

    // Start is called before the first frame update
    void Start()
    {
        m_availableDirections = new List<Vector2>();


        CheckAvailableDirection(Vector2.up);
        CheckAvailableDirection(Vector2.down);
        CheckAvailableDirection(Vector2.left);
        CheckAvailableDirection(Vector2.right);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Check if a Raycast hits walls, if not then add the direction as an available direction for ghosts to use
    private void CheckAvailableDirection(Vector2 direction)
    {
        // Cast a ray straight down.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 0.5f, obstacleLayer);
        Debug.DrawRay(transform.position, direction, Color.white, 10f);
        // If it hits something...
        if (hit.collider == null)
        {
            m_availableDirections.Add(direction);
        }
       
    }
}