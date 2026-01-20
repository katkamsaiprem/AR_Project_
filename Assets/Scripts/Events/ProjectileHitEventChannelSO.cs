using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "ProjectileHitEventChannelSO", menuName = "Scriptable Objects/ProjectileHitEventChannelSO")]
public class ProjectileHitEventChannelSO : ScriptableObject
{
    private UnityAction<ProjectileHitInfo> onProjectileHit;

    public void Subscribe(UnityAction<ProjectileHitInfo> callbackListener)
    {
        onProjectileHit += callbackListener;
    }

    public void Unsubcribe(UnityAction<ProjectileHitInfo> callbackListener)
    {
        onProjectileHit -= callbackListener;
    }
    
    public void RaiseEvent(ProjectileHitInfo projectileHitInfo)
    {
       onProjectileHit?.Invoke(projectileHitInfo);
       Debug.Log($"ProjectileHit event raised: {projectileHitInfo.damage} damage to {projectileHitInfo.target?.name}");
    }

}
