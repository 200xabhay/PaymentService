using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentServices.Domain.Enums
{
    public enum PaymentMethod
    {
        UPI = 1,
        NetBanking = 2,
        AutoDebit = 3,
        Cash = 4,
        Cheque = 5,
        NEFT = 6,
        RTGS = 7
    }
}
