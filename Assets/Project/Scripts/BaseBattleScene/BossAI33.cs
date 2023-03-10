using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace BattleScene
{

    public class BossAI33 : MonoBehaviour
    {
        private enum State
        {
            Standby,
            Move,
            Track,
        }

        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private GameObject _winderPrefab;
        [SerializeField] private float _speed;

        private GameObject _playerObject;
        private State _state = State.Standby;
        private float _transitionTime = 1.5f;
        private float _moveTime = 0;
        private Vector3 _direction = Vector3.zero;
        private EnemiesController _controller;
        private BossHpController _hpController;
        private bool isWindering = false;

        void Start()
        {
            _playerObject = GameObject.Find("Player");
            _controller = transform.parent.GetComponent<EnemiesController>();
            _hpController = transform.GetComponent<BossHpController>();
            StartCoroutine(Attack());
            StartCoroutine(Winder());
        }

        // Update is called once per frame
        void Update()
        {
            if (_hpController.hp == 0)
            {
                Destroy(gameObject);
            }
            if (_controller.permittion && !isWindering)
            {
                switch (_state)
                {
                    case State.Standby:
                        SetMovingDirection();
                        break;
                    case State.Move:
                        Move();
                        break;
                    case State.Track:
                        TrackMove();
                        break;
                }
            }
        }

        void SetMovingDirection()
        {
            _direction = Vector3.zero;

            int rnd = UnityEngine.Random.Range(1, 6);

            _transitionTime = UnityEngine.Random.Range(1f, 2f);

            if (rnd >= 4)
            {
                _direction += new Vector3(0, _speed, 0);
            }
            else if (rnd >= 2)
            {
                _direction += new Vector3(0, -1 * _speed, 0);
            }
            else
            {
                ;
            }

            _state = State.Move;

        }

        void Move()
        {
            _moveTime += Time.deltaTime;

            if (_moveTime > _transitionTime)
            {
                _moveTime = 0;
                _state = State.Track;
            }
            else
            {
                transform.position += _direction * Time.deltaTime;
            }
        }

        void TrackMove()
        {
            float yDistance = transform.position.y - _playerObject.transform.position.y;
            Vector3 direction = Vector3.zero;

            if (Mathf.Abs(yDistance) < 0.7)
            {
                _state = State.Standby;
            }
            else if (yDistance < 0)
            {
                direction = new Vector3(0, _speed, 0);
                transform.position += direction * Time.deltaTime;
            }
            else
            {
                direction = new Vector3(0, -1 * _speed, 0);
                transform.position += direction * Time.deltaTime;
            }
        }

        float GetDirect()
        {
            float dx = _playerObject.transform.position.x - transform.position.x;
            float dy = _playerObject.transform.position.y - transform.position.y;
            return Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
        }

        IEnumerator Attack()
        {

            while (true)
            {
                if (_controller.permittion)
                {
                    switch (UnityEngine.Random.Range(1, 3))
                    {
                        case 1:
                            yield return StartCoroutine(AttackRadiation());
                            break;
                        case 2:
                            yield return StartCoroutine(AttackRapidFire());
                            break;
                    }
                }

                yield return new WaitForSeconds(0.7f);
            }


        }

        IEnumerator AttackRadiation()
        {
            for (int j = 0; j < 3; j++)
            {
                for (int i = 0; i <= 360; i += 20)
                {
                    Quaternion rotation = Quaternion.identity;
                    rotation.eulerAngles = new Vector3(0, 0, i + j * 10);
                    GameObject bullet = Instantiate(_bulletPrefab, new Vector3(2, 0, 0), rotation);
                }
                yield return new WaitForSeconds(0.5f);
            }

            yield return null;
        }

        IEnumerator AttackRapidFire()
        {
            yield return null;
        }

        IEnumerator Winder()
        {
            while (true)
            {
                if (_hpController.HpPercent() <= 50)
                {
                    float dir = GetDirect();
                    float randX = UnityEngine.Random.Range(-3.0f, -2.0f);
                    float randY = UnityEngine.Random.Range(-8.0f, -6.0f);
                    GameObject winderObject = Instantiate(_winderPrefab, new Vector3(transform.position.x + randX, transform.position.y + randY, 0), transform.rotation);
                    for (int j = 0; j < 60; j++)
                    {
                        float dir2 = Mathf.Sin(2 * j * Mathf.Deg2Rad) * 15.0f;
                        for (int i = -2; i < 2; i++)
                        {
                            Quaternion rotation = Quaternion.identity;
                            rotation.eulerAngles = new Vector3(0, 0, dir + dir2 + i * 45);
                            GameObject bullet = Instantiate(_bulletPrefab, transform.position, rotation);
                            bullet.GetComponent<EnemyBulletController>().SetBulletSpeed(8.0f);
                            bullet.GetComponent<EnemyBulletController>().SetBulletDamage(1);
                        }
                        yield return new WaitForSeconds(0.07f);
                    }
                    Destroy(winderObject.gameObject);
                }
                yield return new WaitForSeconds(8.0f);
            }
        }
    }
}
