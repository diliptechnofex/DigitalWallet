using System.Reflection;

namespace DigitalWallet.Modules.Wallets.Application;

public static class AssemblyReference
{
    public static Assembly Assembly =>
        typeof(AssemblyReference).Assembly;
}