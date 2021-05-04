using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    //code snippets from https://medium.com/nice-things-ios-android-development/basic-2d-screen-shake-in-unity-9c27b56b516

    // Desired duration of the shake effect
    private float shakeDuration = 0f;

    // A measure of magnitude for the shake. Tweak based on your preference
    private float shakeMagnitude = 0.5f;

    // A measure of how quickly the shake effect should evaporate
    private float dampingSpeed = 1.0f;

    // The initial position of the GameObject
    Vector3 initialPosition;

    void Update()
    {
        if (shakeDuration > 0)
        {
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;

            shakeDuration -= Time.deltaTime * dampingSpeed;
            if (shakeDuration <= 0)
            {
                shakeDuration = 0f;
                transform.localPosition = initialPosition;
            }
        }
    }
    public void TriggerShake()
    {
        initialPosition = transform.localPosition;
        shakeDuration = 0.35f;
    }
}
