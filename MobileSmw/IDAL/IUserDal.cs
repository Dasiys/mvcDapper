using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.MetadataModel;
using IOC;
using Model.ViewModel;

namespace IDAL
{
    public interface IUserDal:IBaseDal<TB_UserInfo>,IDependency
    {
        /// <summary>
        /// 更改密码
        /// </summary>
        /// <param name="newPassword"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        int UpdatePassword(string newPassword, int id);

        /// <summary>
        /// 通过账户密码获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        int FindUserByIdentity(int id, string password);

        /// <summary>
        /// 通过手机号码查询
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        int FindUserByMobile(string mobile);

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="companyName"></param>
        /// <param name="password"></param>
        /// <param name="contact"></param>
        /// <param name="regType"></param>
        /// <returns></returns>
        int AddUser(string mobile, string companyName, string password, string contact, RegType regType);

        /// <summary>
        /// 设置或获取用户基础信息
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        TB_UserInfo GetUserAccountInfo(string mobile, string password);

        /// <summary>
        /// 获取个人信息详情
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        UserInfoModifyModel GetUserInfoDetail(int uid);

        /// <summary>
        /// 更改用户信息
        /// </summary>
        /// <param name="entity"></param>
        int UpdateUserInfo(UserInfoModifyModel entity);
    }
}
