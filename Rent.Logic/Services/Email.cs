using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Rent.Business.Interfaces;
using Rent.Common;
using Rent.DataAccess.Repositories;
using Rent.DataAccess.SqlDataAccess;
using Rent.Entities;
using Rent.Entities.Models;

namespace Rent.Business.Services
{
    public class Email : IEmail
    {
        private string _email;

        public Email()
        {
            
        }

        public Email(string email)
        {
            // repository = new GenericRepository<Entities.User>(new RentEntities());
            _email = email;
        }

        public string ForgotPasswordEmail(Entities.User obj)
        {
            var r = "";

            try
            {
                using (var context = new RentEntities())
                {
                    var objPassword =
                        context.Users.FirstOrDefault(x => x.Email == obj.Email && x.Active);

                    if (objPassword == null)
                    {
                        r = "Email was not found. Please try again.";
                        return r;
                    }

                    // password generate
                    var newPassword = Rent.Common.Helper.Generator.Password();

                    // set userPassword object
                    // call userPassword interface
                    Business.Interfaces.IUserPassword iUserPassword = new UsersPassword(new RentEntities());
                    var objUsersPassword = iUserPassword.Select(objPassword.Uid);
                    objUsersPassword.Password = newPassword;
                    iUserPassword.Update(objUsersPassword);

                    const string subject = "Rental Payment - New Password";
                    StringBuilder stringBuilder = new StringBuilder();

                    stringBuilder.Append("Hello " + objPassword.Name + ",");
                    stringBuilder.Append("<p>Your temporary password is: </br> " + newPassword);
                    stringBuilder.Append("</br></br>");
                    stringBuilder.Append("We encourage you to create a password you can easily remember. </br>");

                    stringBuilder.Append("You can login with your information here: " +
                                         Rent.Business.Properties.Settings.Default.Host +" to view your account.");
                    stringBuilder.Append("</br> ");

                    // send email
                    var i = Rent.Common.Helper.Email.MlfusionSmtp(new MailAddress(obj.Email), subject,
                        stringBuilder.ToString(), true);

                    var j = Business.Services.LogEmail.Insert(stringBuilder, obj.Uid, obj.Email);

                    // send text message
                    if (obj.Phone != null)
                    {
                        foreach (var source in Rent.Common.Helper.Carriers.SelectMobileCarriers(obj.Phone))
                        {
                            i =
                                + Rent.Common.Helper.Email.MlfusionSmtp(new MailAddress(source), subject,
                                    stringBuilder.ToString(), false);

                            // insert into LogEmail
                            j = + Business.Services.LogEmail.Insert(stringBuilder, obj.Uid, source);
                        }
                    }

                    r = i > 0
                        ? "Login credentials has been emailed to you."
                        : "Sorry, the web service is down. Please try again later.";
                }
            }
            catch (Exception exception)
            {
                Business.Services.LogError.Insert(exception, 0);
                r = "Error: " + exception.Message;
            }

            return r;
        }

        public string RentPaymentEmail(int id)
        {
            var r = "";
            var i = 0;
            var j = 0;

            try
            {
                using (var context = new RentEntities())
                {
                    var obj =
                        context.RentPayments.FirstOrDefault(x => x.RentPaymentId == id);

                    if (obj == null)
                    {
                        r = "Email was not found. Please try again.";
                        return r;
                    }

                    const string subject = "Rental Payment - Rental Payment Email";
                    StringBuilder stringBuilder = new StringBuilder();

                    stringBuilder.Append("<p>Hello, " + obj.User.Name + "</br> ");
                    stringBuilder.Append("Your rent payment was just posted. ");
                    stringBuilder.Append("Please login with your credentials here: " +
                                         Rent.Business.Properties.Settings.Default.Host +
                                         Properties.Settings.Default.Login + " to view your payment.");
                    stringBuilder.Append("</br>");

                    // send email and check if UsersNotification.Email = true
                    if ((bool) obj.User.UsersNotifications.FirstOrDefault().Email)
                    {
                        i = Rent.Common.Helper.Email.MlfusionSmtp(new MailAddress(obj.User.Email), subject,
                            stringBuilder.ToString(), true);
                        j = Business.Services.RentPaymentNoticeSendLog.Insert(stringBuilder, obj, obj.User.Email);

                        // insert into LogEmail
                        Business.Services.LogEmail.Insert(stringBuilder, obj.Uid, obj.User.Email);
                    }
                    else
                    {
                        var sb = new StringBuilder(Notifications.EmailNotice + stringBuilder);
                        // insert into LogEmail
                        Business.Services.LogEmail.Insert(sb, obj.Uid, obj.User.Email);
                    }

                    

                    // send text message
                        if (obj.User.Phone != null)
                            foreach (var source in Rent.Common.Helper.Carriers.SelectMobileCarriers(obj.User.Phone))
                            {
                                //check if UsersNotification.Phone = true
                                if ((bool) obj.User.UsersNotifications.FirstOrDefault().Phone)
                                {
                                    i = + Rent.Common.Helper.Email.MlfusionSmtp(new MailAddress(source), subject,
                                        stringBuilder.ToString(), false);
                                    j = + Business.Services.RentPaymentNoticeSendLog.Insert(stringBuilder, obj, source);

                                    // insert into LogEmail
                                    Business.Services.LogEmail.Insert(stringBuilder, obj.Uid, source);
                                }
                                else
                                {
                                    var sb = new StringBuilder(Notifications.PhoneNotice + stringBuilder);
                                    // insert into LogEmail
                                    Business.Services.LogEmail.Insert(sb, obj.Uid, source);
                                }
                            }

                    if (i > 0)
                    {
                        if (j <= 0)
                            return r = "Error occured on SQL end";
                    }

                    r = "Rent Payment email was sent to " + obj.User.Email + "; Email status -> " +
                        obj.User.UsersNotifications.FirstOrDefault().Email + "; Phone -> " + obj.User.UsersNotifications.FirstOrDefault().Phone;
                }
            }
            catch (Exception exception)
            {
                Business.Services.LogError.Insert(exception, id);
                r = "Error: " + exception.Message;
            }

            return r;
        }

