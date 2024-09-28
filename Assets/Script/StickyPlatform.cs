using System.Collections;
using System.Collections.Generic;  
using UnityEngine;

public class StickyPlatform : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer;

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.layer == playerLayer) {

            // Disable gravity on player rigidbody
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            rb.gravityScale = 0f;

            // Set player as child of platform
            collision.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
         if (collision.gameObject.layer == playerLayer) {
            
            // Re-enable gravity 
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            rb.gravityScale = 2f;

            // Clear parenting 
            collision.transform.SetParent(null); 
        }
    }
}