using UnityEngine;


public class BodyRenderer : MonoBehaviour
{
    [field: SerializeField] public Rigidbody[] OriginalPoints { get; set; }
    [field: SerializeField] public GameObject[] Bones { get; set; }

    private void Update()
    {

        for (int i = 0; i < Bones.Length; i++)
        {
            Vector3 relative;
            if (i+1 >= Bones.Length) relative = OriginalPoints[i].position - OriginalPoints[i - 1].position;
            else relative = OriginalPoints[i].position - OriginalPoints[i + 1].position;
            float angle = Mathf.Atan2(relative.z, relative.x) * Mathf.Rad2Deg;
            Bones[i].transform.position = OriginalPoints[i].position;
            Bones[i].transform.localEulerAngles = new Vector3(-180,0,angle+90);
        }
    }
}