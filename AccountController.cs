using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TestExam.Models;
using TestExamDataAccessModal.entitymodal;

namespace TestExam.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        public ActionResult Index(string returnUrl)
        {
            // Membership.ApplicationName = "/";
            if (Session["Username"] == null)
            {
                ViewBag.ReturnUrl = returnUrl;
            }
            //Logic for Remember me
            HttpCookie myCookie = new HttpCookie("YourAppLogin");
            myCookie = Request.Cookies["YourAppLogin"];

            if (myCookie != null)
            {
                var db = new TestEntity();
                string Uname = Request.Cookies["YourAppLogin"]["LoginName"];

                if (!string.IsNullOrEmpty(Uname))
                {
                    tblUserInformation objUserInfo = db.tblUserInformations.Where(x => x.aspnet_Users.UserName.Equals(Uname.ToLower())).FirstOrDefault();
                    if (Session["Username"] == null)
                    {
                        Session["Username"] = objUserInfo.FirstName + " " + objUserInfo.LastName;
                    }
                }
                return Redirect("/Admin/DashBoard");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Index(AccountModel objModel, string returnUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var db = new TestEntity())
                    {
                        Membership.ApplicationName = "/";
                        tblUserInformation objUserInfo = db.tblUserInformations.Where(x => x.aspnet_Users.UserName.ToLower().Equals(objModel.UserName.ToLower())).FirstOrDefault();
                        if (objUserInfo != null)
                        {
                            //if (objUserInfo.Status == 1)
                            //{
                            if (Membership.ValidateUser(objModel.UserName, objModel.Password))
                            {
                                string VisitorsIPAddr = string.Empty;
                                if (Request.UserHostAddress.Length != 0)
                                {
                                    VisitorsIPAddr = Request.UserHostAddress;
                                }

                                //tbllastlogin objLastLogin = new tbllastlogin();
                                //objLastLogin.idUser = objUserInfo.id;
                                //objLastLogin.IpAddress = VisitorsIPAddr;
                                //objLastLogin.DateTimeStamp = DateTime.Now;
                                //db.tbllastlogins.Add(objLastLogin);
                                //db.SaveChanges(User.Identity.Name);

                                FormsAuthentication.RedirectFromLoginPage(objModel.UserName, objModel.RememberMe);
                                FormsAuthentication.SetAuthCookie(objModel.UserName, objModel.RememberMe);
                                if (Session["Username"] == null)
                                {
                                    Session["Username"] = objUserInfo.FirstName + " " + objUserInfo.LastName;
                                    string host = HttpContext.Request.Url.Host;
                                }
                                if (objModel.RememberMe == true)
                                {
                                    HttpCookie cookie = new HttpCookie("YourAppLogin");
                                    cookie.Values.Add("LoginName", objModel.UserName);
                                    cookie.Expires = DateTime.Now.AddMinutes(1440);
                                    //cookie.Expires = DateTime.Now.AddMinutes(15);
                                    Response.Cookies.Add(cookie);
                                }
                                else
                                {
                                    HttpCookie cookie = new HttpCookie("YourAppLogin");
                                    cookie.Values.Add("LoginName", null);
                                    cookie.Expires = DateTime.Now.AddDays(0);
                                    Response.Cookies.Add(cookie);
                                }

                                if (returnUrl == null || returnUrl == "")
                                {
                                    return RedirectToAction("Index", "Home");
                                }
                                else
                                {
                                    return Redirect(returnUrl);
                                }
                            }
                            else
                            {
                                ViewData["error"] = 1;
                                ViewData["message"] = "Invalid User Name or password";
                            }
                        }
                        else
                        {
                            ViewData["error"] = 1;
                            ViewData["message"] = "Invalid User Name or password";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ViewData["error"] = 1;
                ViewData["message"] = ex.Message.ToString();
            }
            return View();
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        public JsonResult CreateMemberRegister(tblUserInformation objUser)
        {
            Dictionary<string, object> res = new Dictionary<string, object>();
            try
            {
                using (var db = new TestEntity())
                {
                    Membership.ApplicationName = "/";

                    Membership.CreateUser(objUser.Phone.ToString(), objUser.Phone.ToString());
                    MembershipUser user = Membership.GetUser(objUser.Phone.ToString());

                    Roles.ApplicationName = "/";
                    if (!Roles.RoleExists("Customer"))
                    {
                        Roles.CreateRole("Customer");
                        Roles.AddUserToRole(objUser.Phone.ToString(), "Customer");
                    }
                    else
                    {
                        Roles.AddUserToRole(objUser.Phone.ToString(), "Customer");
                    }
                    tblUserInformation objmember = new tblUserInformation();
                    objmember.idUser = new Guid(user.ProviderUserKey.ToString());
                    objmember.FirstName = objUser.FirstName;
                    objmember.LastName = objUser.LastName;
                    objmember.Phone = objUser.Phone.ToString();
                    objmember.DOB = objUser.DOB;
                    objmember.IsActive = true;
                    objmember.IsCompleted = true;
                    objmember.IsNotify = false;
                    objmember.CreatedBy = User.Identity.Name;
                    objmember.CreatedDatetime = DateTime.Now;
                    db.tblUserInformations.Add(objmember);
                    db.SaveChanges();
                    FormsAuthentication.SetAuthCookie(objUser.Phone.ToString(), false);

                    Session["Username"] = null;
                    Session["LanguageId"] = null;

                    if (Session["Username"] == null)
                    {
                        Session["Username"] = objUser.FirstName + " " + objUser.LastName;
                    }
                    res["success"] = 1;
                }
            }
            catch (Exception ex)
            {
                res["error"] = 2;
                res["message"] = ex.Message.ToString();
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        //         public JsonResult GetUser(int idUser)
        //{
        //    MemberList obj = new MemberList();
        //    using (var db = new bigchoysenEntities())
        //    {
        //        obj = db.tbluserinformations.Where(x => x.idUser == idUser).Select(x => new MemberList()
        //        {
        //            id = x.id,
        //            idUser = x.idUser,
        //            Email = x.Email,
        //            FirstName = x.FirstName,
        //            LastName = x.LastName,
        //            idSource = x.idSource,
        //            HandphoneNumber = x.HandphoneNumber,
        //            Gender = x.Gender,
        //            DOB = x.DOB,
        //            Month = x.DOB.HasValue ? x.DOB.Value.Month : 0,
        //            Day = x.DOB.HasValue ? x.DOB.Value.Day : 0,
        //            Year = x.DOB.HasValue ? x.DOB.Value.Year : 0,
        //            PhoneNumber = x.PhoneNumber,
        //            PrefferdLanguage = x.PrefferdLanguage,
        //            PrefferdCurrency = x.PrefferdCurrency,
        //            SmsNotificaion = x.SmsNotificaion,
        //            DateJoined = x.DateJoined,
        //            Remark = x.Remark,
        //            isOTP = x.isOTP,
        //            Status = x.Status,
        //            Ranking = x.Ranking,
        //            Referral = x.Referral,
        //            AccountHolderName = x.AccountHolderName,
        //            BankAccountNumber = x.BankAccountNumber,
        //            BankName = x.BankName,
        //            SecondaryAccountHolderName = x.SecondaryAccountHolderName,
        //            SecondaryBankAccountNumber = x.SecondaryBankAccountNumber,
        //            SecondaryBankName = x.SecondaryBankName,
        //            SecondaryPrefferdCurrency = x.SecondaryPrefferdCurrency,
        //            isApprovedByStaff = x.isApprovedByStaff,
        //            CreatedBy = x.CreatedBy,
        //            CreatedDateTime = x.CreatedDateTime,
        //            ModifiedBy = x.ModifiedBy,
        //            ModifiedDateTime = x.ModifiedDateTime,
        //            RegisterVia = x.RegisterVia,
        //            IsChangePassword = x.IsChangePassword,
        //            IsNotify = x.IsNotify,
        //            IsCompleted = x.IsCompleted
        //        }).FirstOrDefault();
        //    }
        //    return Json(obj, JsonRequestBehavior.AllowGet);
        //}

    }
}
