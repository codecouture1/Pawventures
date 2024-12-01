using System;
using System.Collections;
using UnityEngine;
using Unity.Cinemachine;
using System.Runtime.CompilerServices;

public class cameraScript : MonoBehaviour
{
    private CinemachineCamera cinemachineCamera;
    CinemachineBasicMultiChannelPerlin perlin;
    CinemachinePositionComposer posComp;

    public readonly float DEFAULT_TARGET_OFFSET_X = 12.2f;
    public readonly float DEFAULT_FOV = 17.5f;

    void Awake()
    {
        cinemachineCamera = GetComponent<CinemachineCamera>();
        perlin = cinemachineCamera.GetComponent<CinemachineBasicMultiChannelPerlin>();
        posComp = cinemachineCamera.GetComponent<CinemachinePositionComposer>();
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

    public IEnumerator Zoom(float endFOV, float endTargetOffset, float duration)
    {
        Vector3 newOffset = new Vector3(endTargetOffset, 0f, 0f);
        float time = 0;
        while (time < duration)
        {
            //TODO: X und Y Offset smooth anpassen während dem Zoom
            //try: podComp.Offset = Vector3.Lerp <--Reinfuchsen
            cinemachineCamera.Lens.OrthographicSize = Mathf.Lerp(cinemachineCamera.Lens.OrthographicSize, endFOV, time / duration);
            posComp.TargetOffset = Vector3.Lerp(posComp.TargetOffset, newOffset, time / duration);
            yield return null;
            time += Time.deltaTime;
        }
    }

}
