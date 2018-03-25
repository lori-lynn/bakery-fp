using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bakery.Models
{
    public class NewPerson
    {
        [DisplayName("Last Name"), DisplayFormat(NullDisplayText = "Last Name")]
        public string PersonLastName { get; set; }

        [DisplayName("First Name"), DisplayFormat(NullDisplayText = "First Name")]
        public string PersonFirstName { get; set; }

        [DisplayName("Email"), DisplayFormat(NullDisplayText = "Email")]
        public string PersonEmail { get; set; }

        [DisplayName("Phone Number"), DisplayFormat(NullDisplayText = "Phone")]
        public string PersonPhone { get; set; }

        [DisplayName("Password"), DisplayFormat(NullDisplayText = "Password")]
        public string PersonPlainPassword { get; set; }
    }
}