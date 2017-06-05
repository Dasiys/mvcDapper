using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.UnitOfWork;
using Dapper;
using Model.MetadataModel;
using IDAL;
using Model.Dtos;

namespace DAL
{
    public class UserDal:BaseDal<TB_UserInfo>,IUserDal
    {
        public UserDal(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        private const string TableName = "TB_UserInfo";


        /// <summary>
        /// 更改密码
        /// </summary>
        /// <param name="newPassword"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public int UpdatePassword(string newPassword, int id)
        {
            var uParam = new DynamicParameters();
            uParam.Add("Password", newPassword);
            uParam.Add("Uid", id);
            var sql = $"update {TableName} set Password=@Password where Uid=@Uid";
            var result = Excute(sql, uParam);
            return result;
        }

        /// <summary>
        /// 通过账户密码获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public int FindUserByIdentity(int id, string password)
        {
            var param = new DynamicParameters();
            param.Add("Uid", id);
            param.Add("Password", password);
            var condition = " Uid=@Uid and Password=@Password";
            var result = GetSingleModel<int>(TableName, condition, param, "count(1)");
            return result;
        }

        /// <summary>
        /// 通过手机号码查询
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public int FindUserByMobile(string mobile)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("Mobile", mobile);
            return GetSingleModel<int>(TableName, "Mobile=@Mobile", param, "count(1)");
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="companyName"></param>
        /// <param name="password"></param>
        /// <param name="contact"></param>
        /// <param name="regType"></param>
        /// <returns></returns>
        public int AddUser(string mobile, string companyName, string password, string contact, RegType regType)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("Mobile", mobile);
            param.Add("@CompanyName", companyName);
            param.Add("Password", password);
            param.Add("Contact", contact);
            param.Add("RegType", regType);
            return ExcuteProc("Proc_RegAdd", param);
        }

        /// <summary>
        /// 设置或获取用户基础信息
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public TB_UserInfo GetUserAccountInfo(string mobile, string password)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("Mobile", mobile.Trim());
            param.Add("Password", password.Trim());
            var condition = "Mobile=@Mobile and password=@Password";
            var fields = "Uid as Id,Mobile,Contact,IsShop";
            var result = GetSingleModel<TB_UserInfo>(TableName, condition, param, fields);
            return result;
        }


        /// <summary>
        /// 获取个人信息详情
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public UserInfoModifyModel GetUserInfoDetail(int uid)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("Uid", uid);
            var condition = "Uid=@uid";
            var field = "CompanyName,Mobile,Contact,Email,QQ,Tel,Addr,Photo,Uid";
            var result =GetSingleModel<UserInfoModifyModel>(TableName, condition, param, field);
            return result;
        }

        /// <summary>
        /// 更改用户信息
        /// </summary>
        /// <param name="entity"></param>
        public int UpdateUserInfo(UserInfoModifyModel entity)
        {
            var sql =
                $"Update {TableName} Set CompanyName=@CompanyName,Mobile=@Mobile,Contact=@Contact,Email=@Email,QQ=@QQ,Tel=@Tel,Addr=@Addr,Photo=@Photo " +
                "where Uid=@Uid";
            return Excute(sql, entity);
        }
    }
}
