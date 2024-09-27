namespace Lua.Standard.IO;

public sealed class FileWriteFunction : LuaFunction
{
    public override string Name => "write";
    public static readonly FileWriteFunction Instance = new();

    protected override ValueTask<int> InvokeAsyncCore(LuaFunctionExecutionContext context, Memory<LuaValue> buffer, CancellationToken cancellationToken)
    {
        var file = context.ReadArgument<FileHandle>(0);
        var resultCount = IOHelper.Write(file, Name, context, buffer, cancellationToken);
        return new(resultCount);
    }
}