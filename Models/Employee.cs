using Models.Util;
using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Employee
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        public EmployeeStatus Status{ get; set; }

        [Required]
        public int JobID { get; set; }

        [Required]
        public string JobTitle { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTimeOffset HireDate { get; set; }

        [Required]
        public EmployeeType EmployeeType { get; set; }

        private string _email;
        public string Email { 
            get
            {
                return _email;
            }
            set 
            {
                if (EmailUtil.IsValidEmail(value))
                {
                    _email = value;
                }
                else
                {
                    throw new ArgumentException("The provided email is not in the correct format.");
                }
            } 
        }
        public string PhoneNumber { get; set; }

        public DateTimeOffset DOB { get; set; }
    }

    public enum EmployeeStatus
    {
        Active = 0,
        Teminated
    }

    public enum EmployeeType
    {
        FullTime = 0,
        PartTime,
        Contractor
    }
}
