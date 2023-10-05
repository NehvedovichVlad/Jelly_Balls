using System;
using UnityEngine;

namespace Assets.Scipts.Active
{
    public class ActiveItemFactory
    {
        private Transform _tube;

        public ActiveItem Get(ActiveItemTypes activeItemTypes, ActiveItem activeItem)
        {
            switch (activeItemTypes) 
            {
                case ActiveItemTypes.Ball:
                    return (Ball)activeItem;
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