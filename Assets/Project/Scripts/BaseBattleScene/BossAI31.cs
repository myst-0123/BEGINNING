using System;
using System.Collections;
using TMPro;
using UnityEngine;


namespace BattleScene
{
    public class BossAI31 : MonoBehaviour
    {
        private enum State
        {
            Standby,
            Move,
            Track,
        }

        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private float _speed;

        private GameObject _playerObject;
        private State _state = State.Standby;
        private float _transitionTime = 1.5f;
        private float _moveTime = 0;
        private Vector3 _direction = Vector3.zero;
        private EnemiesController _controller;
        private BossHpController _hpController;
        private bool isWarping = false;

        void Start()
        {
            _playerObject = GameObject.Find("Player");
            _controller = transform.parent.GetComponent<EnemiesController>();
            _hpController = transform.GetComponent<BossHpController>();
            _hpController.SetName("3-1　主の使い「ドミニオン」");
            StartCoroutine(Attack());
            StartCoroutine(Warp());
        }

        // Update is called once per frame
        void Update()
        {
            if (_controller.permittion)
            {
                if (_hpController.hp == 0)
                {
                    Destroy(gameObject);
                }
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
                if (CanMove())
                    transform.position += _direction * Time.deltaTime;
                else
                    _state = State.Track;
            }
        }

        bool CanMove()
        {
            if (_direction.y > 0)
            {
                if (transform.position.y <= 2.3f)
                {
                    return true;
                }
            }
            if (_direction.y < 0)
            {
                if (transform.position.y >= -2.0f)
                {
                    return true;
                }
            }

            return false;
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

        IEnumerator Attack()
        {

            while (true)
            {
                if (_controller.permittion)
                {
                    if (!isWarping)
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
                }

                yield return new WaitForSeconds(0.8f);
            }


        }

        IEnumerator AttackRadiation()
        {
            for (int j = 0; j < 3; j++)
            {
                for (int i = 100; i <= 250; i += 15)
                {
                    Quaternion rotation = Quaternion.identity;
                    rotation.eulerAngles = new Vector3(0, 0, i);
                    GameObject bullet = Instantiate(_bulletPrefab, transform.position, rotation);
                    yield return new WaitForSeconds(0.05f);
                }
                yield return new WaitForSeconds(0.3f);
            }

            if (_hpController.HpPercent() < 50)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int i = 115; i <= 235; i += 12)
                    {
                        Quaternion rotation = Quaternion.identity;
                        rotation.eulerAngles = new Vector3(0, 0, i);
                        GameObject bullet = Instantiate(_bulletPrefab, transform.position, rotation);
                        yield return new WaitForSeconds(0.05f);
                    }
                }
                yield return new WaitForSeconds(0.3f);
            }

            yield return null;
        }

        IEnumerator AttackRapidFire()
        {
            if (_hpController.HpPercent() < 50)
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        for (float j = 120; j <= 240; j += 30)
                        {
                            Quaternion rotation = Quaternion.identity;
                            rotation.eulerAngles = new Vector3(0, 0, j+(i-2)*12);
                            GameObject bullet = Instantiate(_bulletPrefab, transform.position, rotation);
                        }
                        yield return new WaitForSeconds(0.07f);
                    }
                    yield return new WaitForSeconds(0.2f);
                }
            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        for (float j = 120; j <= 240; j += 30)
                        {
                            Quaternion rotation = Quaternion.identity;
                            rotation.eulerAngles = new Vector3(0, 0, j + (i - 1) * 15);
                            GameObject bullet = Instantiate(_bulletPrefab, transform.position, rotation);
                        }
                        yield return new WaitForSeconds(0.07f);
                    }
                    yield return new WaitForSeconds(0.2f);
                }
            }
            yield return null;
        }

        IEnumerator Warp()
        {
            while (true)
            {
                float nextWarpTime = UnityEngine.Random.Range(7.0f, 10.0f);
                if (_controller.permittion)
                {
                    float newPos = UnityEngine.Random.Range(-2.4f, 2.4f);

                    isWarping = true;

                    for (float i = 0.6f; i >= 0; i -= 0.03f)
                    {
                        transform.localScale = new Vector3(i, i, 1);

                        yield return new WaitForSeconds(0.01f);
                    }

                    transform.position = new Vector3(5, newPos, 0);

                    yield return new WaitForSeconds(0.5f);

                    for (float i = 0f; i <= 0.6f; i += 0.03f)
                    {
                        transform.localScale = new Vector3(i, i, 1);

                        yield return new WaitForSeconds(0.01f);
                    }

                    isWarping = false;
                }

                yield return new WaitForSeconds(nextWarpTime);
            }
        }
    }
}
