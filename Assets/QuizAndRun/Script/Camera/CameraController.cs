using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform originalTransform;
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset;
    [SerializeField] float smooth;
    [SerializeField] float shakeDuration, shakeMagnitude;

    private Vector3 velocity = Vector3.zero;
    private Vector3 originalPosition;
    private void Awake()
    {
        originalTransform = transform;
        originalPosition = originalTransform.localPosition;
    }
    public void LateUpdate()
    {
        if (target == null) return;
        //transform.position = Vector3.SmoothDamp(transform.position, target.position + offset, ref velocity , smooth);   
    }
    public void ShakeCamera()
    {
        StartCoroutine(Shake());
    }


    private IEnumerator Shake()
    {
        float elapsedTime = 0.0f;

        while (elapsedTime < shakeDuration)
        {
            float xOffset = Random.Range(-1f, 1f) * shakeMagnitude;
            float yOffset = Random.Range(-1f, 1f) * shakeMagnitude;

            originalTransform.localPosition = originalPosition + new Vector3(xOffset, yOffset, 0);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        originalTransform.localPosition = originalPosition;
    }


}
