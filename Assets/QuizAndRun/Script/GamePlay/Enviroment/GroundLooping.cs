using UnityEngine;
using UnityEngine.Tilemaps;

public class GroundLooping : MonoBehaviour
{
    [SerializeField] GameObject[] listGround;
    [SerializeField] Transform player;
    [SerializeField] Transform cicle;

    private GameObject currentGround;
    private int CurrentGroundIndex = 0;
    private float groundSizeX;
    private void Awake()
    {
        
        if (listGround != null)
        {
            currentGround = listGround[0];
            groundSizeX = currentGround.GetComponent<BoxCollider2D>().bounds.size.x;
        }
    }

    private void Update()
    {

        if(CameraInfo.GetCameraPosition.x + (CameraInfo.GetCameraSize.x / 2) + 4f > currentGround.transform.position.x + groundSizeX / 2)
        {
            if(CurrentGroundIndex <= listGround.Length - 2) 
            {
                CurrentGroundIndex++;
            }
            else
            {
                CurrentGroundIndex = 0;
            }
            listGround[CurrentGroundIndex].transform.position = currentGround.transform.position + new Vector3(groundSizeX , currentGround.transform.position.y, 0f);
            currentGround = listGround[CurrentGroundIndex];
        }
    }
}
