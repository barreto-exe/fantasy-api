using Core.Utils.Mapping;
using FantasyApi.Data.Auth.Inputs;
using FantasyApi.Data.Base.Dtos;
using FantasyApi.Data.Base.Requests;
using FantasyApi.Data.Events.Dtos;
using FantasyApi.Data.Users.Dtos;
using FantasyApi.Data.Users.Exceptions;
using FantasyApi.Data.Users.Inputs;
using FantasyApi.Interfaces;
using FantasyApi.Utils;
using MySqlConnector;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FantasyApi.Services
{
    public class UserService : BaseService, IUserService
    {
        public UserService(IDatabaseService databaseService) : base(databaseService) { }

        public async Task<UserDto> GetUserById(int id)
        {
            List<MySqlParameter> parameters = new()
            {
                new MySqlParameter("idUser", id),
            };

            var cmd = _databaseService.GetCommand("GetUserById", parameters);
            var data = await _databaseService.ExecuteStoredProcedureAsync(cmd);

            if (data.Rows.Count > 0)
            {
                var mapper = new DataNamesMapper<UserDto>();
                var user = mapper.Map(data.Rows[0]);

                return user;
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var cmd = _databaseService.GetCommand("GetUsers");
            var data = await _databaseService.ExecuteStoredProcedureAsync(cmd);

            if (data.Rows.Count > 0)
            {
                var mapper = new DataNamesMapper<UserDto>();
                var user = mapper.Map(data);

                return user;
            }
            else
            {
                return null;
            }
        }

        public async Task<PaginatedListDto<UserDto>> GetAllUsersPaginatedAsync(BaseRequest filter)
        {
            return await GetItemsPaginated<UserDto>(filter, "GetUsersPaginated");
        }

        public async Task<UserDto> GetUserByMailAndPassAsync(LoginInput input)
        {
            List<MySqlParameter> parameters = new()
            {
                new MySqlParameter("user_email", input.Email),
                new MySqlParameter("user_pass", input.Password),
            };

            var cmd = _databaseService.GetCommand("GetUserByMailAndPass", parameters);
            var data = await _databaseService.ExecuteStoredProcedureAsync(cmd);

            if (data.Rows.Count > 0)
            {
                var mapper = new DataNamesMapper<UserDto>();
                var user = mapper.Map(data.Rows[0]);

                return user;
            }
            else
            {
                return null;
            }
        }

        /// <exception cref="UserExistsException"></exception>
        public async Task<UserDto> AddUserAsync(UserAddInput input)
        {
            //Validate if email has already been used
            var users = await GetAllUsersAsync();
            bool userExists = (from u in users
                               where u.Email == input.Email
                               select u).ToList().Count > 0;
            if (userExists)
            {
                throw new UserExistsException();
            }

            List<MySqlParameter> parameters = new()
            {
                new MySqlParameter("user_name", input.Name),
                new MySqlParameter("user_role", input.Role),
                new MySqlParameter("user_birth_date", input.BirthDate),
                new MySqlParameter("user_email", input.Email),
                new MySqlParameter("user_pass", input.Password),
            };

            var cmd = _databaseService.GetCommand("AddUser", parameters);
            await _databaseService.ExecuteStoredProcedureAsync(cmd);

            return await GetUserByMailAndPassAsync(new LoginInput()
            {
                Email = input.Email,
                Password = input.Password,
            });
        }

        /// <exception cref="UserDoesntExistException"></exception>
        public async Task<UserDto> UpdateUserAsync(UserUpdateInput input)
        {
            var user = await GetUserById(input.Id);
            if (user == null)
            {
                throw new UserDoesntExistException();
            }

            List<MySqlParameter> parameters = new()
            {
                new MySqlParameter("user_id", input.Id),
                new MySqlParameter("user_name", input.Name ?? user.Name),
                new MySqlParameter("user_role", input.Role ?? user.Role),
                new MySqlParameter("user_birth_date", input.BirthDate ?? user.BirthDate),
                new MySqlParameter("user_email", input.Email ?? user.Email),
                new MySqlParameter("user_pass", input.Password ?? user.PasswordMd5),
            };

            var cmd = _databaseService.GetCommand("UpdateUser", parameters);
            await _databaseService.ExecuteStoredProcedureAsync(cmd);

            return await GetUserByMailAndPassAsync(new LoginInput()
            {
                Email = input.Email ?? user.Email,
                Password = input.Password ?? user.PasswordMd5,
            });
        }

        /// <exception cref="UserDoesntExistException"></exception>
        public async Task DeleteUserAsync(int id)
        {
            var user = await GetUserById(id);
            if (user == null)
            {
                throw new UserDoesntExistException();
            }

            List<MySqlParameter> parameters = new()
            {
                new MySqlParameter("idUser", id),
            };

            var cmd = _databaseService.GetCommand("DeleteUser", parameters);
            await _databaseService.ExecuteStoredProcedureAsync(cmd);
        }
    }
}
