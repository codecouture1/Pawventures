using System;
using System.Collections;
using UnityEngine;
using Unity.Cinemachine;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;


public class cameraScript : MonoBehaviour
{
    private CinemachineCamera cinemachineCamera;
    CinemachineBasicMultiChannelPerlin perlin;
    CinemachinePositionComposer posComp;

    private CinemachineConfiner2D confiner;
    private CamConfiner confinerScript;

    private bool _playerInConfiner = false;
    public bool PlayerInConfiner { get { return _playerInConfiner; } }

    public readonly float DEFAULT_TARGET_OFFSET_X = 6f;
    public readonly float DEFAULT_TARGET_OFFSET_Y = 6f;
    public readonly float DEFAULT_FOV = 45f;

    void Awake()
    {
        cinemachineCamera = GetComponent<CinemachineCamera>();
        perlin = cinemachineCamera.GetComponent<CinemachineBasicMultiChannelPerlin>();
        posComp = cinemachineCamera.GetComponent<CinemachinePositionComposer>();

        //confiner = cinemachineCamera.GetComponent<CinemachineConfiner2D>();
        //confinerScript = confiner.BoundingShape2D.gameObject.GetComponent<CamConfiner>();
        //if (confinerScript == null) Debug.Log("confinerShape not found");
    }

    public void Rumble(float intensity)
    {
        perlin.AmplitudeGain = intensity;
    }

    public IEnumerator Rumble(float intensity, float duration)
    {
        perlin.AmplitudeGain = intensity;
        yield return new WaitForSeconds(duration);
        perlin.AmplitudeGain = 0f;
    }

    // Update is called once per frame
    void Update()
    {
    }
    public IEnumerator Zoom(float endFOV, float endTargetOffsetX, float endTargetOffsetY, float duration, bool enableDeadzone)
    {
        Vector3 newOffset = new Vector3(endTargetOffsetX, endTargetOffsetY);
        float time = 0;

        if (enableDeadzone == true)
        {
            while (time < duration)
            {
                cinemachineCamera.Lens.FieldOfView = Mathf.Lerp(cinemachineCamera.Lens.FieldOfView, endFOV, time / duration);
                posComp.TargetOffset = Vector3.Lerp(posComp.TargetOffset, newOffset, time / duration);

                yield return null;
                time += Time.deltaTime;
            }
            posComp.Composition.DeadZone.Enabled = enableDeadzone;
        } else 
        {
            posComp.Composition.DeadZone.Enabled = enableDeadzone;
            while (time < duration)
            {
                cinemachineCamera.Lens.FieldOfView = Mathf.Lerp(cinemachineCamera.Lens.FieldOfView, endFOV, time / duration);
                posComp.TargetOffset = Vector3.Lerp(posComp.TargetOffset, newOffset, time / duration);

                yield return null;
                time += Time.deltaTime;
            }

        }
    }
}
