using System;
using UnityEngine;

namespace UpgradedCards
{
    public abstract class UpgradedCard : MonoBehaviour
    {
        private const int StartLevelBorder = 5;
        
        [field: SerializeField] public int Id { get; private set; }

        public event Action<int> LevelChanged;
        public event Action<int, int> LevelBorderChanged;
        
        public int InitialLevel { get; private set; } = 1;
        public bool IsChosen { get; private set; }
        public int LevelBorder { get; private set; } = 5;

        protected int Level { get; private set; }
        
        public void SetCardLevel(int value)
        {
            if (value >= Level)
                Level = value;
            
            LevelChanged?.Invoke(Level);
        }

        public void SetLevelBorder(int value)
        {
            if (value <= LevelBorder) 
                return;
            
            LevelBorder = value;
        }
        
        public void Choose()
        {
            IsChosen = true;
        }

        protected void ReachBorder()
        {
            LevelBorder += StartLevelBorder;
            LevelBorderChanged?.Invoke(Id, LevelBorder);
        }
    }
}