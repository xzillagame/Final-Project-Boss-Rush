using UnityEngine;

namespace Xavian
{
    public class LocalGameManager : MonoBehaviour
    {
        [field: SerializeField] public PlayerLogic Player { get; private set; }
        [field: SerializeField] public BossDataAndInitalizer Boss { get; private set; }

        [field: SerializeField] public MeshCollider GroundCollider { get; private set; }
        [SerializeField] private float boundsExtentsEdgeReduction = -2f;


        [SerializeField] GameObject attackIndicator;

        [ContextMenu("Test Bounds")]
        private void Test()
        {
            Vector3 randomPos = GetRandomPointInArenaBounds();
            Debug.Log(randomPos);
            GameObject t = Instantiate(attackIndicator);
            t.transform.position = randomPos;

        }
        

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



    }
}