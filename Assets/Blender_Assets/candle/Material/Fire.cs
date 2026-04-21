using UnityEngine;

public class Fire : MonoBehaviour
{   
    public Light pointLight;
    public float minIntensity = 1.5f;
    public float maxIntensity = 3f;

    public float speed = 10f;
    // Update is called once per frame
    void Update()
    {
        float noise = Mathf.PerlinNoise(Time.time * speed, 0.0f);
        pointLight.intensity = Mathf.Lerp(minIntensity, maxIntensity, noise);
    }
}
