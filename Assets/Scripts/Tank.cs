using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class Tank : MonoBehaviour
{
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private Bullet _bulletTemplate;
    [SerializeField] private float _delayBetweemShoot;
    [SerializeField] private float _recoilDistance;
    [SerializeField] private TMP_Text _hpText;
    [SerializeField] private float _hp;
    private float _timeAfterShoot;

    private void Start()
    {
        _hpText.text = _hp.ToString();
    }

    private void Update()
    {
        _timeAfterShoot += Time.deltaTime;

        if(Input.GetMouseButton(0))
        {
            if(_timeAfterShoot > _delayBetweemShoot)
            {
                Shoot();
                transform.DOMoveZ(transform.position.z - _recoilDistance, _delayBetweemShoot / 2).SetLoops(2, LoopType.Yoyo);
                _timeAfterShoot = 0;
            }
        }
    }

    private void Shoot()
    {
        Bullet bullet = Instantiate(_bulletTemplate, _shootPoint.position, Quaternion.identity);
        bullet.BulletBounce += OnBulletBounce;
    }

    private void OnBulletBounce(Bullet bullet)
    {
        bullet.BulletBounce -= OnBulletBounce;
        _hp--;
        if (_hp <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            return;
        }

        _hpText.text = _hp.ToString();
    }
}
