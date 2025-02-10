using UnityEngine;

public class RevolveAround : MonoBehaviour
{

    [SerializeField]
    private float revolutionSpeed = 0.0f;

    [SerializeField]
    private Transform revolveAroundPoint;

    private Vector3 revolveAroundAxis;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        revolveAroundAxis = Vector3.up;

        if (!revolveAroundPoint) {
            if (GameObject.FindGameObjectWithTag("Sun")) {
                revolveAroundPoint = GameObject.FindGameObjectWithTag("Sun").transform;
            } else {
                Debug.Log("Missing a reference object to revolve around. Please reference the Sun " +
                "object through the inspector and ensure the Sun is tagged appropriately.");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {   
        if (!revolveAroundPoint) {
            return;
        }

        transform.RotateAround(revolveAroundPoint.position,
                                revolveAroundAxis,
                                revolutionSpeed * Time.deltaTime);
    }
}
