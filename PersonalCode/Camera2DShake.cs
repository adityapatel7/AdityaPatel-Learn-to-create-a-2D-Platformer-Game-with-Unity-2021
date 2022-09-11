using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2DShake : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float shakeVibrato = 10f;
    [SerializeField] private float shakeRandomness = 0.2f;
    [SerializeField] private float skakeTime = 0.01f;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            Shake();
        }
    }

    public void Shake()
    {
        StartCoroutine(IEShake());
    }
    private IEnumerator IEShake()
    {
        Vector3 currentPos = transform.position;
        for (int i = 0; i < shakeVibrato; i++)
        {
                Vector3 shakePos = currentPos + Random.onUnitSphere * shakeRandomness;
                yield return new WaitForSeconds(skakeTime);
                transform.localPosition = shakePos;
        }
    }
}
