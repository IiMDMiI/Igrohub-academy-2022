using UnityEngine;

namespace ExpansionPack
{
    public class Shooting : MonoBehaviour
    {
        [SerializeField] private float _shotInterval;
        [SerializeField] private Projectile _projectilePefab;
        [SerializeField] private Vector3 _shotDirection;
        private float _time;

        private void Update()
        {
            _time -= Time.deltaTime;
            if (_time <= 0)
            {
                _time = _shotInterval;
                Shoot();
            }
        }
        private void Shoot()
        {
            Projectile projectile = Instantiate(_projectilePefab, transform.position + Vector3.up / 2, Quaternion.identity);
            projectile.MoveDirection = _shotDirection;
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + _shotDirection);
        }
    }
}
