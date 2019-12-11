using System;

namespace entities
{
    public class PlayerAction
    {
        public PlayerAction()
        {
        }

        public ActionType ActionType { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }

    public enum ActionType
    {
        RotateLeft,
        RotateRight,
        Thrust
    }
}


