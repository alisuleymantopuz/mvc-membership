using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindMembershipApp.UI.Web.Models.Account
{
    public interface IWebPrincipal : IPrincipal
    {
        int Id { get; set; }

        string Name { get; set; }

        string Surname { get; set; }

        string Email { get; set; }

        int RoleId { get; set; }
    }
}
