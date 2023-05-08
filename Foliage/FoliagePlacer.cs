using UnityEngine;

public class FoliagePlacer : MonoBehaviour
{
    public GameObject[] foliageMeshes;
    public float foliageDensity = 0.2f;
    public float minSize = 0.2f;
    public float maxSize = 0.4f;

    private void Start()
    {
        if (foliageDensity > 4)
        {
            foliageDensity = 4;
        }

        GenerateFoliage();
    }

    [ContextMenu("Generate Foliage")]
    public void GenerateFoliage()
    {
        Bounds bounds = GetComponent<Renderer>().bounds;
        Vector3 center = bounds.center;
        float size = Mathf.Max(bounds.size.x, bounds.size.z);
        int numInstances = Mathf.FloorToInt(foliageDensity * size * size);

        GameObject grassObject = new GameObject("Foliage");

        grassObject.transform.SetParent(transform, true);

        if (foliageMeshes == null || foliageMeshes.Length == 0)
        {
            return;
        }

        for (int i = 0; i < numInstances; i++)
        {
            float x = Random.Range(-size / 2f, size / 2f);
            float z = Random.Range(-size / 2f, size / 2f);

            // Adjust x and z based on the size of the plane
            if (x < -bounds.size.x / 2f)
            {
                x = -bounds.size.x / 2f;
            }
            else if (x > bounds.size.x / 2f)
            {
                x = bounds.size.x / 2f;
            }

            if (z < -bounds.size.z / 2f)
            {
                z = -bounds.size.z / 2f;
            }
            else if (z > bounds.size.z / 2f)
            {
                z = bounds.size.z / 2f;
            }

            Vector3 position = center + new Vector3(x, 0f, z);

            GameObject foliage = Instantiate(foliageMeshes[Random.Range(0, foliageMeshes.Length)], position, Quaternion.Euler(Random.Range(-10f, 10f), Random.Range(0f, 360f), 0f));
            float scale = Random.Range(minSize, maxSize);
            foliage.transform.localScale = new Vector3(scale, scale, scale);
            foliage.transform.parent = grassObject.transform;
        }
    }


}
