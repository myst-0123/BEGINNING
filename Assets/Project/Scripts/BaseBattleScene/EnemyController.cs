using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


namespace BattleScene
{
    public class EnemyController : MonoBehaviour
    {
        private enum State
        {
            Standby,
            Move,
            Track,
            Attack,
        }

        [SerializeField] private GameObject _playerObject;
        [SerializeField] private float _speed;

        private State _state = State.Standby;
        private float _transitionTime = 0.7f;
        private float _moveTime = 0;
        [SerializeField] private Vector3 _direction = Vector3.zero;
        [SerializeField] private bool _a = true;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
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
                case State.Attack:
                    Attack();
                    break;
            }
        }

        void SetMovingDirection()
        {
            _direction = Vector3.zero;

            int rnd = Random.Range(1, 6);

            _transitionTime = Random.Range(1f, 2f);

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

            rnd = Random.Range(1, 6);

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
            float x = transform.position.x;
            float y = transform.position.y;

            _moveTime += Time.deltaTime;

            if (_moveTime > _transitionTime)
            {
                _moveTime = 0;
                _state = State.Track;
            }
            else
            {
                _a = x > 3 && x < 6 && y > -2.5f && y < 2.5f;
                if (x > 3 && x < 6 && y > -2.5f && y < 2.5f)
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
            _state = State.Standby;
        }
    }
}
