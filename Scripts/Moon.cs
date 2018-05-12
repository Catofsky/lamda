
using MoonSharp.Interpreter;
using System;
using System.Threading;
using UnityEngine;

public class Moon
{
    private Thread thread;
    private BreakDebugger debugger;
    private string code;
    private Script script;
    private bool running;
    private int ticks;

    public Moon(string code, int ticks = 200)
    {
        this.code = code;
        this.ticks = ticks;
        running = false;

        var sandboxMode = 
              CoreModules.GlobalConsts
            | CoreModules.TableIterators
            | CoreModules.String
            | CoreModules.Table
            | CoreModules.Basic
            | CoreModules.Math
            | CoreModules.Bit32;

        script = new Script(sandboxMode);

        script.Options.DebugPrint = s => { OnPrint(s); };

        //script.Globals["delta"] = (Func<int>) GetDelta;
    }

    public void Start()
    {
        thread = new Thread(Run);
        thread.Start(script);
    }

    public void Stop()
    {
        if (running)
            debugger.Stop();
    }

    private void Run(object param)
    {
        Script script = (Script)param;
        var stopped = false;

        try
        {
            Boolean stop = false;
            debugger = new BreakDebugger();
            debugger.SetTicks(ticks);
            debugger.SetStop(stop);
            debugger.OnTick = OnTick;

            script.AttachDebugger(debugger);
            running = true;
            script.DoString(code);
        }
        catch (EndException)
        {
            stopped = true;
            OnEnd(debugger.GetStop());
        }
        catch (InterpreterException ie)
        {
            OnLuaError(ie.Message);
        }

        running = false;
        if (!stopped)
            OnEnd(false);
    }

    public event Action<bool> OnEnd;
    public event Action OnTick;
    public event Action<string> OnLuaError;
    public event Action<string> OnPrint;
    
}
