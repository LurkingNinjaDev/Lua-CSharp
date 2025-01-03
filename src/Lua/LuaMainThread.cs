namespace Lua;

public sealed class LuaMainThread : LuaThread
{
    public override LuaThreadStatus GetStatus()
    {
        return LuaThreadStatus.Running;
    }

    public override void UnsafeSetStatus(LuaThreadStatus status)
    {
        // Do nothing
    }

    public override ValueTask<int> ResumeAsync(LuaFunctionExecutionContext context, Memory<LuaValue> buffer, CancellationToken cancellationToken = default)
    {
        buffer.Span[0] = false;
        buffer.Span[1] = "cannot resume non-suspended coroutine";
        return new(2);
    }

    public override ValueTask<int> YieldAsync(LuaFunctionExecutionContext context, Memory<LuaValue> buffer, CancellationToken cancellationToken = default)
    {
        throw new LuaRuntimeException(context.State.GetTraceback(), "attempt to yield from outside a coroutine");
    }
}
