using UnityEngine;

public class redlight : MonoBehaviour
{
    [SerializeField] private Light targetLight;
    [SerializeField] private float intervalSeconds = 1f;
    [SerializeField] private Color colorA = Color.red;
    [SerializeField] private Color colorB = Color.white;

    private float timer = 0f;
    private bool useB = false;

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        if (targetLight == null) targetLight = GetComponent<Light>();
    }

    // OnEnable is called when the object becomes enabled and active
    void OnEnable()
    {
        timer = 0f;
        useB = false;
        if (targetLight != null) targetLight.color = colorA;
    }

    // Update is called once per frame
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
