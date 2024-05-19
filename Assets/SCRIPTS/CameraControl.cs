
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField]private float speed;
    private float currentPosX;
    private Vector3 velocity = Vector3.zero;

    [SerializeField]private Transform player;
    [SerializeField]private float aheadDistance;
    [SerializeField]private float aboveDistance;
    //[SerializeField]private float belowDistance;
    [SerializeField]private float cameraSpeed;
    private float lookAhead;
    private float lookAbove;
    //private float lookBelow;



    private void Update(){
        //transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX, transform.position.y, transform.position.z), ref velocity, speed); ROOM CAMERA

        //player camera
        transform.position = new Vector3(player.position.x + lookAhead, transform.position.y, transform.position.z);
        lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed);
        lookAbove = Mathf.Lerp(lookAbove, (aboveDistance * player.localScale.y), Time.deltaTime * cameraSpeed);
        //lookBelow = Mathf.Lerp(lookBelow, (belowDistance * player.localScale, y), Time.deltaTime * cameraSpeed);
    }

    public void MoveToNewRoom(Transform _newRoom){
        currentPosX = _newRoom.position.x;
    }

}
