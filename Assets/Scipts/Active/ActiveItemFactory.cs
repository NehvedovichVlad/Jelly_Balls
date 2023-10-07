using System;
using UnityEngine;

namespace Assets.Scipts.Active
{
    public class ActiveItemFactory
    {
        private readonly string _pathBall = "Prefab/Ball";

        public ActiveItem Get(ActiveItemTypes activeItemTypes)
        {
            switch (activeItemTypes) 
            {
                case ActiveItemTypes.Ball:
                    return Resources.Load<ActiveItem>(_pathBall);
                default:
                    throw new ArgumentException(nameof(ActiveItem));
            }
        }

    }

    public enum ActiveItemTypes 
    {
        Ball,
        Star,
        Dynamit,
    }
    
}