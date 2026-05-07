using Game;
using UnityEngine;

public abstract class Equipment : MonoBehaviour
{
    protected Transform playerCamTrans;

    protected BulletTrails bulletTrails;

    public AudioClip shootSound;

    protected virtual void Awake()
    {
        // Since this component is attached to the player,
        // search the player hierarchy for these components

        playerCamTrans = Camera.main.transform;

        bulletTrails = GetComponent<BulletTrails>();

        shootSound = GetComponent<PlayerGunController>().currentShootSound;
    }

    public abstract void Use();
}