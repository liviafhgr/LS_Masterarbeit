using UnityEngine;

public class redlight : MonoBehaviour
{
    [SerializeField] private Light targetLight;
    [SerializeField] private float intervalSeconds = 1f;
    [SerializeField] private Color colorA = Color.red;
    [SerializeField] private Color colorB = Color.white;

    private float timer = 0f;
    private bool useB = false;

    void Awake()
    {
        if (targetLight == null) targetLight = GetComponent<Light>();
    }

    void OnEnable()
    {
        timer = 0f;
        useB = false;
        if (targetLight != null) targetLight.color = colorA;
    }

    void Update()
    {
        if (targetLight == null) return;

        timer += Time.deltaTime;
        if (timer >= intervalSeconds)
        {
            timer = 0f;
            useB = !useB;
            targetLight.color = useB ? colorB : colorA;
        }
    }
}
