using UnityEngine;

namespace Cattac.Character.Multiplayer
{
    /// <summary>
    /// Switches between the PlayersAmount modes
    /// </summary>
    public class PlayersAmountSwitcher : MonoBehaviour
    {
        public void SetOnePlayer()
        {
            PlayersManager.ChangePlayerAmount(PlayersAmount.ONE);
            Debug.Log("PlayersAmountSwitcher.SetOnePlayer()");
        }

        public void SetTwoPlayers()
        {
            PlayersManager.ChangePlayerAmount(PlayersAmount.TWO);
            Debug.Log("PlayersAmountSwitcher.SetTwoPlayers()");
        }
    }
}