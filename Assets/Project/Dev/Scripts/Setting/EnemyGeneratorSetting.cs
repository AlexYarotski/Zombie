using System;
using UnityEngine;

namespace Project.Dev.Scripts.Setting
{
    [CreateAssetMenu(fileName = "EnemyGeneratorSetting", menuName = "Settings/EnemyGeneratorSetting", order = 0)]
    public class EnemyGeneratorSetting : ScriptableObject
    {
        [Serializable]
        public class EnemyConfig
        {
            [field: SerializeField]
            public Enemy Enemy
            {
                get;
                private set;
            }

            [field: SerializeField]
            public DropResource.DropResourceConfig[] DropResourceConfigs
            {
                get;
                private set;
            }

            [field: SerializeField]
            public int Count
            {
                get;
                private set;
            }
        }

        [SerializeField]
        private EnemyConfig[] _enemyConfigs = null;

        public EnemyConfig[] GetEnemyConfig()
        {
            return _enemyConfigs;
        }
    }
}