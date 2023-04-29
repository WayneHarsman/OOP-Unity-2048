using UnityEditor;
using UnityEngine;

namespace DefaultNamespace
{
    [CustomEditor(typeof(GameMaster))]
    public class GameMasterEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            var gameMaster = (GameMaster) target;
            if (GUILayout.Button("New Game"))
            {
                gameMaster.NewGame();
            }
            
        }
    }
}