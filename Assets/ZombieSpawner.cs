using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombie;
    [SerializeField] private float spawnRate;
    [SerializeField] private float timer = 0;
    [SerializeField] private float direction;
    private float lastTotalKills = 0;
    public float zombieSpeed = 4f;
    public LogicScript logic1;
    public Danger dangerscript;
    private bool comingFromRight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        logic1 = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        spawnRate = 5;
        spawnZombie();
    }

    // Update is called once per frame
    void Update()
    {
        if (!logic1.gameOverobj.activeSelf)
        {
                /// runs only when:
                /// 1. total kills are more than 0
                /// 2. total kills are a multiple of 10
                /// 3. spawnrate is more than 1
                /// 4. total kills are more than last total kills recorded when this condition ran (so it doesnt run more than once on same number of kills)
            if (logic1.totalKills != 0 && logic1.totalKills % 5 == 0 && spawnRate > 1 && logic1.totalKills != lastTotalKills)
            {
                Debug.Log("Zombies are FASTER!");
                spawnRate -= 0.5f;
                zombieSpeed += 1f;
                lastTotalKills = logic1.totalKills;
            }

            if (timer < spawnRate)
            {
                timer += Time.deltaTime;
            }
            else
            {
                spawnZombie();
                timer = 0;
            }
        }
    }

    void spawnZombie()
    {
        direction = ChooseBetween(-22f, 22f);
        GameObject newZombie = Instantiate(zombie, new Vector3(direction, -3.6f, transform.position.z), transform.rotation);

        //zombie danger direction indicator
        comingFromRight = direction == 22f ? true : false;
        dangerscript.ActivateIndicator(comingFromRight);
    }

    public T ChooseBetween<T>(T optionA, T optionB)
    {
        return UnityEngine.Random.value < 0.5f ? optionA : optionB;
    }
}
