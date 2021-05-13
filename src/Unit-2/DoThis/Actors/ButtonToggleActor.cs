using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ChartApp.Actors
{
    class ButtonToggleActor: UntypedActor
    {
        #region Message types
        public class Toggle { }

        #endregion

        private readonly CounterType _myCounterType;
        private bool _isToggleOn;
        private readonly Button _myButton;
        private readonly IActorRef _coordinatorActor;

        public ButtonToggleActor(IActorRef corrdinatorActor, Button myButton, CounterType myCounterType, bool isToggleOn = false)
        {
            _coordinatorActor = corrdinatorActor;
            _myButton = myButton;
            _isToggleOn = isToggleOn;
            _myCounterType = myCounterType;
        }

        protected override void OnReceive(object message)
        {
            if(message is Toggle && _isToggleOn)
            {
                _coordinatorActor.Tell(new PerformanceCounterCoordinatorActor.Unwatch(_myCounterType));
                FlipToggle();

            }
            else if(message is Toggle && !_isToggleOn)
            {
                _coordinatorActor.Tell(new PerformanceCounterCoordinatorActor.Watch(_myCounterType));
                FlipToggle();
            }
            else
            {
                Unhandled(message);
            }
        }

        private void FlipToggle()
        {
            _isToggleOn = !_isToggleOn;
            _myButton.Text = string.Format("{0}({1})", 
                _myCounterType.ToString().ToUpperInvariant(),
                _isToggleOn? "ON": "OFF");
        }
    }
}
