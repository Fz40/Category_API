using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using API.Encode;
using API.Models;
using Newtonsoft.Json;

namespace API.Data
{
    public class ImpUserService : IUserService
    {
        private readonly NorthwindContext db;
        private readonly HashService hash;
        public ImpUserService(NorthwindContext _db, HashService _hash)
        {
            db = _db;
            hash = _hash;
        }
        public void CreateUser(User user)
        {
            User findUser = new User();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            findUser = db.User.FirstOrDefault(u => u.UserID == user.UserID);
            if (findUser == null)
            {
                user.Password = hash.ComputeStringToSha512Hash(user.Password);
                db.User.Add(user);
            }
        }

        public void DeleteUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            db.User.Remove(user);
        }

        public async Task<IEnumerable<User>> GetAllUser()
        {
            IEnumerable<User> data;
            try
            {
                data = db.User.ToList();
                return await Task.FromResult(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<User>> GetUserByCondition(ConditionModel condition)
        {
            IEnumerable<User> data;
            try
            {
                var decrypt = EncryptDecryptService.DecryptAes(condition.encrypt);
                var _condition = JsonConvert.DeserializeObject<ConditionModel>(decrypt);
                data = db.User;
                // .AsNoTracking()
                // .AsExpandable();
                if (_condition.sid != null && _condition.sid.Any())
                {
                    data = data.Where(u => u.UserID.Equals(_condition.sid));
                }
                if (_condition.Name != null && _condition.Name.Any())
                {
                    data = data.Where(u => u.UserName.ToLower().Contains(_condition.Name.ToLower()));
                }
                if (_condition.Email != null && _condition.Email.Any())
                {
                    data = data.Where(u => u.Email.ToLower().Contains(_condition.Email.ToLower()));
                }

                //*
                //* ตัวอย่างการใช้งานการค้นหาแบบหลายเงื่อนไข 
                //* ใช้ function ของ LINQKit
                //*
                // if(mySearchFilter.IsActive)
                //     result = result.Where(p => p.ActiveOnly);

                // if(mySearchFilter.CategoryIds != null && mySearchFilter.CategoryIds.Any())
                //     result = result.Where(p => mySearchFilter.CategoryIds.Contains(p.CategoryId));

                // if(mySearchFilter.Keywords != null && mySearchFilter.Keywords.Any())
                // {
                //     //Crate the builder
                //     var predicate = PredicateBuilder.New();

                //     //Loop through the keywords
                //     foreach(var item in mySearchFilter.Keywords)
                //     {
                //         var currentKeyword = item;
                //         predicate = predicate.Or (p => p.ProductName.Contains (currrentKeyword));
                //         predicate = predicate.Or (p => p.Description.Contains (currrentKeyword));
                //     }
                //     result = result.Where(predicate);
                //*

                data = data.ToList();
                return await Task.FromResult(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<User> GetUserById(string Id)
        {
            User user = new User();
            try
            {
                user = db.User.FirstOrDefault(u => u.UserID == Id);
                return await Task.FromResult(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool SaveChanges()
        {
            //*
            //* ตัวอย่างการใช้งาน begin transaction committ rollback
            //* ในกรณีนี้เราใช้ EF เป็นตัวจัดการ เราจึงประกาศใช้ TransactionScope
            //* และใช้ tranScope.Complete เพื่อจบงาน โดยที่ไม่ต้องใช้ Committ หรือ RollBack
            //* ซึ่งถ้าเราใช้ EF มันจะทำทั้ง 2 ฟังชั่น (เมตตอด) ให้อัตโนมัติ
            //* 
            using (TransactionScope tranScope = new TransactionScope())
                try
                {
                    var res = db.SaveChanges() >= 0;
                    tranScope.Complete();
                    return (res);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    db.Dispose();

                    tranScope.Dispose();
                }

            //*
            //* ตัวอย่างการใช้งาน BeginTransaction Commit Rollback แบบปกติสำหรับใช้กับ LINQ
            //*
            // using (dbDataContext db = new dbDataContext())
            // using (var dbContextTransaction = db.Database.BeginTransaction()) 
            // {
            //     try
            //     { 
            //         var user = new User(){ID = 1, Name = "Nick"};
            //         db.Users.Add(user);
            //         db.SaveChanges();
            //         dbContextTransaction.Commit(); 
            //     } 
            //     catch (Exception) 
            //     { 
            //         dbContextTransaction.Rollback(); 
            //     }
            // } 
        }

        public void UpdateUser(User user)

        {
            //* Nothing 
            //* ไม่ต้องทำอะไรเนื่องจากเรารับค่ามาอยู่ใน Model User อยู่แล้ว 
            //* เราจึงสามารถนำเอาค่าที่ได้ ส่ง save ได้เลย
            //*
        }
    }

}