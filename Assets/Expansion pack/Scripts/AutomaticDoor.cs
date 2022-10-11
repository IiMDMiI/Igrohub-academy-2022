using UnityEngine;
using Game;

namespace ExpansionPack
{
    public class AutomaticDoor : MonoBehaviour
    {
        [SerializeField] private float _startDelay;
        [SerializeField] private float _changeTime = 2;
        [SerializeField] private LayerMask _playerMask;
        private Vector3 _defaultPosition;

        private void Start()
        {
            _defaultPosition = transform.position;
            InvokeRepeating(nameof(PinPongPosition), _startDelay, _changeTime);
        }

        private void PinPongPosition() => 
            transform.position = transform.position == _defaultPosition ? _defaultPosition + Vector3.up : _defaultPosition;

        private void Update()
        {            
            if (Physics.Raycast(transform.position + Vector3.down, Vector3.up, out RaycastHit hit, 1f, _playerMask))
            {                
                if (hit.collider.TryGetComponent(out Player player))
                    player.Kill();
            }
        }


    }

}
