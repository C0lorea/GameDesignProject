using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpoint;
    private Transform currentCheckpoint;
    private Health playerHealth;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
    }

    public void Respawn()
    {
        if (currentCheckpoint != null)
        {
            // Restore player health and reset animation
            playerHealth.Respawn(); 
            // Move player to checkpoint location
            transform.position = currentCheckpoint.position; 

            // Move the camera to the checkpoint's room
            Camera.main.GetComponent<CameraControl>().MoveToNewRoom(currentCheckpoint.parent);

            // Enable player movement script
            GetComponent<PlayerMovements>().enabled = true;

            // Reset any input variables or flags that might prevent movement
            GetComponent<PlayerMovements>().ResetMovementInput();

            // Optionally, reset animation states that might block movement
            GetComponent<Animator>().SetBool("Run", false);
        }
        else
        {
            Debug.LogWarning("No checkpoint assigned. Player cannot respawn.");
            // Optionally handle the case when no checkpoint is assigned
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform;
            //SoundManager.instance.PlaySound(checkpoint);
            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<Animator>().SetTrigger("Appear");
        }
    }
}