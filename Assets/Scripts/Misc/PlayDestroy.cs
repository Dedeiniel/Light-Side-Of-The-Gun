using UnityEngine;

public class PlayDestroy : MonoBehaviour
{
    void Start()
    {
        Animator animator = GetComponent<Animator>();

        float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;
        SoundManager.PlaySound(SoundType.EnemyExplode);

        Destroy(gameObject, animationLength);
    }
}