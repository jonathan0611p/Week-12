
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    [Header("Block Materials")]
    public Material grassMaterial;
    public Material dirtMaterial;
    public Material stoneMaterial;

    [Header("World Settings")]
    public int worldSize = 20;
    public int hillHeight = 5;
    public float terrainScale = 0.12f;

    void Start()
    {
        GenerateWorld();
    }

    void GenerateWorld()
    {
        // Generate each column of the world
        for (int x = 0; x < worldSize; x++)
        {
            for (int z = 0; z < worldSize; z++)
            {
                // Calculate the terrain height
                float noise = Mathf.PerlinNoise(
                    x * terrainScale,
                    z * terrainScale
                );

                int height = Mathf.RoundToInt(
                    noise * hillHeight
                ) + 2;

                // Create blocks from bottom to top
                for (int y = 0; y < height; y++)
                {
                    GameObject block =
                        GameObject.CreatePrimitive(
                            PrimitiveType.Cube
                        );

                    // Position the block
                    block.transform.position =
                        new Vector3(x, y, z);

                    // Choose the correct material
                    Renderer blockRenderer =
                        block.GetComponent<Renderer>();

                    if (y == height - 1)
                    {
                        blockRenderer.sharedMaterial =
                            grassMaterial;
                    }
                    else if (y >= height - 3)
                    {
                        blockRenderer.sharedMaterial =
                            dirtMaterial;
                    }
                    else
                    {
                        blockRenderer.sharedMaterial =
                            stoneMaterial;
                    }

                    // Organize blocks under the generator
                    block.transform.SetParent(transform);
                }
            }
        }
    }
}