        public string UserAccessRegistrationEmail(int id)
        {
            var r = "";
            var i = 0;
            var j = 0;

            try
            {
                Business.Interfaces.IUser iUser = new User(new RentEntities());
                var obj = iUser.Select(id);

                const string subject = "Rental Payment - Registration Access";
                StringBuilder stringBuilder = new StringBuilder();

                stringBuilder.Append("Welcome " + obj.Name + ",");
                stringBuilder.Append("<p>Below is your login credentials for the Rent Payment Web Application</br> ");
                stringBuilder.Append("Email: <strong>" + obj.Email + "</strong>");
                stringBuilder.Append("</br> ");
                stringBuilder.Append("Password: <strong>" + obj.UsersPasswords.FirstOrDefault().Password +
                                     "</strong>");
                stringBuilder.Append("</br></br> ");
                stringBuilder.Append("You can login with your information here: " +
                                     Rent.Business.Properties.Settings.Default.Host +
                                     Properties.Settings.Default.Login + " to view your payment.");
                stringBuilder.Append("</br> ");

                // send email and check if UsersNotification.Email = true
                if ((bool)obj.UsersNotifications.FirstOrDefault().Email)
                {
                    i = Rent.Common.Helper.Email.MlfusionSmtp(new MailAddress(obj.Email), subject,
                        stringBuilder.ToString(), true);

                    // insert into LogEmail
                    Business.Services.LogEmail.Insert(stringBuilder, obj.Uid, obj.Email);
                }
                else
                {
                    var sb = new StringBuilder(Notifications.EmailNotice + stringBuilder);
                    // insert into LogEmail
                    Business.Services.LogEmail.Insert(sb, obj.Uid, obj.Email);
                }

                r = i > 0 ? "Your login creditinals has been email to you." : "Sorry, the web service is down. Please try again later."; ;
            }
            catch (Exception exception)
            {
                Business.Services.LogError.Insert(exception, id);
                r = "Error: " + exception.Message;
            }
            return r;
        }

        public string UserRegistrationEmail(int id)
        {
            var r = "";
            var i = 0;
            var j = 0;

            try
            {
                Business.Interfaces.IUser iUser = new User(new RentEntities());
                var obj = iUser.Select(id);

                //var accessCode = Rent.Common.Helper.Generator.AccessCode();

                //// insert into userAccess
                //Business.Interfaces.IUserAccess iAccess = new UserAcess();
                //var result = iAccess.Insert(accessCode, id);

                //if (result != null)
                //    return result;


                const string subject = "Rental Payment - Registration Email";
                StringBuilder stringBuilder = new StringBuilder();

                stringBuilder.Append("Welcome " + obj.Name + ",");
                stringBuilder.Append("<p>Below is your access code for the Rent Payment Web Application</br> ");
                stringBuilder.Append("Access Code: <strong>" + obj.UsersAccesses.FirstOrDefault().AccessCode + "</strong>");
                stringBuilder.Append("</br> ");
                stringBuilder.Append("</br></br> ");
                stringBuilder.Append("You can login with your information here: " +
                                     Rent.Business.Properties.Settings.Default.Host  + " to view your account.");
                stringBuilder.Append("</br> ");

                // send email and check if UsersNotification.Email = true
                if ((bool)obj.UsersNotifications.FirstOrDefault().Email)
                {
                    i = Rent.Common.Helper.Email.MlfusionSmtp(new MailAddress(obj.Email), subject,
                        stringBuilder.ToString(), true);

                    // insert into LogEmail
                    Business.Services.LogEmail.Insert(stringBuilder, obj.Uid, obj.Email);
                }
                else
                {
                    var sb = new StringBuilder(Notifications.EmailNotice + stringBuilder);
                    // insert into LogEmail
                    Business.Services.LogEmail.Insert(sb, obj.Uid, obj.Email);
                }

                // send text message
                if (obj.Phone != null)
                    foreach (var source in Rent.Common.Helper.Carriers.SelectMobileCarriers(obj.Phone))
                    {
                        //check if UsersNotification.Phone = true
                        if ((bool)obj.UsersNotifications.FirstOrDefault().Phone)
                        {
                            i = +Rent.Common.Helper.Email.MlfusionSmtp(new MailAddress(source), subject,
                                stringBuilder.ToString(), false);

                            // insert into LogEmail
                            Business.Services.LogEmail.Insert(stringBuilder, obj.Uid, source);
                        }
                        else
                        {
                            var sb = new StringBuilder(Notifications.PhoneNotice + stringBuilder);
                            // insert into LogEmail
                            Business.Services.LogEmail.Insert(sb, obj.Uid, source);
                        }
                    }

                r = i > 0
                    ? "Registration email send successfully."
                    : obj.UsersNotifications.FirstOrDefault().Phone == false &&
                      obj.UsersNotifications.FirstOrDefault().Email == false
                        ? "Notification settings are turned off."
                        : "Sorry, the web service is down. Please try again later.";
            }
            catch (Exception exception)
            {
                Business.Services.LogError.Insert(exception, id);
                r = "Error: " + exception.Message;
            }
            return r;
        }
    }
}
