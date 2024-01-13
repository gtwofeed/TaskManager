﻿namespace TaskManager.Api.Models
{
    public class User : CommpnModel
    {
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime LastLoginDate { get; set;}
        public byte[] Photo { get; set; }
    }
}
