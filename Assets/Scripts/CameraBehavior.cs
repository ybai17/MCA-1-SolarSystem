using UnityEngine;
using UnityEngine.UI;

public class CameraBehavior : MonoBehaviour
{
    private Vector3 originalPosition = new Vector3(0, 0, -70);
    private Vector3 originalRotation = Vector3.zero;

    [SerializeField]
    private Transform targetObject;
    private Vector3 targetPosition;

    //Earth distance from Sun + 2.0 units more so you can catch the Earth with good timing :)
    [SerializeField]
    private float minimumDistance = 20;

    [SerializeField]
    private float zoomSpeed = 10;

    [SerializeField]
    private float revolvingSpeed = 10; //use Earth revolution speed as default, degrees/sec

    //false = zoomed out, static
    //true = zoomed in, revolve around sun
    private bool zoomedInState = false;
    private bool zoomingIn = false; //tracking whether or not it's currently zooming in
    private bool zoomingOut = false; //tracking whether or not it's currently zooming out

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!targetObject) {
            if (GameObject.FindGameObjectWithTag("Sun")) {
                targetObject = GameObject.FindGameObjectWithTag("Sun").transform;
            } else {
                Debug.Log("Camera unable to locate Sun. Please make sure the Sun object exists and is tagged.");
            }
        }

        Debug.Log("Target object = " + targetObject);

        targetPosition = new Vector3(targetObject.position.x,
                                     targetObject.position.y,
                                     targetObject.position.z - minimumDistance);
    }

    // Update is called once per frame
    void Update()
    {
        if (!targetObject)
            return;

        if (Input.GetKeyDown(KeyCode.Space)) {
            ToggleZoom();
        }

        if (zoomingIn) {
            ZoomIn();
        }

        if (zoomingOut) {
            ZoomOut();
        }

        if (Vector3.Distance(transform.position, targetObject.position) == minimumDistance) {
            Debug.Log("###############DISTANCE BETWEEN REACHED 20");
            zoomedInState = true;
            zoomingIn = false;
        }

        if (Vector3.Distance(transform.position, originalPosition) == 0) {
            Debug.Log("###############At ORIGINAL position");
            zoomedInState = false;
            zoomingIn = false;
            zoomingOut = false;
        }

        if (zoomedInState) {
            Revolve();
        }

        if (!zoomedInState) {
            RevolveStop();
        }
    }

    //helper function to update the camera state
    void ToggleZoom()
    {
        if (!zoomedInState) {
            Debug.Log("Zooming IN");
            zoomingIn = true;
            zoomingOut = false;
        } else {
            Debug.Log("Zooming OUT");
            zoomingIn = false;
            zoomingOut = true;
            zoomedInState = false;
        }
    }

    void ZoomIn()
    {
        transform.position = Vector3.MoveTowards(transform.position,
                                                 targetPosition,
                                                 zoomSpeed * Time.deltaTime);
        Debug.Log("Zooming in");
        Debug.Log("Revolve = ON");
    }

    void ZoomOut()
    {
        transform.position = Vector3.MoveTowards(transform.position,
                                                 originalPosition,
                                                 zoomSpeed * Time.deltaTime);
        Debug.Log("Zooming out");
        Debug.Log("Revolve = OFF");
    }

    void Revolve()
    {
        transform.RotateAround(targetObject.position, Vector3.up, revolvingSpeed * Time.deltaTime);
        transform.LookAt(targetObject);

        Debug.Log("REVOLVING");
    }

    void RevolveStop()
    {
        transform.RotateAround(transform.position, Vector3.up, 0);
        transform.LookAt(targetObject);

        Debug.Log("stopped revolving");
    }
}
