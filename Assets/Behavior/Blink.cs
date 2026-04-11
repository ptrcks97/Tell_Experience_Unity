using UnityEngine;
using System.Collections;

public class Blink : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer body;
    [SerializeField] string leftEye = "Eye_Blink_L";
    [SerializeField] string rightEye = "Eye_Blink_R";
    private float currentBlinkWeight = 0f;
    private int blinkIndexL = -1;
    private int blinkIndexR = -1;

    void Start()
    {
        Mesh mesh = body.sharedMesh;
        blinkIndexL = mesh.GetBlendShapeIndex(leftEye);
        blinkIndexR = mesh.GetBlendShapeIndex(rightEye);
        StartCoroutine(blinkLoop());
    }

    private IEnumerator blinkLoop()
    {
        float durationLeft;
        float minOpenDuration;
        float maxOpenDuration;
        float waitTime;
        float doubleBlinkProp;
        float totalBlinkDuration;
        float openDuration;
        float closeDuration;
        float closedDuration = 50;
        float intervalMin = 3000f;
        float intervalMax = 5000f;

        while (true)
        {
            totalBlinkDuration = Random.Range(150, 450);
            durationLeft = totalBlinkDuration - closedDuration;
            minOpenDuration = durationLeft / 2 + 1;
            maxOpenDuration = durationLeft - 1;
            openDuration = Random.Range(minOpenDuration, maxOpenDuration);
            closeDuration = durationLeft - openDuration;
            waitTime = Random.Range(intervalMin, intervalMax);

            yield return new WaitForSeconds(waitTime / 1000);
            yield return StartCoroutine(blinkAnim(0f, 100f, closeDuration / 1000));
            yield return new WaitForSeconds(closedDuration / 1000);
            yield return StartCoroutine(blinkAnim(100f, 0f, openDuration / 1000));
            doubleBlinkProp = Random.Range(0, 100);
            if  (doubleBlinkProp > 70)
            {
                yield return StartCoroutine(blinkAnim(0f, 100f, closeDuration / 1000));
                yield return new WaitForSeconds(closedDuration / 1000);
                yield return StartCoroutine(blinkAnim(100f, 0f, openDuration / 1000));
            }
        }
    }

    private IEnumerator blinkAnim(float from, float to, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed/duration);
            currentBlinkWeight = Mathf.Lerp(from, to, t);
            yield return null;
        }
        currentBlinkWeight = to;
        
    }

    private void LateUpdate()
    {
        body.SetBlendShapeWeight(blinkIndexL, currentBlinkWeight);
        body.SetBlendShapeWeight(blinkIndexR, currentBlinkWeight);
    }
}
