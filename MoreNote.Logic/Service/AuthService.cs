﻿using System;

using MoreNote.Common.Util;
using MoreNote.Common.Utils;
using MoreNote.Logic.Entity;

namespace MoreNote.Logic.Service
{
    public class AuthService
    {
        public static bool LoginByPWD(String email, string pwd, out string tokenStr,out User user)
        {
            user = UserService.GetUser(email);
            if (user != null)
            {
                string temp = Encrypt_Helper.Hash256Encrypt(pwd + user.Salt);
                if (temp.Equals(user.Pwd))
                {
                    long tokenid = SnowFlake_Net.GenerateSnowFlakeID();
                    var token=Base64Util.ToBase64String(tokenid.ToString("x") + "@" + RndNum.CreatRndNum(16));
                    Token myToken = new Token
                    {
                        TokenId = SnowFlake_Net.GenerateSnowFlakeID(),
                        UserId = user.UserId,
                        Email = user.Email,
                        TokenStr = token,
                        Type = 0,
                        CreatedTime = DateTime.Now
                    };
                    TokenSerivce.AddToken(myToken);
                    tokenStr = myToken.TokenStr;
                    return true;
                }
                else
                {
                    tokenStr = "";
                    return false;
                }

            }
            else
            {
                tokenStr = "";
                return false;
            }

        }
        public static bool LoginByToken(string email, string token)
        {
            return true;
        }
        /// <summary>
        /// 通过Token判断用户是否登录
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="tokenStr"></param>
        /// <returns></returns>
        public static bool IsLogin(long userid,string tokenStr)
        {
            Token token = TokenSerivce.GetTokenByTokenStr(userid
                , tokenStr);
            if (token!=null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // 使用bcrypt认证或者Md5认证
        // Use bcrypt (Md5 depreciated)
        public static User Login(string emailOrUserName ,string pwd)
        {
            throw new Exception();
        }
        public static bool Register(string email,string pwd,long fromUserId)
        {
            throw new Exception();
        }
        public static bool register(User
             user)
        {
            throw new Exception();
        }
        public static string getUsername(string thirdType,string thirdUserName)
        {
            throw new Exception();
        }
        public User ThirdRegister(string thirdType,string thirdUserId,string thirdUserName)
        {
            throw new Exception();
        }

    }
}
