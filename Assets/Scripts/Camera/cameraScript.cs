using System;
using System.Collections;
using UnityEngine;
using Unity.Cinemachine;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;


public class CameraScript : MonoBehaviour
{
    private GameObject referenceManagerObj;
    private ReferenceManager referenceManager;

    private CinemachineCamera cinemachineCamera;
    CinemachineBasicMultiChannelPerlin perlin;
    CinemachinePositionComposer posComp;
    CameraTarget cameraTarget;

    private bool _playerInConfiner = false;
    public bool PlayerInConfiner { get { return _playerInConfiner; } }

    public readonly float DEFAULT_TARGET_OFFSET_X = 6f;
    public readonly float DEFAULT_TARGET_OFFSET_Y = 8f;
    public readonly float DEFAULT_FOV = 45f;

    void Awake()
    {
        referenceManagerObj = GameObject.Find("ReferenceManager");
        referenceManager = referenceManagerObj.GetComponent<ReferenceManager>();

        cinemachineCamera = GetComponent<CinemachineCamera>();
        perlin = cinemachineCamera.GetComponent<CinemachineBasicMultiChannelPerlin>();
        posComp = cinemachineCamera.GetComponent<CinemachinePositionComposer>();
    }

    private void Start()
    {
        SetTrackingTarget(referenceManager.player.transform);
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

    public void SetTrackingTarget(Transform transform)
    {
        cameraTarget.TrackingTarget = transform;
        cinemachineCamera.Target = cameraTarget;
    }
}
