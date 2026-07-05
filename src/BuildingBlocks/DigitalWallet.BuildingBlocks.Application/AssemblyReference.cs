using System.Reflection;

namespace DigitalWallet.BuildingBlocks.Application;

public static class AssemblyReference
{
    public static Assembly Assembly =>
        typeof(AssemblyReference).Assembly;
}