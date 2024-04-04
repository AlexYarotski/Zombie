using Project.Dev.Scripts.Setting;
using UnityEngine;

public class SceneContexts : Singleton<SceneContexts>
{ 
    [field: SerializeField]
    public SceneWindowSetting SceneWindowSetting
    {
        get;
        private set;
    }
    
    [field: SerializeField]
    public ResourceSetting ResourceSetting
    {
        get;
        private set;
    }
    
    [field: SerializeField]
    public EnemyGeneratorSetting EnemyGeneratorSetting
    {
        get;
        private set;
    }
    
    protected override void SingleAwake()
    {
        
    }
}
