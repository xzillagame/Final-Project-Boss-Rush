using UnityEngine;
using UnityEngine.Events;

namespace Xavian
{
    public abstract class BaseState : MonoBehaviour
    {
        protected BossStateMachine stateMachine;

        public event UnityAction OnStateCompleted;

        protected void FireStateCompletedEvent() => OnStateCompleted?.Invoke();

        virtual public void EnterState(BossStateMachine bossStateMachine) => stateMachine = bossStateMachine;
        virtual public void UpdateState() { }
        virtual public void ExitState() { }


    }
}