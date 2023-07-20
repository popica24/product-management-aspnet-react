using Business.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Helpers
{
    public interface ITokenHelper
    {
        public JWTTokens Create(LoginRequest request);
    }
}
