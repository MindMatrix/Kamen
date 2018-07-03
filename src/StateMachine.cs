//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace Kamen
//{


//    public interface IStateMachineState
//    {
//        void Enter();
//        Task<IStateMachineState> Process();
//        void Leave();
//    }


//    public class StateMachine
//    {
//        private IStateMachineState _stateChange;
//        private IStateMachineState _currentState;

//        public StateMachine(IStateMachineState initialState)
//        {
//            _stateChange = initialState;
//        }

//        public async Task Process()
//        {
//            if (_stateChange != null)
//            {
//                _currentState?.Leave();
//                _currentState = _stateChange;
//                _stateChange = null;

//                _currentState.Enter();
//            }

//            _stateChange = await _currentState.Process();
//        }
//    }


//    public class State<TState>
//    {
//        public TState _state;
//        public Action _enter;
//        public Action _leave;
//        public Func<Task> _processAsync;

//        public State(TState state)
//        {
//            _state = state;
//        }

//        //public State<TState> Process(Action process)
//        //{
//        //    var task = new Task(process);

//        //    return Process(async () => await (new Task(process).));
//        //}

//        public State<TState> Process(Func<Task> process)
//        {
//            _processAsync = process;
//            return this;
//        }

//        public State<TState> Enter(Action enter)
//        {
//            _enter = enter;
//            return this;
//        }

//        public State<TState> Leave(Action leave)
//        {
//            _leave = leave;
//            return this;
//        }
//    }

//    public class StateMachine2<TState>
//    where TState : struct
//    {
//        private Dictionary<TState, State<TState>> _states = new Dictionary<TState, State<TState>>();
//        private TState? _stateChange;
//        private State<TState> _currentState;

//        public TState CurrentState => _currentState._state;

//        public StateMachine2(TState initialState)
//        {
//            _stateChange = initialState;
//        }

//        public State<TState> Configure(TState state)
//        {
//            if (_states.ContainsKey(state))
//                throw new ArgumentException($"StateMachine already contains {state}", nameof(state));

//            var st = new State<TState>(state);
//            _states.Add(state, st);
//            return st;
//        }

//        public void Trigger(TState newState)
//        {
//            _stateChange = newState;
//        }

//        private void ChangeState(TState newState)
//        {
//            if (!_states.ContainsKey(newState))
//                throw new ArgumentException($"StateMachine doesn't contain {newState}", nameof(newState));

//            var _newState = _states[newState];
//            _stateChange = null;
//            if (_currentState != null && _newState == _currentState)
//                return;

//            _currentState?._leave?.Invoke();
//            _currentState = _newState;
//            _currentState?._enter?.Invoke();
//        }

//        public async Task ProcessAsync()
//        {
//            if (_stateChange.HasValue)
//                ChangeState(_stateChange.Value);

//            await _currentState._processAsync();
//        }

//        //public void Process()
//        //{
//        //    AsyncHelpers.RunSync(ProcessAsync);
//        //}
//    }

//}
