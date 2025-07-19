using System.Reflection;

Console.WriteLine("\e[1mR2R App: Testing reflection\e[0m");

var type = Assembly.Load("SharedLib").GetType("SharedLib.MathUtils");
var method = type?.GetMethod("Add");

if (method != null)
{
    object? invokeResult = method.Invoke(null, [5, 10]);
    if (invokeResult is int result)
    {
        Console.WriteLine($"5 + 10 = {result}");
    }
    else
    {
        Console.WriteLine("Method invocation did not return an int.");
    }
}
else
{
    Console.WriteLine("Failed to reflect on SharedLib.MathUtils.");
}
