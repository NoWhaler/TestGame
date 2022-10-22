using TestGame.Character;
using TestGame.Levels;
using UnityEngine;
using UnityEngine.UI;


namespace TestGame.UI
{
    public class LevelPoints : MonoBehaviour
    {
        private Text _scorePointsText;
        
        private float _levelScorePoints;
        private float _scoreMultiplier = 1f;
        private const float HighSpeedMultiplier = 4f;
        
        private LevelEndChecker _levelEndChecker;
        private PlayerMovement _playerMovement;

        public float ScoreMultiplier 
        { 
            get => _scoreMultiplier;
            set => _scoreMultiplier = value;
        }

        public float LevelScorePoints
        {
            get => _levelScorePoints;
            set => _levelScorePoints = value;
        }

        private void Start()
        {
            _playerMovement = FindObjectOfType<PlayerMovement>();
            _levelEndChecker = FindObjectOfType<LevelEndChecker>();
            _scorePointsText = GetComponent<Text>();
        }

        private void Update()
        {
            if (!_levelEndChecker.IsLevelEnd)
            {
                if (_playerMovement.IsSpeedUp)
                {
                    var speedUpMultiplier = ScoreMultiplier * HighSpeedMultiplier;
                    _levelScorePoints += speedUpMultiplier;
                }
                else
                {
                    _levelScorePoints += ScoreMultiplier;
                }
            }
            Debug.Log(_playerMovement.IsSpeedUp);
            Debug.Log(ScoreMultiplier * HighSpeedMultiplier);
            _scorePointsText.text = _levelScorePoints.ToString();
        }
    }
}