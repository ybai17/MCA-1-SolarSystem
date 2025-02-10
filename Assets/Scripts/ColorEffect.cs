using UnityEngine;

public class ColorEffect : MonoBehaviour
{
    public Color color1 = Color.yellow;
    public Color color2 = new Color32(252, 186, 3, 0); //orange by default

    [SerializeField]
    private float changingSpeed = 5.0f;

    private Renderer renderer;
    float step;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        renderer = GetComponent<Renderer>();
        Debug.Log("Sun starting color = " + renderer.material.color);
    }

    // Update is called once per frame
    void Update()
    {
        step = Mathf.PingPong(Time.time, 1 / changingSpeed * 2);

        renderer.material.color = Color.Lerp(color1, color2, step);
    }
}
