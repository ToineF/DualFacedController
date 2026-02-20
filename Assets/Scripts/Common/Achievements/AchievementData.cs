using UnityEngine;

[CreateAssetMenu(menuName = "Achievement")]
public class AchievementData : ScriptableObject
{
    [field:SerializeField] public Sprite Image { get; private set; }
    [field:SerializeField] public string Title { get; private set; }
    [field:SerializeField] public string Description { get; private set; }
}
