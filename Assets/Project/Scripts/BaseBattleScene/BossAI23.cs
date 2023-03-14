using System;
using System.Collections;
using UnityEngine;


namespace BattleScene
{
    public class BossAI23 : MonoBehaviour
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

        void Start()
        {
            _playerObject = GameObject.Find("Player");
            _controller = transform.parent.GetComponent<EnemiesController>();
            _hpController = transform.GetComponent<BossHpController>();
            StartCoroutine(Attack());
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
                if (transform.position.y <= 4.0f)
                {
                    return true;
                }
            }
            if (_direction.y < 0)
            {
                if (transform.position.y >= -4.0f)
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
                    switch(UnityEngine.Random.Range(1, 3))
                    {
                        case 1:
                            yield return StartCoroutine(AttackRadiation());
                            break;
                        case 2:
                            yield return StartCoroutine(AttackRapidFire());
                            break;
                    }
                }

                yield return new WaitForSeconds(1.0f);
            }

            
        }

        IEnumerator AttackRadiation()
        {
            for (int i = 105; i <= 255; i += 15)
            {
                Quaternion rotation = Quaternion.identity;
                rotation.eulerAngles = new Vector3(0, 0, i);
                GameObject bullet = Instantiate(_bulletPrefab, transform.position, rotation);
            }

            yield return new WaitForSeconds(0.7f);

            if (_hpController.HpPercent() < 50)
            {
                for (int j = 0; j < 2; j++)
                {
                    for (float i = 97.5f; i <= 262.5; i += 15)
                    {
                        Quaternion rotation = Quaternion.identity;
                        rotation.eulerAngles = new Vector3(0, 0, i);
                        GameObject bullet = Instantiate(_bulletPrefab, transform.position, rotation);
                    }
                    yield return new WaitForSeconds(0.4f);
                }
            }

            yield return null;
        }

        IEnumerator AttackRapidFire()
        {
            if (_hpController.HpPercent() < 50)
            {
                for (int i = 0; i < 20; i++)
                {
                    for (float j = 120; j <= 240; j += 30)
                    {
                        Quaternion rotation = Quaternion.identity;
                        rotation.eulerAngles = new Vector3(0, 0, j);
                        GameObject bullet = Instantiate(_bulletPrefab, transform.position, rotation);
                    }
                    yield return new WaitForSeconds(0.1f);
                }
            }
            else
            {
                for (int i = 0; i < 15; i++)
                {
                    for (float j = 150; j <= 210; j += 30)
                    {
                        Quaternion rotation = Quaternion.identity;
                        rotation.eulerAngles = new Vector3(0, 0, j);
                        GameObject bullet = Instantiate(_bulletPrefab, transform.position, rotation);
                    }
                    yield return new WaitForSeconds(0.1f);
                }
            }
            yield return null;
        }
    }
}
