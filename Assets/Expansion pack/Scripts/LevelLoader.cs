using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game;

namespace ExpansionPack
{
    public class LevelLoader : MonoBehaviour
    {   
        public static LevelLoader Instance;
        private const string CompletedLevels = "CompletedLevels";
        [SerializeField] private List<Level> _levels;
        [SerializeField] private Button _restart;
        [SerializeField] private Button _next;
        [SerializeField] private WinLoseUI _winLoseUI;
        
        public event Action OnLevelLoad;
        
        private int _currentLevelIndex;
        private Level _currentLevel;

        private void Awake()
        {
            //PlayerPrefs.SetInt(CompletedLevels, 0);
            Instance = this;

            _restart.onClick.AddListener(() => StartCoroutine(nameof(Restart), 0.5f));
            _next.onClick.AddListener(() => StartCoroutine(nameof(Next), 0.5f));

            _currentLevelIndex = PlayerPrefs.GetInt(CompletedLevels, 0);
            LoadLevel(_currentLevelIndex);
        }
        private void LoadLevel(int index)
        {   
            OnLevelLoad?.Invoke();
            _currentLevel = Instantiate(_levels[index]);
            SubscribeToLevelEvents(_currentLevel);
        }

        private void LoadNextLevel()
        {
            DestroyCurrentLevel();

            _currentLevelIndex = _currentLevelIndex + 1 >= _levels.Count ? 0 : _currentLevelIndex + 1;
            SaveLevel();
            LoadLevel(_currentLevelIndex);
        }
        private void DestroyCurrentLevel()
        {
            if (_currentLevel != null)
            {
                UnsubscribeFromLevelEvents(_currentLevel);
                Destroy(_currentLevel.gameObject);
            }
        }
        private void SaveLevel() =>
            PlayerPrefs.SetInt(CompletedLevels, _currentLevelIndex);

        private void SubscribeToLevelEvents(Level level)
        {
            level.OnLose += OnLoseHandler;
            level.OnWin += OnWinHandler;
        }
        private void UnsubscribeFromLevelEvents(Level level)
        {
            level.OnLose -= OnLoseHandler;
            level.OnWin -= OnWinHandler;
        }

        private void OnWinHandler() =>
            _winLoseUI.Show(true);
        private void OnLoseHandler() =>
            _winLoseUI.Show(false);

        private IEnumerator Restart(float delay)
        {
            _winLoseUI.Hide();
            yield return new WaitForSeconds(delay);
            DestroyCurrentLevel();
            LoadLevel(_currentLevelIndex);
        }
        private IEnumerator Next(float delay)
        {
            _winLoseUI.Hide();
            yield return new WaitForSeconds(delay);
            LoadNextLevel();
        }

    }

}
