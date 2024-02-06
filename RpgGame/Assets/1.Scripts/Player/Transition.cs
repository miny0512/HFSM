using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Transition
{
    public Transition(IState from, IState to, Func<bool> condition)
    {
        this.condition = condition;
        From = from;
        To = to;
    }
    private Func<bool> condition;
    public IState From { get; private set; }
    public IState To { get; private set; }

    public bool CheckCondition()
    {
        return condition.Invoke();
    }
}
