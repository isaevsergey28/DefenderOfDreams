using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    private Player _player;
    [SerializeField] private Transform _mainCamera;
    [SerializeField] private AudioSource _shotAudio;

    [SerializeField] private GameObject _explosionPrefab;
    private ParticleSystem _explosionAnim;
    [SerializeField] private Transform _spawnExplosionPos;

    [SerializeField] private GameObject _bleedingPrefab;
    private ParticleSystem _bleedingAnim;

    [SerializeField] private GameObject _damageInfoTextPrefab;
    

    private bool _isGunReloaded = true;
    public float _timeToReload { get; set; } = 0.5f;

    private void Start()
    {
        _player = transform.parent.GetComponent<Player>();
        _explosionAnim = _explosionPrefab.GetComponent<ParticleSystem>();
        _bleedingAnim = _bleedingPrefab.GetComponent<ParticleSystem>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _isGunReloaded)
        {
            TakeShot();
        }
    }

    private void TakeShot()
    {
        _isGunReloaded = false;
        _shotAudio.Play();
        GameObject explosion;

        Vector3 direction = _mainCamera.TransformDirection(Vector3.forward);
        explosion = Instantiate(_explosionPrefab, _spawnExplosionPos.position, Quaternion.identity, _spawnExplosionPos);
        DestroyEsplosion(explosion);
        if (Physics.Raycast(_mainCamera.position, direction, out RaycastHit hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject.TryGetComponent(out EnemyBehaviour enemy))
            {
                enemy.GiveDamage(_player.damage);
                MakeBleeding(enemy);
                ShowDamageInfo(enemy);
            }
        }
        StartCoroutine(ReloadGun());
    }

    private void MakeBleeding(EnemyBehaviour enemy)
    {
        GameObject bleeding;
        Vector3 damagePosition = new Vector3(enemy.gameObject.transform.position.x, enemy.gameObject.transform.position.y + 1f, enemy.gameObject.transform.position.z);
        bleeding = Instantiate(_bleedingPrefab, damagePosition, Quaternion.identity, enemy.transform);
        DestroyBleeding(bleeding);
    }

    private void ShowDamageInfo(EnemyBehaviour enemy)
    {
        GameObject damageInfo = Instantiate(_damageInfoTextPrefab, enemy.transform.GetChild(0).transform.position, Quaternion.identity, enemy.transform.GetChild(0).transform);
        damageInfo.transform.LookAt(_player.gameObject.transform);
        damageInfo.transform.Rotate(Vector3.up, 180f);
        damageInfo.GetComponent<Text>().text = "-" + _player.damage.ToString();
        DestroyDamageInfo(damageInfo);
    }


    private void DestroyDamageInfo(GameObject damageInfo)
    {
        Destroy(damageInfo, 1.5f);
    }
    private void DestroyEsplosion(GameObject explosion)
    {
        Destroy(explosion, _explosionAnim.main.duration);
    }
    private void DestroyBleeding(GameObject bleeding)
    {
        Destroy(bleeding, _bleedingAnim.main.duration);
    }
    private IEnumerator ReloadGun()
    {
        yield return new WaitForSeconds(_timeToReload);
        _isGunReloaded = true;
    }
}
