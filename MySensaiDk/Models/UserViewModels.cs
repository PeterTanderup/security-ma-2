using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using PagedList;
using MySensaiDk.Infrastructure;

namespace MySensaiDk.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Feltet {0} er påkrævet")]
        [Display(Name = "E-mail")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Feltet {0} er påkrævet")]
        [DataType(DataType.Password)]
        [Display(Name = "Kode")]
        public string Password { get; set; }
    }

    public class EditUserDetails
    {
        [Required(ErrorMessage = "Feltet {0} er påkrævet")]
        [EmailAddress]
        [Display(Name = "E-mail")]
        [Remote("ValidateEmail", "User")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Feltet {0} er påkrævet")]
        [Display(Name = "Fornavn")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Feltet {0} er påkrævet")]
        [Display(Name = "Efternavn")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Feltet {0} er påkrævet")]
        [Display(Name = "Fødsels dato")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Birthday { get; set; }

        [Required(ErrorMessage = "Feltet {0} er påkrævet")]
        [Display(Name = "Køn")]
        public Gender Gender { get; set; }

        [Display(Name = "Telefonnummer")]
        [StringLength(11, MinimumLength = 8, ErrorMessage = "Dit {0} skal mindst have {2} tegn og højst {1}")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Feltet {0} er påkrævet")]
        [StringLength(100, ErrorMessage = "Din {0} skal være mindst {2} tegn langt.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Kode")]
        public string Password { get; set; }
    }

    public class AdminUserDetails
    {
        public AppUser CurrentUser { get; set; }
        public int Age
        {
            get
            {
                int age = DateTime.Now.Year - CurrentUser.Birthday.Year;
                return age - (CurrentUser.Birthday > DateTime.Today.AddYears(-age) ? 1 : 0);
            }
        }
    }

    public class AdminEditUserDetails
    {
        [Required(ErrorMessage = "Feltet {0} er påkrævet")]
        [EmailAddress]
        [Display(Name = "E-mail")]
        [Remote("ValidateEmail", "AdminUser", AdditionalFields = "Id")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Feltet {0} er påkrævet")]
        [Display(Name = "Fornavn")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Feltet {0} er påkrævet")]
        [Display(Name = "Efternavn")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Feltet {0} er påkrævet")]
        [Display(Name = "Fødsels dato")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DateRangeAttribute()]
        public DateTime Birthday { get; set; }

        [Required(ErrorMessage = "Feltet {0} er påkrævet")]
        [Display(Name = "Køn")]
        public Gender Gender { get; set; }

        [Display(Name = "Telefonnummer")]
        [StringLength(11, MinimumLength = 8, ErrorMessage ="Dit {0} skal mindst have {2} tegn og højst {1}")]
        public string PhoneNumber { get; set; }

        public string Id { get; set; }

    }
    public class EditUserPassword
    {
        [Required(ErrorMessage = "Feltet {0} er påkrævet")]
        [StringLength(100, ErrorMessage = "Din {0} skal være mindst {2} tegn langt.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Gammel kode")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Feltet {0} er påkrævet")]
        [StringLength(100, ErrorMessage = "Din {0} skal være mindst {2} tegn langt.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Ny kode")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Bekræft ny kode")]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "Ny kode og bekræft ny kode skal være ens")]
        public string ConfirmNewPassword { get; set; }
    }

    public class AdminRegisterViewModel
    {
        [Required(ErrorMessage = "Feltet {0} er påkrævet")]
        [EmailAddress]
        [Display(Name = "E-mail")]
        [Remote("ValidateEmailReg", "AdminUser")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Feltet {0} er påkrævet")]
        [Display(Name = "Fornavn")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Feltet {0} er påkrævet")]
        [Display(Name = "Efternavn")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Feltet {0} er påkrævet")]
        [Display(Name = "Fødsels dato")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DateRangeAttribute()]
        public DateTime Birthday { get; set; }

        [Required(ErrorMessage = "Feltet {0} er påkrævet")]
        [Display(Name = "Køn")]
        public Gender Gender { get; set; }

        [Display(Name = "Telefonnummer")]
        [StringLength(11, MinimumLength = 8, ErrorMessage = "Dit {0} skal mindst have {2} tegn og højst {1}")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Feltet {0} er påkrævet")]
        [StringLength(100, ErrorMessage = "Din {0} skal være mindst {2} tegn langt.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Kode")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Bekræft kode")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Kode og bekræft kode skal være ens")]
        public string ConfirmPassword { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Feltet {0} er påkrævet")]
        [EmailAddress]
        [Display(Name = "E-mail")]
        [Remote("ValidateEmail", "Account")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Feltet {0} er påkrævet")]
        [Display(Name = "Fornavn")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Feltet {0} er påkrævet")]
        [Display(Name = "Efternavn")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Feltet {0} er påkrævet")]
        [Display(Name = "Fødsels dato")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DateRangeAttribute()]
        public DateTime Birthday { get; set; }

        [Required(ErrorMessage = "Feltet {0} er påkrævet")]
        [Display(Name = "Køn")]
        public Gender Gender { get; set; }

        [Display(Name = "Telefonnummer")]
        [StringLength(11, MinimumLength = 8)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Feltet {0} er påkrævet")]
        [StringLength(100, ErrorMessage = "Din {0} skal være mindst {2} tegn langt.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Kode")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Bekræft kode")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Kode og bekræft kode skal være ens")]
        public string ConfirmPassword { get; set; }
    }

    public class UserDetails
    {
        public AppUser CurrentUser { get; set; }
        public IPagedList<Course> CurrentUserCourses { get; set; }
        public IPagedList<Address> CurrentUserAddresses { get; set; }
        public IPagedList<Enrollment> CurrentUserEnrollments { get; set; }
        public int Age
        {
            get
            {
                int age = DateTime.Now.Year - CurrentUser.Birthday.Year;
                return age - (CurrentUser.Birthday > DateTime.Today.AddYears(-age) ? 1 : 0);
            }
        }
    }

    public class CourseModel
    {
        public CourseModel()
        {

        }

        public CourseModel(Course cur)
        {
            this.Titel = cur.Titel;
            this.Description = cur.Description;
            this.StartTime = cur.StartTime;
            this.EndTime = cur.EndTime;
            this.ResponseDate = cur.ResponseDate;
            this.MaxSpots = cur.MaxSpots;
            this.AddressId = cur.AddressId;
            this.TeacherID = cur.TeacherID;
        }

        [Required(ErrorMessage = "Feltet {0} er påkrævet")]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Titel")]
        public string Titel { get; set; }

        [Required(ErrorMessage = "Feltet {0} er påkrævet")]
        [StringLength(255, MinimumLength = 10)]
        [Display(Name = "Detaljer")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Starter")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? StartTime { get; set; }

        [Display(Name = "Slutter")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? EndTime { get; set; }

        [Display(Name = "Frist")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? ResponseDate { get; set; }

        [Display(Name = "Pladser")]
        [Range(0, int.MaxValue, ErrorMessage = "Skal være positiv")]
        public int? MaxSpots { get; set; }

        [Display(Name = "Adresse")]
        public int? AddressId { get; set; }
        
        [Display(Name = "Lære")]
        public string TeacherID { get; set; }
    }
}
