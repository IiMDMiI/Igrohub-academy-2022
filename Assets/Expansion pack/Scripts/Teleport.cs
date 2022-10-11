using UnityEngine;
using Game;

namespace ExpansionPack
{
    public class Teleport : MonoBehaviour
    {
        [SerializeField] private Teleport _exit;
        private bool _isAbleToPort = true;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player) && _isAbleToPort)
                player.transform.position = _exit.transform.position;

            _exit._isAbleToPort = false;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Player player))
                _isAbleToPort = true;
        }
    }

}
