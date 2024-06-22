using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClotheBehaviour : MonoBehaviour
{
    [SerializeField] EquippmentType type;
    [SerializeField] Animator animator;

    public EquippmentType Type { get => type;}

    private void OnEnable()
    {
        animator = GetComponent<Animator>();
    }
    public void SetAnimationState(Vector2 playerMovementVector)
    {
        animator.SetFloat("horizontalValue", playerMovementVector.x);
        animator.SetFloat("verticalValue", playerMovementVector.y);
    }
}
