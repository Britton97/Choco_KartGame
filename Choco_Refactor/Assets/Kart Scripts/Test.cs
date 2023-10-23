using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public float frequency = 1f;
    public float amplitude = 1f;
    public float offset = 0f;
    private float time = 0f;
    public bool thing = false;

    private void Update()
    {
        time += Time.deltaTime;

        float y = amplitude * Mathf.Sin(2f * Mathf.PI * frequency * time);

        y = Mathf.Clamp(y, -amplitude, amplitude);

        if(thing)
        {
        transform.position = new Vector3(transform.position.x, y, transform.position.z);

        }
        else
        {

        transform.position = new Vector3(transform.position.x, y + amplitude + offset, transform.position.z);
        }
    }
}
