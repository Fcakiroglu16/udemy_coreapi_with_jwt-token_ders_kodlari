using System;
using UdemyApiWithToken.Domain;
using UdemyApiWithToken.Domain.Repositories;
using UdemyApiWithToken.Domain.Responses;
using UdemyApiWithToken.Domain.Services;
using UdemyApiWithToken.Domain.UnitOfWork;

namespace UdemyApiWithToken.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        private readonly IUnitOfWork<UdemyApiWithTokenDBContext> unitOfWork;

        public UserService(IUserRepository userRepository, IUnitOfWork<UdemyApiWithTokenDBContext> unitOfWork)
        {
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
        }

        public BaseResponse<User> AddUser(User user)
        {
            try
            {
                userRepository.AddUser(user);
                unitOfWork.Complete();
                return new BaseResponse<User>(user);
            }
            catch (Exception ex)
            {
                return new BaseResponse<User>($"Kullanıcı eklenirken bir hata meydana geldi:{ex.Message}");
            }
        }

        public BaseResponse<User> FindById(int userId)
        {
            try
            {
                User user = userRepository.FindById(userId);

                if (user == null)
                {
                    return new BaseResponse<User>("Kullanıcı bulunamadı.");
                }

                return new BaseResponse<User>(user);
            }
            catch (Exception ex)
            {
                return new BaseResponse<User>($"Kullanıcı bulunurken bir hata meydana geldi:{ex.Message}");
            }
        }

        public BaseResponse<User> FindEmailAndPassword(string email, string password)
        {
            try
            {
                User user = userRepository.FindByEmailandPassword(email, password);
                if (user == null)
                {
                    return new BaseResponse<User>("Kullanıcı bulunamadı.");
                }
                else
                {
                    return new BaseResponse<User>(user);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<User>($"Kullanıcı bulunurken bir hata meydana geldi:{ex.Message}");
            }
        }

        public BaseResponse<User> GetUserWithRefreshToken(string refreshToken)
        {
            try
            {
                User user = userRepository.GetUserWithRefreshToken(refreshToken);

                if (user == null)
                {
                    return new BaseResponse<User>("Kullanıcı bulunamadı.");
                }
                else
                {
                    return new BaseResponse<User>(user);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<User>($"Kullanıcı bulunurken bir hata meydana geldi:{ex.Message}");
            }
        }

        public void RemoveRefreshToken(User user)
        {
            try
            {
                userRepository.RemoveRefreshToken(user);
                unitOfWork.Complete();
            }
            catch (Exception)
            {
                //loglama yapılacaktır.
            }
        }

        public void SaveRefreshToken(int userId, string refreshToken)
        {
            try
            {
                userRepository.SaveRefreshToken(userId, refreshToken);

                unitOfWork.Complete();
            }
            catch (Exception)
            {
                //loglama yapılacaktır..
            }
        }
    }
}