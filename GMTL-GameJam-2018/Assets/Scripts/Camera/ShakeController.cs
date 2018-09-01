using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeController : MonoBehaviour
{
    [Header("Defaults")]
    public float durationDefault = 0.1f;
    public float shakeCycleTimeDefault = 0.01f;
    public float shakeDistanceDefault = 0.05f;

    public void Shake(float duration = -1, float shakeCycleTime = -1, float shakeDistance = -1)
    {
        StartCoroutine(ShakeRoutine(duration < 0 ? durationDefault : duration,
                                    shakeCycleTime < 0 ? shakeCycleTimeDefault : shakeCycleTime,
                                    shakeDistance < 0 ? shakeDistanceDefault : shakeDistance));
    }

    private IEnumerator ShakeRoutine(float duration, float shakeCycleTime, float shakeDistance)
    {
        var origPos = transform.position;

        var iterations = Mathf.RoundToInt(duration / shakeCycleTime);

        for (int i = 0; i < iterations; i++)
        {
            var randomVector = new Vector3((Random.value - 0.5f), (Random.value - 0.5f), (Random.value - 0.5f)).normalized;
            randomVector = randomVector * shakeDistance;
            transform.position = origPos + randomVector;

            yield return new WaitForSeconds(shakeCycleTime);
        }

        transform.position = origPos;

    }
}
