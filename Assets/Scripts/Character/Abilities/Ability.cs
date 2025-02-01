using UnityEngine;

namespace Cattac.Character.Ability
{
    public abstract class Ability : MonoBehaviour
    {
        public abstract void UseAbility(CharacterHead user);
    }
}