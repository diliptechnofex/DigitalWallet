using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalWallet.Modules.Wallets.Application.Results
{
    public enum ErrorType
    {
        Validation = 1,
        NotFound = 2,
        Conflict = 3,
        Failure = 4
    }
}
