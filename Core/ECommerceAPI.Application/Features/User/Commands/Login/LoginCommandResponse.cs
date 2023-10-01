using ECommerceAPI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.User.Commands.Login
{
    public class LoginCommandResponse
    {

    }
    public class LoginSuccessCommandResponse : LoginCommandResponse
    {
        public Token Token { get; set; }
    }
    public class LoginErrorCommandResponse : LoginCommandResponse
    {
        public string Message { get; set; }
    }
}
