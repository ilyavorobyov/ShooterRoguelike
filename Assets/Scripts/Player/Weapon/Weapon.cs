using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private Mesh _weaponMesh;
    [SerializeField] private Material _weaponMaterial;
    [SerializeField] private PlayerBullet _bullet;
    [SerializeField] private int _damage;
    [SerializeField] private string _label;
    [SerializeField] private float _reloadDuration;

    public Mesh Mesh => _weaponMesh;
    public Material WeaponMaterial => _weaponMaterial;
    public PlayerBullet Bullet => _bullet;
    public int Damage => _damage;
    public string Label => _label;
    public float ReloadDuration => _reloadDuration;
}