using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.ResponseModel
{
    public class ResponseModel
    {
        public int ID { set; get; }

        public string FirstName { set; get; }

        public string LastName { set; get; }

        public string Email { set; get; }
    }
}
