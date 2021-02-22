using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Data;
using AutoMapper;
using API.Dtos;
using Microsoft.AspNetCore.JsonPatch;
using System.Threading.Tasks;
using Newtonsoft.Json;
using API.Encode;

namespace API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {


        //! เป็นการช้งาน Dependency injection (DI) โดยการไม่เรียกใช้งาน new 
        //! เหมือนบรรทัดที่ 20
        //* พิมพ์ ctor เพื่อ เพิ่ม function ชื่อเดียวกับ controller
        //* public CategoryController(Parameters)
        //* {

        //* }

        private readonly IUserService Impuser;
        private readonly IMapper _mapper;

        public UserController(IUserService IUser, IMapper mapper)
        {
            Impuser = IUser;
            _mapper = mapper;
        }

        //*private readonly ImpuseregoryService Impuser = new ImpuseregoryService();

        //Get api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUser()
        {
            var t_Impuser = Impuser.GetAllUser();
            await Task.WhenAll(t_Impuser);
            var _Impuser = await t_Impuser;

            return Ok(_mapper.Map<IEnumerable<UserReadDto>>(_Impuser));
        }

        //Get api/User/{id}
        [HttpGet("{id}", Name = "GetUserById")]
        public async Task<ActionResult<UserReadDto>> GetUserById(string id)
        {
            var t_Impuser = Impuser.GetUserById(id);
            await Task.WhenAll(t_Impuser);
            var _Impuser = await t_Impuser;

            if (_Impuser != null)
            {
                return Ok(_mapper.Map<UserReadDto>(_Impuser));
            }
            else
            {
                return NotFound();
            }

        }

        [HttpPost("Condition", Name = "GetUserByCondition")]
        public async Task<ActionResult<string>> GetUserByCondition(ConditionModel condition)
        {
            var t_Impuser = Impuser.GetUserByCondition(condition);
            await Task.WhenAll(t_Impuser);
            var _Impuser = await t_Impuser;

            if (_Impuser != null)
            {
                var encryptjson = EncryptDecryptService.EncryptAes(JsonConvert.SerializeObject(_mapper.Map<IEnumerable<UserReadDto>>(_Impuser)));
                condition.clear();
                condition.encrypt = encryptjson;
                return Ok(condition);
            }
            else
            {
                return NotFound();
            }

        }
        //Post api/User
        [HttpPost]
        public ActionResult<UserReadDto> CreateUser(UserCreateDto UserCreateDto)
        {
            var UserModel = _mapper.Map<User>(UserCreateDto);
            Impuser.CreateUser(UserModel);
            Impuser.SaveChanges();

            //* _mapper.Map(Source,Destination) 
            //* แม็พจาก อ็อบเจ็กต์ต้นทาง ไปยัง อ็อบเจ็กต์ปลายทาง ใหม่ ด้วยอ็อพชันการแม็พที่ให้มา

            var userReadDto = _mapper.Map<UserReadDto>(UserModel);

            return CreatedAtRoute(nameof(GetUserById), new { Id = userReadDto.UserID }, userReadDto);


        }

        //Put api/Category/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(string id, UserUpdateDto userUpdateDto)
        {
            //* เช็คก่อนว่า id ที่ให้มา มีอยู่ไหม

            var _UserModelFromImpuser = Impuser.GetUserById(id);
            await Task.WhenAll(_UserModelFromImpuser);
            var userModelFromImpuser = await _UserModelFromImpuser;
            if (userModelFromImpuser == null)
            {
                return NotFound();
            }

            //* _mapper.Map(Source,Destination) 
            //* แม็พจาก อ็อบเจ็กต์ต้นทาง ไปยัง อ็อบเจ็กต์ปลายทาง ที่มีอยู่ ด้วยอ็อพชันการแม็พที่ให้มา

            _mapper.Map(userUpdateDto, userModelFromImpuser);
            Impuser.UpdateUser(userModelFromImpuser);
            Impuser.SaveChanges();

            return NoContent();

        }

        //Patch api/Category/{id}
        [HttpPatch("{id}")]
        public async Task<ActionResult> PartialUpdateCategory(string id, JsonPatchDocument<UserUpdateDto> patchDoc)
        {
            var _UserModelFromImpuser = Impuser.GetUserById(id);
            await Task.WhenAll(_UserModelFromImpuser);
            var userModelFromImpuser = await _UserModelFromImpuser;

            if (userModelFromImpuser == null)
            {
                return NotFound();
            }
            var UserToPacth = _mapper.Map<UserUpdateDto>(userModelFromImpuser);
            patchDoc.ApplyTo(UserToPacth, ModelState);
            if (!TryValidateModel(UserToPacth))
            {
                return NotFound();
            }

            //* _mapper.Map(Source,Destination) 
            //* แม็พจาก อ็อบเจ็กต์ต้นทาง ไปยัง อ็อบเจ็กต์ปลายทาง ที่มีอยู่ ด้วยอ็อพชันการแม็พที่ให้มา

            _mapper.Map(UserToPacth, userModelFromImpuser);
            Impuser.UpdateUser(userModelFromImpuser);
            Impuser.SaveChanges();

            return NoContent();

        }

        //Delete api/Category/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(string id)
        {
            var _UserModelFromImpuser = Impuser.GetUserById(id);
            await Task.WhenAll(_UserModelFromImpuser);
            var userModelFromImpuser = await _UserModelFromImpuser;

            if (userModelFromImpuser == null)
            {
                return NotFound();
            }
            Impuser.DeleteUser(userModelFromImpuser);
            Impuser.SaveChanges();

            return NoContent();

        }

    }
}