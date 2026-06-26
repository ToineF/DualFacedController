using AntoineFoucault.Utilities;
using UnityEngine;

namespace Cattac.Interactables
{
    public class WindZone : BoxTrigger
    {
        [Header("Wind Zone")] [SerializeField] float _windStrength = 10f;

        protected void OnTriggerStay(Collider other)
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(transform.right * _windStrength, ForceMode.Force);
            }
        }

        protected override void DrawGizmos(Color boxColor, Color wireColor)
        {
            base.DrawGizmos(boxColor, wireColor);

            GizmoExtensions.DrawArrow(transform.position, transform.right);
        }
    }
}