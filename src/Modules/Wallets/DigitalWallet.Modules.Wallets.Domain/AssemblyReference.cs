using System.Reflection;

namespace DigitalWallet.Modules.Wallets.Domain;

public static class AssemblyReference
{
    public static Assembly Assembly =>
        typeof(AssemblyReference).Assembly;
}
