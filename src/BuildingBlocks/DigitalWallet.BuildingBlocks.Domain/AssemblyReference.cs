using System.Reflection;

namespace DigitalWallet.BuildingBlocks.Domain;

public static class AssemblyReference
{
    public static Assembly Assembly =>
        typeof(AssemblyReference).Assembly;
}
