using Cattac.Character;
using UnityEngine;

namespace Cattac.Interactables
{
    public class SeparatorFakeParent : Separator
    {
        [SerializeField] private FakeParent _fakeParent;

        protected override void OnGrabInternal(CharacterHead characterHead)
        {
            _fakeParent.OnGrab(characterHead);
        }

        protected override void OnUngrabInternal(CharacterHead characterHead)
        {
            _fakeParent.OnUngrab(characterHead);
        }
    }
}