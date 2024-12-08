using System;
using System.Collections;
using UnityEngine;
using Unity.Cinemachine;

public class CameraShakeManagerScript : MonoBehaviour
{
    public static CameraShakeManagerScript instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void CameraShake(CinemachineImpulseSource impulseSource, float shakeForce)
    {
        impulseSource.GenerateImpulseWithForce(shakeForce);
    }

}
