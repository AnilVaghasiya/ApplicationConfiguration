namespace GodrejMaterialDAL.Repository
{
    using GodrejMaterialDAL.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public sealed class AccountRepository
    {
        private static IDA_db_godrejEntities _dbContext;

        private static readonly Lazy<AccountRepository> Repository = new Lazy<AccountRepository>(() => new AccountRepository());
        private AccountRepository()
        {
            _dbContext = DBContextRepository.Instance;
        }
        public static AccountRepository Instance
        {
            get
            {
                return Repository.Value;
            }
        }

        public string ValidateUser(Guid userId, string password)
        {
            try
            {
                var user = _dbContext.aspnet_Membership.Where(x => x.UserId == userId && x.Password.ToLower() == password.ToLower()).FirstOrDefault();
                if (user != null)
                {
                    if (user.IsApproved)
                    {
                        return "true";
                    }
                    else if (user.IsLockedOut)
                    {
                        return "Your account has been blocked";
                    }
                }
                else
                {
                    return "Your User ID and/or Password are invalid.";
                }

            }
            catch (Exception ex)
            {
                return "Your account has been blocked, Please contact to system administrator";
            }
            return "true";
        }
    }
}
