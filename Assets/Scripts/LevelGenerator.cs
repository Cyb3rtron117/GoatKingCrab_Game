using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] tilePrefabs;
    int tileLength = 16;
    int gridCount = 4;
    private GameObject[] spawnedPrefabs;
    Vector2 nextSpawnPos =Vector2.zero;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnedPrefabs = new GameObject[gridCount];

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < gridCount; i++)
        {
            if (spawnedPrefabs[i] == null)
            {
                spawnedPrefabs[i] = Instantiate(tilePrefabs[0], nextSpawnPos, Quaternion.identity);
                nextSpawnPos += ConvertToIsometric(new Vector3(tileLength * 0.5f, 0, 0));

            }
            else 
            { 
                float cameraY = Camera.main.transform.position.y;
                if (spawnedPrefabs[i].transform.position.y < cameraY - 8)
                {
                    Destroy(spawnedPrefabs[i]);
                    spawnedPrefabs[i] = null;
                }

            }

        }

    }

    public Vector2 ConvertToIsometric(Vector3 cartesianPos)
    {
        // The isometric X is calculated from the horizontal (X) and depth/vertical (Z)
        float isoX = (cartesianPos.x - cartesianPos.z) * Mathf.Cos(Mathf.Deg2Rad * 30);

        // The isometric Y is calculated from the horizontal (X), depth (Z), and height (Y)
        float isoY = (cartesianPos.x + cartesianPos.z) * Mathf.Sin(Mathf.Deg2Rad * 30) + cartesianPos.y;

        return new Vector2(isoX, isoY);

    }
}
