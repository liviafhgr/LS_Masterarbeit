using UnityEngine;

public class LightColorLooper : MonoBehaviour
{
    public Light targetLight;

    private Color[] colors = new Color[]
    {
        new Color32(0xFC, 0xDE, 0x05, 0xFF), // FCDE05
        new Color32(0xE8, 0x42, 0x40, 0xFF), // E84240
        new Color32(0x1C, 0x63, 0xB8, 0xFF), // 1C63B8
        new Color32(0xFF, 0xFF, 0xFF, 0xFF)  // FFFFFF
    };

    private int colorIndex = 0;
    private float timer = 0f;
    private bool loopActive = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (targetLight == null)
            targetLight = GetComponent<Light>();
        Invoke(nameof(StartLoop), 6f);
    }

    void StartLoop()
    {
        loopActive = true;
        timer = 0f;
        colorIndex = 0;
        targetLight.color = colors[colorIndex];
    }

    // Update is called once per frame
    void Update()
    {
        if (!loopActive) return;

        timer += Time.deltaTime;
        if (timer >= 1f)
        {
            timer = 0f;
            colorIndex = (colorIndex + 1) % colors.Length;
            targetLight.color = colors[colorIndex];
        }
    }
}
