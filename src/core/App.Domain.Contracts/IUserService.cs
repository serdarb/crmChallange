﻿using System.ServiceModel;

namespace App.Domain.Contracts
{
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        string CreateUser(UserDto dto);
        [OperationContract]
        bool Authenticate(UserDto dto);
        [OperationContract]
        UserDto GetUser(string id);
        [OperationContract]
        UserDto GetUserByEmail(string email);
    }
}