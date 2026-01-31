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
            PlayersManager.PlayersAmount = PlayersAmount.ONE;
            Debug.Log("PlayersAmountSwitcher.SetOnePlayer()");
        }

        public void SetTwoPlayers()
        {
            PlayersManager.PlayersAmount = PlayersAmount.TWO;
            Debug.Log("PlayersAmountSwitcher.SetTwoPlayers()");
        }
    }
}