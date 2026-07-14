using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalWallet.Modules.Wallets.Application.Results
{
    public interface IResultFactory<TSelf> where TSelf : IResultFactory<TSelf>
    {
        static abstract TSelf Failure(IReadOnlyCollection<Error> errors);
    }
}
