using Java.Nio.Channels;
using SoilesuX.Model;
using SoilesuX.Services;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace SoilesuX.ViewModels.LoginPage
{
    /// <summary>
    /// ViewModel for sign-up page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class SignUpPageViewModel : LoginViewModel
    {
        #region Fields

        ApiServices _apiServices;

        private string email;

        private string name;

        private string password;

        private string confirmPassword;

        private string message;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="SignUpPageViewModel" /> class.
        /// </summary>
        public SignUpPageViewModel()
        {
            this.LoginCommand = new Command(this.LoginClicked);
            _apiServices = new ApiServices();
            //this.SignUpCommand = new Command(this.SignUpClicked);
        }

        #endregion

        #region Property

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the name from user in the Sign Up page.
        /// </summary>
         public string Message { get { return message; } set { this.message = value; this.NotifyPropertyChanged("Message"); } }
        public string Email
        {
            get
            {
                return this.email;
            }

            set
            {
                if (this.email == value)
                {
                    return;
                }

                this.email = value;
                this.NotifyPropertyChanged("Email");
            }
        }
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (this.name == value)
                {
                    return;
                }

                this.name = value;
                this.NotifyPropertyChanged("Name");
            }
        }

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the password from users in the Sign Up page.
        /// </summary>
        public string Password
        {
            get
            {
                return this.password;
            }

            set
            {
                if (this.password == value)
                {
                    return;
                }

                this.password = value;
                this.NotifyPropertyChanged("Password");
            }
        }

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the password confirmation from users in the Sign Up page.
        /// </summary>
        public string ConfirmPassword
        {
            get
            {
                return this.confirmPassword;
            }

            set
            {
                if (this.confirmPassword == value)
                {
                    return;
                }

                this.confirmPassword = value;
                this.NotifyPropertyChanged("ConfirmPassword");
            }
        }

        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the command that is executed when the Log In button is clicked.
        /// </summary>
        public Command LoginCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Sign Up button is clicked.
        /// </summary>
        public Command SignUpCommand {
            get
            {

                return new Command(async () =>
                {
                    User user = new User() { UserName = Name, Email = this.Email, Password = this.Password };
                   var response = await _apiServices.RegisterAsync(user);
                    if (response.IsSuccessStatusCode)
                    {
                        Message = "Registered successfully.Pls check you email for authentication";
                        Name = string.Empty;
                        Email = string.Empty;
                        Password = string.Empty;
                        ConfirmPassword = string.Empty;
                    }
                    else
                    {
                        Email = string.Empty;
                        Password = string.Empty;
                        ConfirmPassword = string.Empty;
                        Message = $"{response.Content.ReadAsStringAsync().Result}";                       
                    }
                });
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Invoked when the Log in button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void LoginClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the Sign Up button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        //private void SignUpClicked(object obj)
        //{
        //    // Do something
        //}

        #endregion
    }
}