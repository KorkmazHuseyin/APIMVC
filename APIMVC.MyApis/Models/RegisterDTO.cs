using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIMVC.MyApis.Models
{
    // Kayıt sırasında kişiden alınacak bilgiler tutulacak burada
    public class RegisterDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
