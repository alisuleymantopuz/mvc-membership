using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthwindMembershipApp.UI.Web.Models.Account
{
    public class WebPrincipalSerializeModel
    {
        public string Email { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public int RoleId { get; set; }

        public string Surname { get; set; }
    }
}