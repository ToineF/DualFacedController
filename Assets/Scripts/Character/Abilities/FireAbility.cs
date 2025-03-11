using UnityEngine;

namespace Cattac.Character.Ability
{
    public class FireAbility : Ability
    {
        [SerializeField] private ParticleSystem _fireVFX;
        [SerializeField] private bool _isFire;
        [SerializeField] private Collider _collider;
        public override void UseAbility(CharacterHead user, bool use)
        {
            _fireVFX.Stop();
            if (use) _fireVFX.Play();
            _collider.gameObject.SetActive(use);
        }
    }
}