using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private LayerMask selectableLayer;
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private float highlightDistance = 1f;
    [SerializeField] private Transform characterTransform;
    [SerializeField] private float highlightDuration = 1f;

    private Dictionary<int, Coroutine> activeHighlights = new Dictionary<int, Coroutine>();

    private void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(characterTransform.position, highlightDistance, selectableLayer);
        foreach (var hitCollider in hitColliders)
        {
            var selection = hitCollider.transform;
            var selectionRenderer = selection.GetComponent<Renderer>();
            if (selectionRenderer != null)
            {
                int instanceID = selection.GetInstanceID();


                if (!activeHighlights.ContainsKey(instanceID))
                {
                    var highlightCoroutine = StartCoroutine(HighlightObject(selectionRenderer, instanceID));
                    activeHighlights[instanceID] = highlightCoroutine;
                }
            }
        }
    }

    private IEnumerator HighlightObject(Renderer selectionRenderer, int instanceID)
    {
        var originalMaterials = selectionRenderer.materials;
        var newMaterials = new Material[selectionRenderer.materials.Length];
        for (int i = 0; i < newMaterials.Length; i++)
        {
            newMaterials[i] = highlightMaterial;
        }
        selectionRenderer.materials = newMaterials;

        yield return new WaitForSeconds(highlightDuration);

        selectionRenderer.materials = originalMaterials;
        activeHighlights.Remove(instanceID);
    }
}