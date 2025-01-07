using UnityEngine;


public class NPC_Icons : MonoBehaviour
{
    [SerializeField] private NPC_Seeker _npc_seeker;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _seekingSprite;
    [SerializeField] private Color _seekingColor;
    [SerializeField] private Sprite _idleSprite;
    [SerializeField] private Color _idleColor;
    [SerializeField, Range(0,1)] private float _colorLerp;

    private void Update()
    {
        _spriteRenderer.sprite = _npc_seeker.Target ? _seekingSprite : _idleSprite;
        _spriteRenderer.color = Color.Lerp(_spriteRenderer.color, _npc_seeker.Target ? _seekingColor : _idleColor, _colorLerp);
    }
}
