using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using prueba.DTOS;

namespace prueba.services
{
    public class hashService
    {
        public resultHashDto hash(string textPlane)
        {
            byte[] sal = new byte[16];
            using (RandomNumberGenerator random = RandomNumberGenerator.Create())
            {
                random.GetBytes(sal);
            }
            return newHash(textPlane, sal);
        }
        public resultHashDto newHash(string textPlane, byte[] sal)
        {
            byte[] derivedKey = KeyDerivation.Pbkdf2(textPlane, sal, KeyDerivationPrf.HMACSHA1, 10000, 32);
            string hash = Convert.ToBase64String(derivedKey);
            return new resultHashDto
            {
                hash = hash,
                sal = sal
            };
        }
    }
}