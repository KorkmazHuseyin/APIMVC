using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIMVC.MyApis.DAL.Entities
{
    public class User
    {
        // Kullanıcıya ait bilgiler burda tutulacak

        public int UserID { get; set; }
        public byte[] PasswordSalt { get; set; }// Kullanıcıdan alınan bilgiler 2 paraçaya bölünüyorve PasswordSalt ve passwordHash olarak                                              tutuluyor
        public byte[] PasswordHash { get; set; } // Kullanıcıdan alınan bilgiler 2 paraçaya bölünüyorve PasswordSalt ve passwordHash olarak                                              tutuluyor
        public string UserName { get; set; }
    }
}
