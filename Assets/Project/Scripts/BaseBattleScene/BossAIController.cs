using System;
using UnityEngine;


namespace BattleScene
{
    public class BossAIController : MonoBehaviour
    {
        private enum State
        {
            Standby,
            Move,
            Track,
            Attack,
        }

        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private float _speed;

        private GameObject _playerObject;
        private State _state = State.Standby;
        private float _transitionTime = 1.5f;
        private float _moveTime = 0;
        private float _fireTime = 0;
        private Vector3 _direction = Vector3.zero;
        private EnemiesController _controller;

        void Start()
        {
            _playerObject = GameObject.Find("Player");
            _controller = transform.parent.GetComponent<EnemiesController>();
        }

        // Update is called once per frame
        void Update()
        {
            if (_controller.permittion)
            {
                if (gameObject.GetComponent<BossHpController>().hp == 0)
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
                    case State.Attack:
                        Attack();
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

            rnd = UnityEngine.Random.Range(1, 6);

            if (rnd == 1)
            {
                _direction += new Vector3(_speed, 0, 0);
            }
            else if (rnd == 2)
            {
                _direction += new Vector3(-1 * _speed, 0, 0);
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
                _state = State.Attack;
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

        void Attack()
        {
            AttackRadiation();
            AttackRapidFire();

            _moveTime += Time.deltaTime;

            if (_moveTime > _transitionTime)
            {
                _moveTime = 0;
                _state = State.Standby;
            }
        }

        void AttackRadiation()
        {
            if (_moveTime == 0)
            {
            Debug.Log(DateTime.Now.Millisecond);
                for (int i = 105; i <= 255; i += 15)
                {
                    Quaternion rotation = Quaternion.identity;
                    rotation.eulerAngles = new Vector3(0, 0, i);
                    GameObject bullet = Instantiate(_bulletPrefab, transform.position, rotation);
                }
            Debug.Log(DateTime.Now.Millisecond);
            }
        }

        void AttackRapidFire()
        {
            _fireTime += Time.deltaTime;

            float yDistance = transform.position.y - _playerObject.transform.position.y;
            Vector3 direction = Vector3.zero;

            if (yDistance < 0)
            {
                direction = new Vector3(0, _speed, 0);
                transform.position += direction * Time.deltaTime;
            }
            else
            {
                direction = new Vector3(0, -1 * _speed, 0);
                transform.position += direction * Time.deltaTime;
            }

            if (_fireTime > 0.15f)
            {
                _fireTime = 0;
                Quaternion rotation = Quaternion.identity;
                rotation.eulerAngles = new Vector3(0, 0, 180);
                GameObject bullet = Instantiate(_bulletPrefab, transform.position, rotation);
            }
        }
    }
}
