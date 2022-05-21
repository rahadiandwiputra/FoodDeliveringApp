using FoodDeleveryApp.Data.Models;
using HotChocolate.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace UserService.GraphQL
{
    public class Mutation
    {
        [Authorize(Roles = new[] { "ADMIN" })]
        public async Task<UserData> UpdateUserAsync(
            UpdateUser input,
            [Service] FoodDeliveryContext context
            )
        {
            var user = context.Users.Where(s=>s.Id == input.Id).FirstOrDefault();
            if (user != null)
            {
                user.FullName = input.FullName;
                user.Email = input.Email;
                user.Username = input.UserName;
                user.Password = BCrypt.Net.BCrypt.HashPassword(input.Password);
                context.Users.Update(user);
                await context.SaveChangesAsync();
            }
            return await Task.FromResult(new UserData
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                FullName = user.FullName
            });
        }
        [Authorize(Roles = new[] { "ADMIN" })]
        public async Task<UserData> DeleteUserAsync(
            int id,
            [Service] FoodDeliveryContext context
            )
        {
            var courier = context.UserRoles.FirstOrDefault(s => s.UserId == id);
            var profile = context.Profiles.FirstOrDefault(s => s.UserId == id);
            var courierTable = context.Couriers.FirstOrDefault(s => s.UserId == id);
            var user = context.Users.Where(s => s.Id == id).FirstOrDefault();
            if (user != null)
            {
                context.Profiles.Remove(profile);
                context.UserRoles.Remove(courier);
                context.Users.Remove(user);
                await context.SaveChangesAsync();
            }
            return await Task.FromResult(new UserData
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                FullName = user.FullName
            });
        }

        [Authorize(Roles = new[] { "ADMIN" })]
        public async Task<UserData> CreateUserConditionAsync(
            CreateUser input,
            [Service] FoodDeliveryContext context)
        {
            var user = context.Users.Where(o => o.Username == input.UserName).FirstOrDefault();
            if (user != null)
            {
                return await Task.FromResult(new UserData());
            }
            var newUser = new User
            {
                FullName = input.FullName,
                Email = input.Email,
                Username = input.UserName,
                Password = BCrypt.Net.BCrypt.HashPassword(input.Password) // encrypt password
            };
            context.Users.Add(newUser);
            await context.SaveChangesAsync();

            var newProfile = new Profile
            {
                UserId = newUser.Id,
                Name = newUser.FullName,
            };
            context.Profiles.Add(newProfile);

            var newUserRole = new UserRole
            {
                RoleId = input.RoleId,
                UserId = newUser.Id,
            };
            context.UserRoles.Add(newUserRole);
            // EF
            await context.SaveChangesAsync();

            return await Task.FromResult(new UserData
            {
                Id = newUser.Id,
                Username = newUser.Username,
                Email = newUser.Email,
                FullName = newUser.FullName
            });
        }
        [Authorize(Roles = new[] { "MANAGER" })]
        public async Task<UserData> CreateCourierConditionAsync(
            CreateUser input,
            [Service] FoodDeliveryContext context)
        {
            var user = context.Users.Where(o => o.Username == input.UserName).FirstOrDefault();
            if (user != null)
            {
                return await Task.FromResult(new UserData());
            }
            var newUser = new User
            {
                FullName = input.FullName,
                Email = input.Email,
                Username = input.UserName,
                Password = BCrypt.Net.BCrypt.HashPassword(input.Password) // encrypt password
            };
            context.Users.Add(newUser);
            await context.SaveChangesAsync();
            var newCourier = new Courier
            {
                Name = newUser.FullName,
                UserId = newUser.Id,
            };
            context.Couriers.Add(newCourier);

            var newProfile = new Profile
            {
                UserId = newUser.Id,
                Name = newUser.FullName,
            };
            context.Profiles.Add(newProfile);

            var newUserRole = new UserRole
            {
                RoleId = input.RoleId,
                UserId = newUser.Id,
            };
            context.UserRoles.Add(newUserRole);
            // EF
            await context.SaveChangesAsync();

            return await Task.FromResult(new UserData
            {
                Id = newUser.Id,
                Username = newUser.Username,
                Email = newUser.Email,
                FullName = newUser.FullName
            });
        }

        // By Default Register Sebagai Buyer
        public async Task<UserData> RegisterUserAsync(
            RegisterUser input,
            [Service] FoodDeliveryContext context)
        {
            var user = context.Users.Where(o => o.Username == input.UserName).FirstOrDefault();
            if (user != null)
            {
                return await Task.FromResult(new UserData());
            }
            var newUser = new User
            {
                FullName = input.FullName,
                Email = input.Email,
                Username = input.UserName,
                Password = BCrypt.Net.BCrypt.HashPassword(input.Password) // encrypt password
            };
            context.Users.Add(newUser);
            await context.SaveChangesAsync();

            var newProfile = new Profile
            {
                UserId = newUser.Id,
                Name = newUser.FullName,
            };
            context.Profiles.Add(newProfile);
            var newUserRole = new UserRole
            {
                RoleId = 2,
                UserId = newUser.Id,
            };
            context.UserRoles.Add(newUserRole);
            // EF
            await context.SaveChangesAsync();

            return await Task.FromResult(new UserData
            {
                Id = newUser.Id,
                Username = newUser.Username,
                Email = newUser.Email,
                FullName = newUser.FullName
            });
        }
        public async Task<UserToken> LoginAsync(
            LoginUser input,
            [Service] IOptions<TokenSettings> tokenSettings, // setting token
            [Service] FoodDeliveryContext context) // EF
        {
            var user = context.Users.Where(o => o.Username == input.Username).FirstOrDefault();
            if (user == null)
            {
                return await Task.FromResult(new UserToken(null, null, "Username or password was invalid"));
            }
            bool valid = BCrypt.Net.BCrypt.Verify(input.Password, user.Password);
            if (valid)
            {
                // generate jwt token
                var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.Value.Key));
                var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

                // jwt payload
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, user.Username));

                var userRoles = context.UserRoles.Where(o => o.UserId == user.Id).ToList();
                foreach (var userRole in userRoles)
                {
                    var role = context.Roles.Where(o => o.Id == userRole.RoleId).FirstOrDefault();
                    if (role != null)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role.Name));
                    }
                }

                var expired = DateTime.Now.AddHours(3);
                var jwtToken = new JwtSecurityToken(
                    issuer: tokenSettings.Value.Issuer,
                    audience: tokenSettings.Value.Audience,
                    expires: expired,
                    claims: claims, // jwt payload
                    signingCredentials: credentials // signature
                );

                return await Task.FromResult(
                    new UserToken(new JwtSecurityTokenHandler().WriteToken(jwtToken),
                    expired.ToString(), null));
                //return new JwtSecurityTokenHandler().WriteToken(jwtToken);
            }

            return await Task.FromResult(new UserToken(null, null, Message: "Username or password was invalid"));
        }

        [Authorize]
        public async Task<ResponseChangePassword> ChangePasswordAsync(
            ChangePassword input,
            ClaimsPrincipal claimsPrincipal,// setting token
            [Service] FoodDeliveryContext context) // EF
        {
            var userName = claimsPrincipal.Identity.Name;

            var user = context.Users.Where(o => o.Username == userName).FirstOrDefault();
            bool valid = BCrypt.Net.BCrypt.Verify(input.CurrentPassword, user.Password);

            if (valid)
            {
                if (input.NewPassword != input.ConfirmPassword)
                {
                    return await Task.FromResult(new ResponseChangePassword(Message: "Current Password And Confirmation Password Are Not The Same", Created: DateTime.Now.ToString()));
                }
                user.Password = BCrypt.Net.BCrypt.HashPassword(input.ConfirmPassword);
                context.Users.Update(user);
                context.SaveChangesAsync();
                return await Task.FromResult(new ResponseChangePassword(Message: "Password Has Been Updated", Created: DateTime.Now.ToString()));
            }
            return await Task.FromResult(new ResponseChangePassword(Message: "Failed To Update Password", Created: DateTime.Now.ToString()));
        }

        [Authorize(Roles = new[] { "MANAGER" })]
        public async Task<string> DeleteCourierAsync(int id, [Service] FoodDeliveryContext context, ClaimsPrincipal claimsPrincipal)
        {
            var user = context.Users.FirstOrDefault(o => o.Id == id);
            var courier = context.UserRoles.FirstOrDefault(s => s.UserId == user.Id);
            var profile = context.Profiles.FirstOrDefault(s => s.UserId == user.Id);
            var courierTable = context.Couriers.FirstOrDefault(s => s.UserId == user.Id);
            if (courier == null) return "Courier Data Notfound!";
            if (courier.RoleId != 4) return "This Is Not Courier Data";
            if (user != null && courier.RoleId == 4)
            {
                context.Profiles.Remove(profile);
                context.Couriers.Remove(courierTable);
                context.UserRoles.Remove(courier);
                context.Users.Remove(user);
                await context.SaveChangesAsync();
                return "Courier Data Deleted";
            }
            //if (user == null) return "Users Data Notfound!";

            return "Courier Data Is Not Deleted";
        }
        [Authorize]
        public async Task<ProfileInput1> UpdateProfileAsync(ProfileInput1 input, [Service] FoodDeliveryContext context, ClaimsPrincipal claimsPrincipal)
        {
            var userName = claimsPrincipal.Identity.Name;
            var user = context.Users.Where(u => u.Username == userName).FirstOrDefault();
            if (user == null) return new ProfileInput1(0,"", "", "","");
            
            var profile = context.Profiles.Where(p => p.UserId == user.Id).FirstOrDefault();
            profile.Name = input.Name;
            profile.City = input.City;
            profile.Address = input.Address;
            profile.Phone = input.Phone;
            context.Profiles.Update(profile);
            user.FullName = input.Name;
            context.Users.Update(user);
            await context.SaveChangesAsync();

            return input;
        }
    }
}
