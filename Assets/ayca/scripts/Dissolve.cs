using System.Collections;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    public float dissolveDuration = 2f;
    private float dissolveStrength = 0f;
    private Material[] dissolveMaterials;

    public GameObject[] replacementObjects;

    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        dissolveMaterials = renderer.materials;

        foreach (var mat in dissolveMaterials)
        {
            mat.SetFloat("_DissolveStrength", 0f);
        }
    }

    public void StartDissolver()
    {
        StartCoroutine(Dissolver());
    }

    private IEnumerator Dissolver()
    {
        float elapsedTime = 0f;

        while (elapsedTime < dissolveDuration)
        {
            elapsedTime += Time.deltaTime;

            dissolveStrength = Mathf.Lerp(0, 1, elapsedTime / dissolveDuration);

            foreach (var mat in dissolveMaterials)
            {
                mat.SetFloat("_DissolveStrength", dissolveStrength);
            }

            yield return null;
        }

        foreach (var obj in replacementObjects)
        {
            obj.SetActive(true);
        }

        Destroy(gameObject);
    }
}
