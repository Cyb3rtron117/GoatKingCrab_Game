using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelGenerator : MonoBehaviour
{
    [Header("Tile Prefabs")]
    [SerializeField] private GameObject[] tilePrefabs;
    int tileLength = 16;
    int gridCount = 4;
    private GameObject[] spawnedPrefabs;
    Vector2 nextSpawnPos = Vector2.zero;
    int tileIndex = 0;

    [Header("Goat Prefab")]
    [SerializeField] private GameObject goatPrefab;
    GameObject[] spawnedGoats;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnedPrefabs = new GameObject[gridCount];
        spawnedGoats = new GameObject[gridCount];
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < gridCount; i++)
        {
            if (spawnedPrefabs[i] == null)
            {
                // Spawn a tile at the next spawn position
                spawnedPrefabs[i] = Instantiate(tilePrefabs[0], nextSpawnPos, Quaternion.identity);
                //Update layers
                spawnedPrefabs[i].transform.GetChild(0).GetComponent<TilemapRenderer>().sortingOrder = tileIndex * -3;
                spawnedPrefabs[i].transform.GetChild(1).GetComponent<TilemapRenderer>().sortingOrder = tileIndex * -2;
                spawnedPrefabs[i].transform.GetChild(2).GetComponent<TilemapRenderer>().sortingOrder = tileIndex * -1;

                //Spawn a goat on the tile with a 50% chance
                spawnedGoats[i] = Instantiate(goatPrefab, nextSpawnPos, Quaternion.identity);
                Vector3 spawnPos = new Vector3(tileIndex * tileLength * 0.5f, 0, 0);
                spawnedGoats[i].GetComponent<IsometricRigidbody>().SetPosition(spawnPos);

                nextSpawnPos += ConvertToIsometric(new Vector3(tileLength * 0.5f, 0, 0));
                tileIndex++;

            }
            else 
            {
                float cameraY = Camera.main.transform.position.y;
                if (spawnedPrefabs[i].transform.position.y < cameraY - 8)
                {
                    Destroy(spawnedPrefabs[i]);
                    Destroy(spawnedGoats[i]);
                    spawnedPrefabs[i] = null;
                }

            }

        }

    }

    Vector2 ConvertToIsometric(Vector3 cartesianPos)
    {
        // The isometric X is calculated from the horizontal (X) and depth/vertical (Z)
        float isoX = (cartesianPos.x - cartesianPos.z) * Mathf.Cos(Mathf.Deg2Rad * 30);

        // The isometric Y is calculated from the horizontal (X), depth (Z), and height (Y)
        float isoY = (cartesianPos.x + cartesianPos.z) * Mathf.Sin(Mathf.Deg2Rad * 30) + cartesianPos.y;

        return new Vector2(isoX, isoY);

    }

}
