﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PawFund.Contract.DTOs.Account;
namespace PawFund.Contract.Services.Accounts
{

    public static class Response
    {

        public record UserResponse(Guid Id, string FirstName, string LastName, string Email, string PhoneNum, bool Status);

        public record GetListUserResponse(List<GetUserAccount.AccountDto> listAccountDto);
    }
}
