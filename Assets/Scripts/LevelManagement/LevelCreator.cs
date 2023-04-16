using UnityEngine;

    public class LevelCreator : MonoBehaviour
    {
        [SerializeField] private Transform _currentLevelPlatform;
        public Transform CurrentLevelPlatform => _currentLevelPlatform;
    
    }

