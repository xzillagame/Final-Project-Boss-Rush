using Cinemachine;
using UnityEngine;

namespace Xavian
{
    public class LocalGameManager : MonoBehaviour
    {
        [field: SerializeField] public PlayerLogic Player { get; private set; }
        [field: SerializeField] public BossDataAndInitalizer Boss { get; private set; }

        [field: SerializeField] public MeshCollider GroundCollider { get; private set; }
        [SerializeField] private float boundsExtentsEdgeReduction = -2f;

        [field: SerializeField] public Transform PlayerFinalStandoffPosition { get; private set; }
        [field: SerializeField] public Transform BossFinalStandoffPosition { get; private set; }

        [field: SerializeField] public CinemachineVirtualCamera FinalStandCamera { get; private set; }

        public Vector3 GetRandomPointInArenaBounds()
        {
            Vector3 extent = GroundCollider.bounds.extents;
            
            //Slight offset from the ground
            extent.y = 0.1f;

            extent.x += boundsExtentsEdgeReduction;
            extent.z += boundsExtentsEdgeReduction;


            Vector3 areanaOrginPosition = GroundCollider.transform.position;


            Vector3 randomPosition = Vector3.zero;
            randomPosition.x = Random.Range(areanaOrginPosition.x - extent.x, areanaOrginPosition.x + extent.x);
            randomPosition.z = Random.Range(areanaOrginPosition.z - extent.z, areanaOrginPosition.z + extent.z);
            randomPosition.y = extent.y;

            return randomPosition;

        }

        public void BeginFinalPhase()
        {
            Boss.transform.position = BossFinalStandoffPosition.position;
            CharacterController controller = Player.GetComponent<CharacterController>();
            controller.enabled = false;
            Player.transform.position = PlayerFinalStandoffPosition.position;
            controller.enabled = true;

            Player.transform.LookAt(Boss.transform,Vector3.up);
            Boss.transform.LookAt(Player.transform, Vector3.up);
            FinalStandCamera.Priority += 5;
        }


    }
}