namespace Lua.Standard.IO;

public sealed class FileLinesFunction : LuaFunction
{
    public override string Name => "lines";
    public static readonly FileLinesFunction Instance = new();

    protected override ValueTask<int> InvokeAsyncCore(LuaFunctionExecutionContext context, Memory<LuaValue> buffer, CancellationToken cancellationToken)
    {
        var arg0 = context.ReadArgument<FileHandle>(0);
        var arg1 = context.ArgumentCount >= 2
            ? context.Arguments[1]
            : "*l";
        
        buffer.Span[0] = new Iterator(arg0, arg1);
        return new(1);
    }

    class Iterator(FileHandle file, LuaValue format) : LuaFunction
    {
        readonly LuaValue[] formats = [format];

        protected override ValueTask<int> InvokeAsyncCore(LuaFunctionExecutionContext context, Memory<LuaValue> buffer, CancellationToken cancellationToken)
        {
            var resultCount = IOHelper.Read(file, Name, 0, true, context, formats, buffer);
            return new(resultCount);
        }
    }
}