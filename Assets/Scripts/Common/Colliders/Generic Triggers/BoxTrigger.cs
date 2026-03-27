using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(BoxCollider))]
public class BoxTrigger : GenericTrigger
{
    protected override void DrawGizmos(Color boxColor, Color wireColor)
    {
        if (_collider == null)
            _collider = GetComponent<Collider>();
        var boxCollider = _collider as BoxCollider;
        if (boxCollider == null) return;
        var position = _collider.transform.position +  Vector3.Scale(boxCollider.center, _collider.transform.localScale);
        var size = Vector3.Scale(boxCollider.size, _collider.transform.localScale);
        Gizmos.color = boxColor;
        Gizmos.DrawCube(position, size);
        Gizmos.color = wireColor;
        Gizmos.DrawWireCube(position, size);
    }
}
