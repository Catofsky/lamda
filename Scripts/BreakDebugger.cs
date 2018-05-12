using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Debugging;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EndException : Exception
{
}

class BreakDebugger : IDebugger
{
    int m_InstructionCounter = 0;
    List<DynamicExpression> m_Dynamics = new List<DynamicExpression>();
    Boolean stop;
    int ticks = 1000;
    public Action OnTick;

    public void SetTicks(int ticks)
    {
        this.ticks = ticks;
    }

    public void SetStop(Boolean stop)
    {
        this.stop = stop;
    }

    public Boolean GetStop()
    {
        return stop;
    }

    public void Stop()
    {
        stop = true;
    }

    public DebuggerAction GetAction(int ip, SourceRef sourceref)
    {
        m_InstructionCounter += 1;

        if (stop)
            throw new EndException();

        if (m_InstructionCounter == ticks)
        {
            m_InstructionCounter = 0;
            OnTick();
        }

        return new DebuggerAction()
        {
            Action = DebuggerAction.ActionType.StepIn,
        };
    }

    public DebuggerCaps GetDebuggerCaps()
    {
        return DebuggerCaps.CanDebugSourceCode;
    }

    public List<DynamicExpression> GetWatchItems()
    {
        return m_Dynamics;
    }

    public bool IsPauseRequested()
    {
        return true;
    }

    public void RefreshBreakpoints(IEnumerable<SourceRef> refs)
    {
    }

    public void SetByteCode(string[] byteCode)
    {
    }

    public void SetDebugService(DebugService debugService)
    {
    }

    public void SetSourceCode(SourceCode sourceCode)
    {
    }

    public void SignalExecutionEnded()
    {
    }

    public bool SignalRuntimeException(ScriptRuntimeException ex)
    {
        return false;
    }

    public void Update(WatchType watchType, IEnumerable<WatchItem> items)
    {
    }

}
