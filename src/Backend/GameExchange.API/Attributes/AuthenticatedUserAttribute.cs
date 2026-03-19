using GameExchange.API.Filters;
using Microsoft.AspNetCore.Mvc;

namespace GameExchange.API.Attributes
{
    public class AuthenticatedUserAttribute : TypeFilterAttribute
    {
        public AuthenticatedUserAttribute() : base(typeof(AuthenticatedUserFilter))
        {
        }
    }
}
