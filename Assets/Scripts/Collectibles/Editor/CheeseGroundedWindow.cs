using Cattac.Interactables.Collectibles;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheeseGroundedWindow : EditorWindow
{
    private float _offset = 3;
    private LayerMask _groundLayer = 512;
    
#if UNITY_EDITOR
    [MenuItem("Cattac/Cheese Grounded")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(CheeseGroundedWindow));
    }

    void OnGUI()
    {
        GUILayout.Label("This tool makes all cheeses in the scene automatically grounded", EditorStyles.boldLabel);

        _offset = EditorGUILayout.FloatField("Ground Offset", _offset);
        LayerMask temp = EditorGUILayout.MaskField( InternalEditorUtility.LayerMaskToConcatenatedLayersMask(_groundLayer), InternalEditorUtility.layers);
        _groundLayer = InternalEditorUtility.ConcatenatedLayersMaskToLayerMask(temp);

        if (GUILayout.Button("Ground All Cheeses"))
        {
            GroundAllCheeses();
        }
    }
    

    /// <summary>
    /// Editor-only method to bake all 'Index'es of 'DataPersistence's
    /// </summary>
    private void GroundAllCheeses()
    {
        var cheeses = FindObjectsByType<CheeseCollectible>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        foreach (var cheese in cheeses)
        {
            if (Physics.Raycast(cheese.transform.position + Vector3.up * 50, Vector3.down, out RaycastHit hit, Mathf.Infinity, _groundLayer, QueryTriggerInteraction.Ignore)) // ground layer
            {
                Debug.Log(hit.collider.gameObject.name);
                cheese.transform.position = hit.point + Vector3.up * _offset;
            }
            EditorUtility.SetDirty(cheese);
        }

        Debug.Log($"All cheeses moved to ground in {SceneManager.GetActiveScene().name}");
    }
#endif
}