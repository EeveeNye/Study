using System;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gyvr.Mythril2D
{
    [Serializable]
    public struct PlayerDataBlock
    {
        [LabelText("预制体")] public GameObject prefab;
        [LabelText("已使用点数")] public int usedPoints;
        [LabelText("经验值")] public int experience;
        [LabelText("当前缺失属性")] public Stats missingCurrentStats;
        [LabelText("自定义属性")] public Stats customStats;
        [LabelText("装备")] public Equipment[] equipments;
        [LabelText("已装备技能表")] public AbilitySheet[] equipedAbilities;
        [LabelText("位置")] public Vector3 position;
    }


    public class PlayerSystem : AGameSystem, IDataBlockHandler<PlayerDataBlock>
    {
        [Header("Settings")] [SerializeField] private GameObject m_dummyPlayerPrefab = null;

        public Hero PlayerInstance => m_playerInstance;
        public GameObject PlayerPrefab => m_playerPrefab;

        private GameObject m_playerPrefab = null;
        private Hero m_playerInstance = null;

        public override void OnSystemStart()
        {
            m_playerInstance = InstantiatePlayer(m_dummyPlayerPrefab);
        }

        private Hero InstantiatePlayer(GameObject prefab)
        {
            GameObject playerInstance = Instantiate(prefab, transform);
            Hero hero = playerInstance.GetComponent<Hero>();
            Debug.Assert(hero != null, "The player instance specified doesn't have a Hero component");
            m_playerPrefab = prefab;
            return hero;
        }

        public void LoadDataBlock(PlayerDataBlock block)
        {
            if (m_playerInstance)
            {
                Destroy(m_playerInstance.gameObject);
            }

            m_playerInstance = InstantiatePlayer(block.prefab);
            m_playerInstance.Initialize(block);
        }

        public PlayerDataBlock CreateDataBlock()
        {
            return new PlayerDataBlock
            {
                prefab = m_playerPrefab,
                usedPoints = m_playerInstance.usedPoints,
                experience = m_playerInstance.experience,
                equipments = m_playerInstance.equipments.Values.ToArray(),
                missingCurrentStats = m_playerInstance.stats - m_playerInstance.currentStats,
                customStats = m_playerInstance.customStats,
                equipedAbilities = m_playerInstance.equippedAbilities,
                position = m_playerInstance.transform.position
            };
        }
    }
}