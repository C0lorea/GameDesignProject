// using UnityEngine;

// public class Gem : MonoBehaviour
// {
//     private SpriteRenderer gemRenderer;
//     private Collider2D gemCollider;

//     private void Start()
//     {
//         // Get the renderer and collider components
//         gemRenderer = GetComponent<SpriteRenderer>();
//         gemCollider = GetComponent<Collider2D>();
//     }

//     private void OnTriggerEnter2D(Collider2D other)
//     {
//         // Check if the collider's layer is the player layer
//         if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
//         {
//             // Perform any desired effects when the player collects the gem
//             CollectGem();
//         }
//     }

//     private void CollectGem()
//     {
//         // Perform any desired effects when collecting the gem (e.g., increase score, play sound)
//         Debug.Log("Gem collected!");

//         // Disable the renderer and collider to make the gem disappear
//         gemRenderer.enabled = false;
//         gemCollider.enabled = false;

//         // Invoke a method to make the gem appear again after a delay (if desired)
//         Invoke("RespawnGem", 5f);
//     }

//     private void RespawnGem()
//     {
//         // Enable the renderer and collider to make the gem appear again
//         gemRenderer.enabled = true;
//         gemCollider.enabled = true;
//     }
// }
