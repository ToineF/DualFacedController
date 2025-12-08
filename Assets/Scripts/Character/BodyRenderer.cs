using UnityEngine;

namespace Cattac.Character.Visuals
{
    public class BodyRenderer : MonoBehaviour
    {
        [field: SerializeField] public Transform[] OriginalPoints { get; set; }
        [field: SerializeField] public GameObject[] Bones { get; set; }
        [SerializeField] private Vector3 _positionOffset;
        [SerializeField] private Vector3 _rotationOffset;

        private void Update()
        {
            for (int i = 0; i < Bones.Length; i++)
            {
                var index = i;
                if (i + 1 >= Bones.Length) index--;
                var boneTransform = Bones[i].transform;
                boneTransform.position = OriginalPoints[index].position + _positionOffset;
                boneTransform.LookAt(OriginalPoints[index + 1].position + _positionOffset);
                boneTransform.Rotate(_rotationOffset);
            }
        }
    }
}