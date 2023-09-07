using APIMVC.MyApis.DAL.AuthDAL.Interfaces;
using APIMVC.MyApis.DAL.Context;
using APIMVC.MyApis.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace APIMVC.MyApis.DAL.AuthDAL.Concrete
{
    public class AuthDAL : IAuthDAL
    {
        private readonly AuthContext _context;
        public AuthDAL(AuthContext context)
        {
            _context = context;
        }

     



        public async Task<User> Login(string username, string password)
        {
            var kisi = await _context.User.FirstOrDefaultAsync(a => a.UserName == username);
            if (kisi == null)
            {
                return null;
            }
            if (! KontrolEt(password,kisi.PasswordSalt,kisi.PasswordHash))
            {
                return null;
            }
            return kisi;
        }

        private bool KontrolEt(string password, byte[] passwordSalt, byte[] passwordHash)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var passHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < password.Length; i++)
                {
                    if (passHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passHash, passSalt;

            KullaniciKaydetSifre(password, out passHash, out passSalt);
            user.PasswordHash = passHash;
            user.PasswordSalt = passSalt;
            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;

        }

        private void KullaniciKaydetSifre(string password, out byte[] passHash, out byte[] passSalt)
        {
            using (HMACSHA512 hmac = new HMACSHA512())
            {
                passHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                passSalt = hmac.Key;
            }
        }

        public async Task<bool> UserExists(string username)
        {
            return !await _context.User.AnyAsync(a => a.UserName == username);
        }
    }
}
