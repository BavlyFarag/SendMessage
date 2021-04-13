using Innovs.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Innovs.Service.User
{
    public class User
    {
        protected UnitOfWork unitOfWork = new UnitOfWork();
        protected Repository<Core.Data.User> UserRepository;

        public User()
        {
            UserRepository = unitOfWork.Repository<Core.Data.User>();
        }

        #region User
        public Core.Data.User GetUserById(int id)
        {
            var user = UserRepository.Table.Where(c => c.Id == id).FirstOrDefault();
            return user;
        }
        public bool AddUserCheck(Core.Data.User model)
        {
            return UserRepository.Table.Where(c => c.Email == model.Email || c.UserName == model.UserName).Any();
        }
        public bool UserAdd(Core.Data.User model)
        {
            try
            {
                UserRepository.Insert(model);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public Core.Data.User UserEdit(Core.Data.User model)
        {

            var newUser = UserRepository.GetById(model.Id);
            foreach (var item in model.GetType().GetProperties())
            {
                foreach (var item2 in newUser.GetType().GetProperties())
                {
                    if (item.GetValue(model) != null)
                    {
                        if (item2.Name == item.Name)
                        {
                            item2.SetValue(newUser, item.GetValue(model));
                        }
                    }
                }
            }

            UserRepository.Update(newUser);
            return newUser;
        }

        public IQueryable<Core.Data.User> GetAllUser()
        {
            var users = UserRepository.Table.OrderByDescending(c => c.Id).AsQueryable();
            return users;
        }
        public IQueryable<Core.Data.User> SearchFor(Expression<Func<Core.Data.User, bool>> predicate)
        {
            return UserRepository.SearchFor(predicate);
        }

        #endregion
    }
}
