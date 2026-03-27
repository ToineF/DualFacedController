using Cattac.Character;
using Cattac.Interactables.Death;
using UnityEngine;

public class RespawnZone : BoxTriggerUnityEventPlayer
{
    private void Start()
    {
        OnTriggerEnter.AddListener(Respawn);
    }
    
    private void OnDestroy()
    {
        OnTriggerEnter.RemoveListener(Respawn);
    }

    private void Respawn(CharacterHead characterHead)
    {
        Debug.Log("Respawn");
        Vector3? position = CheckpointsManager.CurrentCheckpoint?.transform.position;
        if (position.HasValue) characterHead.CharacterManager.TeleportPlayer(position.Value);
        else Debug.LogError("Respawn position not found");
    }


    // private void EnterState(CharacterManager manager)
    // {
    //     Debug.Log("DEATH");
    //     manager.MovementManager.ResetAllExternalForces();
    //     manager.MovementManager.OnDeath?.Invoke(manager);
    //
    //     manager.Transition.SetTransition(() =>
    //     {
    //         manager.Transition.PlayFadeIn();
    //         Debug.Log(manager.CharacterCollider.Rigidbody.gameObject.name + ": " + manager.CharacterCollider.Rigidbody.gameObject.transform.position);
    //
    //         manager.CharacterCollider.Rigidbody.gameObject.SetActive(false);
    //         manager.CheckpointManager.SetToCheckpointPosition(manager.CharacterCollider.Rigidbody.gameObject);
    //         manager.CharacterCollider.Rigidbody.gameObject.SetActive(true);
    //         Debug.Log("AAA " + manager.CharacterCollider.Rigidbody.gameObject.name + ": " + manager.CharacterCollider.Rigidbody.gameObject.transform.position);
    //         manager.States.SwitchState(manager.States.FallingState);
    //         //manager.Transition.PlayFadeIn();
    //
    //
    //         Debug.Log("AAA " + manager.CharacterCollider.Rigidbody.gameObject.name + ": " + manager.CharacterCollider.Rigidbody.gameObject.transform.position);
    //     });
    //     
    //     // Feedbacks
    //     manager.Feedbacks.PlayFeedback(manager.Data.FeedbacksData.DeathFeedback);
    // }
}