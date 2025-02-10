using UnityEngine;

public class Rotation : MonoBehaviour
{

    [SerializeField]
    private float rotationSpeed = 1.0f;

    private Vector3 rotateVector = Vector3.zero;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rotateVector = new Vector3(0, rotationSpeed, 0);
        //Debug.Log("Rotation Vector3 = " + rotateVector);
    }

    // Update is called once per frame
    void Update()
    {
        rotateVector.y = rotationSpeed * Time.deltaTime;

        //Debug.Log("rotateVector = " + rotateVector);

        transform.Rotate(rotateVector);
    }
}
