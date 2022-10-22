using System.Collections;
using TestGame.Character;
using TestGame.UI;
using UnityEngine;

namespace TestGame.Levels
{
    public class LevelEndChecker : MonoBehaviour
    {
        [SerializeField] private Transform _levelEndPosition;
        [SerializeField] private VictoryPopUp _victoryPopUp;
        private PlayerMovement _playerMovement;
        
        private const float PlayerStoppingTime = 0.6f;
        private LevelPoints _levelPoints;
        
        private bool _isLevelEnd;

        public bool IsLevelEnd
        {
            get => _isLevelEnd;
        }

        private void Awake()
        {
            _playerMovement = FindObjectOfType<PlayerMovement>();
            _playerMovement.OnLevelEnd += CheckEndLevel;
        }

        private bool CheckEndLevel(Vector3 startPosition, Vector3 endPosition)
        {
            if (Mathf.Abs(_playerMovement.transform.position.z - _levelEndPosition.position.z) <= 1)
            {
                StartCoroutine(PlayerStopCo());
                
                return _isLevelEnd = true;
                IEnumerator PlayerStopCo()
                {
                    yield return new WaitForSeconds(PlayerStoppingTime);
                    _victoryPopUp.PopUp();
                    _playerMovement.IsMoving = false;
                }
            }
            return false;
        }
    }
}