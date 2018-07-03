//namespace Kamen.Tests
//{
//    using System;
//    using System.Collections.Generic;
//    using System.Text;
//    using System.Threading.Tasks;
//    using Xunit;

//    public class StateMachineTests
//    {
//        private enum States : int { Open, Assigned, Closed, Delete }

//        [Fact]
//        public async void ChangesState()
//        {
//            var stateMachine = new StateMachine2<States>(States.Open);

//            stateMachine.Configure(States.Open)
//                        .Process(async () => { });

//            stateMachine.Configure(States.Assigned)
//                        .Process(async () => { });

//            stateMachine.Trigger(States.Assigned);
//            await stateMachine.ProcessAsync();
            
//            Assert.Equal(States.Assigned, stateMachine.CurrentState);
//        }
//    }
//}
