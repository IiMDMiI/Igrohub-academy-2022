using UnityEngine;
using Game;

namespace ExpansionPack
{
    public class DoorKey : MonoBehaviour
    {   
        [SerializeField] private int _keyId; 

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out Player player))
            {   
                var doors = FindObjectsOfType<LockedDoor>();
                foreach (var door in doors)
                {  
                    if(door.ID == _keyId)
                        Destroy(door.gameObject);  
                    Destroy(gameObject);  
                }
            }             
        }
    }
    
    
    

}
