using UnityEngine;
using Game;

namespace ExpansionPack
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [HideInInspector] public Vector3 MoveDirection;
        private float _lifeTime = 5f;

        private void Start() =>
            LevelLoader.Instance.OnLevelLoad += DestroyOnLevelLoad; 

        private void Update()
        {
            transform.position += MoveDirection * _speed * Time.deltaTime;
            _lifeTime -= Time.deltaTime;
            if (_lifeTime <= 0)
                Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player))
                player.Kill();
                       
            if(other.GetComponent<Shooting>() == null)
                Destroy(gameObject);        
            
        }

        private void OnDestroy() => 
            LevelLoader.Instance.OnLevelLoad -= DestroyOnLevelLoad;

        private void DestroyOnLevelLoad() => 
            Destroy(gameObject);

    }
}
