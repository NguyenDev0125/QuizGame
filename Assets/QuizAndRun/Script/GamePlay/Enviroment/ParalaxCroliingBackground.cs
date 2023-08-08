using UnityEngine;

public class ParalaxCroliingBackground : MonoBehaviour
{
    [SerializeField] Transform cameraTransform;
    [SerializeField] float paralaxMultipler;

    private float startPos;
    private float length;
    private void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x  ;
        
    }

    private void LateUpdate()
    {
        float temp = (cameraTransform.position.x * (1 - paralaxMultipler));
        float dist = cameraTransform.position.x * paralaxMultipler;
        transform.position = new Vector3(startPos + dist , transform.position.y , transform.position.z);
        if (temp > startPos + length) startPos += length;
        else if(temp < startPos - length) startPos -= length;

    }
}
