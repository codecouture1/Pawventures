using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CloudSpawner : MonoBehaviour
{
    [Header("Referenzen")]
    public RectTransform canvasRect;
    public Image cloudTemplate;
    public Sprite[] cloudSprites;
    public RectTransform[] spawnPoints; // ← Die leeren GameObjects (RectTransforms)

    [Header("Wolkenverhalten")]
    public int cloudsPerSpawnPoint = 2;
    public Vector2 speedRange = new Vector2(20f, 60f);
    public Vector2 sizeRange = new Vector2(0.8f, 1.5f);

    private List<Cloud> clouds = new List<Cloud>();

    void Start()
    {
        foreach (RectTransform spawnPoint in spawnPoints)
        {
            for (int i = 0; i < cloudsPerSpawnPoint; i++)
            {
                SpawnCloudAt(spawnPoint);
            }
        }
    }

    void Update()
    {
        foreach (var cloud in clouds)
        {
            cloud.Update();
        }
    }

    void SpawnCloudAt(RectTransform spawnPoint)
    {
        Image newCloud = Instantiate(cloudTemplate, canvasRect);
        newCloud.gameObject.SetActive(true);
        newCloud.sprite = cloudSprites[Random.Range(0, cloudSprites.Length)];

        RectTransform rt = newCloud.GetComponent<RectTransform>();

        float speed = Random.Range(speedRange.x, speedRange.y);
        float scale = Random.Range(sizeRange.x, sizeRange.y);

        rt.anchoredPosition = spawnPoint.anchoredPosition;
        rt.localScale = new Vector3(scale, scale, 1f);
       // rt.SetAsLastSibling(); auskommentiert


        clouds.Add(new Cloud(rt, speed, rt.anchoredPosition.y));
    }

    class Cloud
    {
        private RectTransform rt;
        private float speed;
        private float baseY;
        private float swayAmplitude;
        private float swayFrequency;
        private float timeOffset;

        public Cloud(RectTransform rt, float speed, float baseY)
        {
            this.rt = rt;
            this.speed = speed;
            this.baseY = baseY;
            this.swayAmplitude = Random.Range(3f, 8f);
            this.swayFrequency = Random.Range(0.5f, 1.5f);
            this.timeOffset = Random.Range(0f, 100f);
        }

        public void Update()
        {
            Vector2 pos = rt.anchoredPosition;
            pos.x += speed * Time.deltaTime;
            pos.y = baseY + Mathf.Sin((Time.time + timeOffset) * swayFrequency) * swayAmplitude;
            rt.anchoredPosition = pos;

            float canvasWidth = rt.parent.GetComponent<RectTransform>().rect.width;
            if (pos.x > canvasWidth / 2f + 300f)
            {
                pos.x = -canvasWidth / 2f - 300f;
                rt.anchoredPosition = pos;
            }
        }
    }
}
