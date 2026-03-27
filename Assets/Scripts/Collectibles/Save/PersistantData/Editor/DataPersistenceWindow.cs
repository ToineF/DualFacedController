using Cattac.Collectibles.Save;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataPersistenceWindow : EditorWindow
{
#if UNITY_EDITOR
    [MenuItem("Cattac/Save System")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(DataPersistenceWindow));
    }

    void OnGUI()
    {
        if (GUILayout.Button("Rebake save system"))
        {
            BakeData();
        }
    }
    

    /// <summary>
    /// Editor-only method to bake all 'Index'es of 'DataPersistence's
    /// </summary>
    private void BakeData()
    {
        var dataList = FindObjectsByType<DataPersistence>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        for (int i = 0; i < dataList.Length; i++)
        {
            var data = dataList[i];
            data.Index = i;
            EditorUtility.SetDirty(data);
        }

        Debug.Log($"Persistent data baked in {SceneManager.GetActiveScene().name}");
    }
#endif
}