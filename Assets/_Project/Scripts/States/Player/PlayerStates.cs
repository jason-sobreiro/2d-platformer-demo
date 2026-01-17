using System;

namespace Scripts.States.Player
{


    public static class PlayerStates
    {
        private static States _currentState;
        public static States CurrentState
        {
            get => _currentState;
            set
            {
                if (_currentState != value)
                {
                    _currentState = value;
                }
            }
        }
        public enum States
        {
            Idle,
            Run,
            Jump,
            Attack,
            Run_Attack,
            Attack_Up,
            Hurt,
            Death,
            Double_Jump,
            Wall_Hang
        }
       
    }

}
