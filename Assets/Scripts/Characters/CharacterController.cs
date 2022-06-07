using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScaredBug.Game;

namespace ScaredBug.Character
{
    public class CharacterController
    {
        private const float dangerDistance = 0.8f;
        private const float safeDistance = 2f;

        private CharacterView _character;
        private IGameZone _gameZone;
        private IDangerZone _dangerZone;

        public CharacterController(CharacterView character, IGameZone gameZone, IDangerZone dangerZone)
        {
            _character = character;
            _gameZone = gameZone;
            _dangerZone = dangerZone;
        }

        private Vector3 GetSafePoint()
        {
            Vector3 dir = (_character.transform.position - _dangerZone.position).normalized;
            return _dangerZone.position + dir * (_dangerZone.radius + safeDistance);
        }

        public IEnumerator Play()
        {
            bool complete = false;
            while (!complete)
            {
                _character.SetDestination(_gameZone.end, () => complete = true);
                while (!complete && Vector3.Distance(_character.transform.position, _dangerZone.position) >= _dangerZone.radius + dangerDistance)
                    yield return null;
                if (!complete)
                {
                    bool safe = false;
                    Vector3 safePoint = _character.transform.position;
                    while (!safe)
                    {
                        float x = Vector3.Dot(_character.transform.position - _dangerZone.position, safePoint - _character.transform.position);
                        float distance = Vector3.Distance(_character.transform.position, _dangerZone.position);
                        if (x <= 0 && distance < _dangerZone.radius + dangerDistance)
                        {
                            safePoint = GetSafePoint();
                            _character.SetDestination(safePoint, () => safe = true);
                        }
                        else if (distance >= _dangerZone.radius + safeDistance)
                            safe = true;
                        yield return null;
                    }
                }
            }
        }
    }
}
