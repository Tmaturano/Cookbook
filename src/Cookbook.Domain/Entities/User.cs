﻿namespace Cookbook.Domain.Entities;

public class User : EntityBase
{
    protected User()
    {
    }

    public User(string name, string email, string phone)
    {           
        Name = name;
        Email = email;
        Phone = phone;
    }    
    
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public string Phone { get; private set; }

    public void SetPassword(string password) => PasswordHash = password;
}
