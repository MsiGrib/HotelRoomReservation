﻿using DataModel.DataBase;
using DataModel.DataStructures;

namespace IdentityMService.Service
{
    public interface IUserService
    {
        public Task<UserDTO?> GetUserByIdAsync(string idGuid);
        public Task<UserDTO?> GetUserByEmailAsync(string email);
        public Task<Pair<UserDTO, bool>> RegistrationUserAsync(string login, string password, string numberPhone, string lastName, string firstName, string email, DateTime birthday);
        public Task<bool> IsExistsRegistrUserAsync(string login, string email);
        public Task<UserDTO?> AuthorizationUserAsync(string login, string password);
    }
}
