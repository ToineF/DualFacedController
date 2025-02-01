using UnityEngine;

namespace Cattac.Character.Visuals
{
    public class BodyRenderer : MonoBehaviour
    {
        [field: SerializeField] public Transform[] OriginalPoints { get; set; }
        [field: SerializeField] public GameObject[] Bones { get; set; }

        private void Update()
        {
            for (int i = 0; i < Bones.Length; i++)
            {
                var otherIndex = (i + 1 >= Bones.Length) ? -1 : 1;
                var relative = OriginalPoints[i].position - OriginalPoints[i + otherIndex].position;
                var boneTransform = Bones[i].transform;
                boneTransform.position = OriginalPoints[i].position;
                boneTransform.LookAt(OriginalPoints[i + otherIndex]);
                boneTransform.Rotate(90, 0, 0);
            }
        }
    }
}