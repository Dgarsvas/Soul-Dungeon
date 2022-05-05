using System;
using System.Collections.Generic;

public class StateMachine
{
    private IState curState;

    private Dictionary<Type, List<Transition>> transitions = new Dictionary<Type, List<Transition>>();
    private List<Transition> curTransitions = new List<Transition>();
    private List<Transition> anyTransitions = new List<Transition>();

    private static readonly List<Transition> EmptyTransitions = new List<Transition>(0);

    public void Tick()
    {
        var transition = GetTransition();
        if (transition != null)
        {
            SetState(transition.To);
        }

        curState?.Tick();
    }

    public void SetState(IState state)
    {
        if (state == curState)
        {
            return;
        }

        curState?.OnExit();
        curState = state;

        transitions.TryGetValue(curState.GetType(), out curTransitions);
        if (curTransitions == null)
        {
            curTransitions = EmptyTransitions;
        }

        curState.OnEnter();
    }

    public void AddTransition(IState from, IState to, Func<bool> predicate)
    {
        if (transitions.TryGetValue(from.GetType(), out var newTransitions) == false)
        {
            newTransitions = new List<Transition>();
            transitions[from.GetType()] = newTransitions;
        }

        newTransitions.Add(new Transition(to, predicate));
    }

    public void AddAnyTransition(IState state, Func<bool> predicate)
    {
        anyTransitions.Add(new Transition(state, predicate));
    }

    private class Transition
    {
        public Func<bool> Condition
        {
            get;
        }
        public IState To
        {
            get;
        }

        public Transition(IState to, Func<bool> condition)
        {
            To = to;
            Condition = condition;
        }
    }

    private Transition GetTransition()
    {
        foreach (var transition in anyTransitions)
        {
            if (transition.Condition())
            {
                return transition;
            }
        }

        foreach (var transition in curTransitions)
        {
            if (transition.Condition())
            {
                return transition;
            }
        }

        return null;
    }
}